using System;
using System.Collections;
using System.Collections.Generic;

public class DepositableStorageData : StorageData, IDepositToStorage {

    /// <summary>
    /// Obtains a list of indexes for inventory slots that currently contain the item to be added 
    /// and the total number of spaces available in those index slots.
    /// 
    /// If there are existing slots with partial stacks available, iterates through the indexes
    /// and calls the FillQuantity method to fill them up to the maximum stack size and updates 
    /// the quantityToAdd variable to the new value after filling.
    /// 
    /// If there is any quantity leftover after filling the partial stacks, checks whether 
    /// there are empty slots in the storage device, return the leftover value if there is not 
    /// otherwise iterate through each slot in the storage device and fill the first available empty 
    /// slots up to the remaining quantity or the maximum stack size, whichever is smaller.
    /// </summary>
    /// <param name="itemToAdd">Item to be added to storage device</param>
    /// <param name="quantityToAdd">Quantity to be added to storage device</param>
    /// <returns>The remaining quantity that could not be added or 0 if successful</returns>
    public int AddItem(ItemBase itemToAdd, int quantityToAdd) {
        GetPartialStacks(itemToAdd, out int count, out List<int> indexes);

        int spaceInExistingSlots = count;
        int availableSlots = AvailableSlotsCount();

        int remainingSpaceNeeded = Math.Max(quantityToAdd - spaceInExistingSlots, 0); // Clamped to min value of 0 as anything less means no additional slots required
        int slotsNeeded = CalculateSlotsNeeded(remainingSpaceNeeded, itemToAdd.MaximumStackSize);

        // Fill any existing slots
        if (indexes.Count > 0) {
            for (int i = 0; i < indexes.Count; i++) {
                quantityToAdd = FillExistingQuantity(i, quantityToAdd); // Returns the remaining quantity that could not be added to that partial stack

                if (quantityToAdd == 0) {
                    return quantityToAdd; // Break out of loop early if no further quantity remains before filling all partial stacks
                }
            }
        }

        if (availableSlots == 0) {
            return quantityToAdd;
        } else {
            // Store remaining quantity in new inventory slots
            for (int i = 0; i < _storage.Length; i++) {
                if (_storage[i] == null) {
                    int maximumQuantity = Math.Min(quantityToAdd, itemToAdd.MaximumStackSize); // Quantity added to stack should never be more than maxStackSize, take smallest of two

                    ItemQuantity tempItem = new ItemQuantity(itemToAdd, maximumQuantity);

                    _storage[i] = tempItem;
                    _storageSlotsInUse++;
                    quantityToAdd -= maximumQuantity;
                }

                if (quantityToAdd == 0) {
                    return quantityToAdd; // Break out of loop early if all necessary empty inventory slots have been filled
                }
            }
        }

        return quantityToAdd;
    }

    /// <summary>
    /// If the slot is empty then add the new ItemQuantity to the slot.
    /// If the slot is occupied then update the quantity up to the quantity
    /// to be added or the maximum stack size, whichever is smaller.
    /// </summary>
    /// <param name="itemToAdd">Item to be added to storage device</param>
    /// <param name="quantityToAdd">Quantity to be added to storage device</param>
    /// <param name="newSlot">Which slot to add the new item and quantity to</param>
    /// <returns>The remaining quantity that could not be added or 0 if successful</returns>
    public int AddItem(ItemBase itemToAdd, int quantityToAdd, int newSlot) {
        if (_storage[newSlot] == null) {
            ItemQuantity temp = new ItemQuantity(itemToAdd, quantityToAdd);
            _storage[newSlot] = temp;

            return 0;
        } else {
            quantityToAdd = FillExistingQuantity(newSlot, quantityToAdd);
            return quantityToAdd;
        }
    }

    public int AvailableSlotsCount() {
        return _maximumStorageSlots - _storageSlotsInUse;
    }

    public int CalculateSlotsNeeded(double quantity, double maximumStack) {
        return (int)Math.Ceiling(quantity / maximumStack);
    }

    /// <summary>
    /// Checks how much space the current storage slot has available and updates the quantity up to the
    /// maximum stack size before returning the remaining.
    /// </summary>
    /// <param name="slot">Storage slot index to access</param>
    /// <param name="quantityToAdd">Quantity amount to be added</param>
    /// <returns>The remaining quantity after filling the requested slot</returns>
    public int FillExistingQuantity(int slot, int quantityToAdd) {
        ItemQuantity currentStorageSlot = _storage[slot];
        ItemBase currentItemInSlot = currentStorageSlot.Item;
        int currentQuantityInSlot = currentStorageSlot.Quantity;

        if (currentQuantityInSlot + quantityToAdd > currentItemInSlot.MaximumStackSize) {
            quantityToAdd -= (currentItemInSlot.MaximumStackSize - currentStorageSlot.Quantity);
            currentStorageSlot.Quantity = currentItemInSlot.MaximumStackSize;
        } else {
            currentStorageSlot.Quantity += quantityToAdd;
            quantityToAdd = 0;
        }
        return quantityToAdd;
    }

    public ItemBase GetItem(int slot) {
        if (_storage[slot] == null) return null;

        return _storage[slot].Item;
    }

    public int GetMaximumSlots() {
        return _maximumStorageSlots;
    }

    /// <summary>
    /// Iterates over each index in the storage and checks for existing slots that only contain partial stacks of the requested item.
    /// </summary>
    /// <param name="item">Existing item to look for within the storage slots</param>
    /// <param name="count">A count of the maximum free quantity space available in the partial existing stacks</param>
    /// <param name="indexes">A reference to the storage slot indexes that contain partial stacks</param>
    /// <returns>The number of spaces remaining in the partial stacks and the indexes of those partial stacks</returns>
    public void GetPartialStacks(ItemBase item, out int count, out List<int> indexes) {
        count = 0;
        indexes = new List<int>();

        for (int i = 0; i < _maximumStorageSlots; i++) {
            if (_storage[i] != null) {
                if (_storage[i].Item.Id == item.Id) {
                    int spaceRemaining = _storage[i].Item.MaximumStackSize - _storage[i].Quantity;

                    if (spaceRemaining > 0) {
                        count += spaceRemaining;
                        indexes.Add(i);
                    }
                }
            }
        }
    }

    public int GetQuantity(int slot) {
        return _storage[slot].Quantity;
    }

    public bool IsSlotEmpty(int slot) {
        return _storage[slot] == null;
    }

    public bool IsStorageFull() {
        return ((_maximumStorageSlots - _storageSlotsInUse) > 0);
    }

    public void MoveItem(int startingSlot, int newSlot) {
        _storage[newSlot] = _storage[startingSlot];
        _storage[startingSlot] = null;
    }

    public void RemoveQuantity(int slot, int amount) {
        _storage[slot].Quantity -= amount;
    }

    public void SetMaximumSlots(int numOfSlots) {
        _maximumStorageSlots = numOfSlots;
    }

    public void SetQuantity(int slot, int amount) {
        _storage[slot].Quantity = amount;
    }

    public void SwapItem(int slotA, int slotB) {
        ItemQuantity temp = _storage[slotA];
        _storage[slotA] = _storage[slotB];
        _storage[slotB] = temp;
    }
}
