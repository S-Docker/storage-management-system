using System;
using System.Collections;
using System.Collections.Generic;

public class DepositableStorageData : StorageData, IDepositToStorage 
{
    public int AddItem(ItemBase itemToAdd, int quantityToAdd) {
        GetPartialStacks(itemToAdd, out int count, out List<int> indexes);

        int spaceInExistingSlots = count;
        int availableSlots = AvailableSlotsCount();

        int remainingSpaceNeeded = Math.Max(quantityToAdd - spaceInExistingSlots, 0); // clamped to min value of 0 as anything less means no additional slots required
        int slotsNeeded = CalculateSlotsNeeded(remainingSpaceNeeded, itemToAdd.MaximumStackSize);

        // If there are no available slots of partial stacks to fill, return value
        if (availableSlots == 0 && indexes.Count == 0) {
            return remainingSpaceNeeded;
        }

        // Fill any existing slots
        if (indexes.Count > 0) {
            for (int i = 0; i < indexes.Count; i++) {
                quantityToAdd = FillQuantity(i, quantityToAdd); // Returns the remaining quantity that could not be added to that partial stack

                if (quantityToAdd == 0) {
                    return quantityToAdd; // Break out of loop early if no further quantity remains before filling all partial stacks
                }
            }
        }

        // Store remaining quantity in new inventory slots
        for (int i = 0; i < _storage.Length; i++) {
            if (_storage[i] == null) {
                int maximumQuantity = Math.Min(quantityToAdd, itemToAdd.MaximumStackSize); // quantity added to stack should never be more than maxStackSize, take smallest of two

                ItemQuantity tempItem = new ItemQuantity(itemToAdd, maximumQuantity);

                _storage[i] = tempItem;
                _storageSlotsInUse++;
                quantityToAdd -= maximumQuantity;
            }

            if (quantityToAdd == 0) {
                return quantityToAdd; // break out of loop early if all necessary empty inventory slots have been filled
            }
        }

        return quantityToAdd;
    }

    public int AddItem(ItemBase itemToAdd, int quantityToAdd, int newSlot) {
        if (_storage[newSlot] == null) {
            ItemQuantity temp = new ItemQuantity(itemToAdd, quantityToAdd);
            _storage[newSlot] = temp;

            return 0;
        } else {
            quantityToAdd = FillQuantity(newSlot, quantityToAdd);
            return quantityToAdd;
        }
    }

    public int AvailableSlotsCount() {
        return _maximumStorageSlots - _storageSlotsInUse;
    }

    public ItemBase GetItem(int checkSlot) {
        throw new System.NotImplementedException();
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
                    int spaceRemaining = _storage[i].MaximumStackSize - _storage[i].Quantity;

                    if (spaceRemaining > 0) {
                        count += spaceRemaining;
                        indexes.Add(i);
                    }
                }
            }
        }
    }

    public void MoveItem() {
        throw new System.NotImplementedException();
    }

    public void RemoveItem() {
        throw new System.NotImplementedException();
    }

    public void SwapItem() {
        throw new System.NotImplementedException();
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
    public int FillQuantity(int slot, int quantityToAdd) {
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
}
