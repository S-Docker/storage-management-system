using System.Collections;
using System.Collections.Generic;

public interface IDepositToStorage
{
    int AddItem(ItemBase itemToAdd, int quantityToAdd);
    int AddItem(ItemBase itemToAdd, int quantityToAdd, int newSlot);
    void MoveItem();
    void SwapItem();

    /**void GetQuantity();
    void SetQuantity();
    AddQuantity(int slot, int quantityToAdd);
    void RemoveQuantity();
    void RemoveAllQuantity();*/
    int FillQuantity(int slot, int quantityToAdd);

    /**
    int GetMaximumSlots();
    void SetMaximumSlots();

    bool IsSlotEmpty();
    bool IsStorageFull();*/
    int AvailableSlotsCount();
    int CalculateSlotsNeeded(double quantity, double maximumStack);

    void GetPartialStacks(ItemBase item, out int count, out List<int> indexes);
}
