using System.Collections;
using System.Collections.Generic;

public interface IDepositToStorage
{
    ItemBase GetItem(int slot);
    int AddItem(ItemBase itemToAdd, int quantityToAdd);
    int AddItem(ItemBase itemToAdd, int quantityToAdd, int newSlot);
    void MoveItem(int startingSlot, int newSlot);
    void SwapItem(int slotA, int slotB);

    int GetQuantity(int slot);
    void SetQuantity(int slot, int amount);

    int AvailableSlotsCount();
    int CalculateSlotsNeeded(double quantity, double maximumStack);
    int GetMaximumSlots();
    void SetMaximumSlots(int numOfSlots);

    void RemoveQuantity(int slot, int amount);
    int FillExistingQuantity(int slot, int quantityToAdd);
    void GetPartialStacks(ItemBase item, out int count, out List<int> indexes);

    bool IsSlotEmpty(int slot);
    bool IsStorageFull();
}
