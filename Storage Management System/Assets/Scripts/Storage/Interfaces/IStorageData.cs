using System.Collections;
using System.Collections.Generic;

public interface IStorageData
{
    void InitialiseItem(ItemBase item, int quantity);
    ItemQuantity RemoveItem(int slot);
}
