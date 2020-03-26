using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageData : MonoBehaviour, IStorageData  {

    protected ItemQuantity[] _storage;

    // Storage slot variables
    [SerializeField]
    protected int _storageSlotsInUse;
    [SerializeField]
    protected int _maximumStorageSlots = 24;

    #region ---------- Constructors ----------

    public StorageData() {
        _storageSlotsInUse = 0;
        _storage = new ItemQuantity[_maximumStorageSlots];
    }

    public void StorageDataConstructor(int maximumSlots) {
        _storageSlotsInUse = 0;
        _maximumStorageSlots = maximumSlots;
        _storage = new ItemQuantity[_maximumStorageSlots];
    }
    #endregion

    /// <summary>
    /// Find the first available slot inside the storage device and initialise a new ItemQuantity.
    /// </summary>
    /// <param name="item">Item to be added to the first available slot</param>
    /// <param name="quantity">Quantity to be added to the first available slot</param>
    public void InitialiseItem(ItemBase item, int quantity) {
        ItemQuantity temp = new ItemQuantity(item, quantity);

        for (int i = 0; i < _storage.Length; i++) {
            if (_storage[i] == null) {
                _storage[i] = temp;
            }
        }
    }

    /// <summary>
    /// Access the item in slot index i and remove it from the storage device
    /// </summary>
    /// <param name="slot">the storage index to access</param>
    /// <returns>Returns the item removed from selected slot</returns>
    public ItemQuantity RemoveItem(int slot) {
        ItemQuantity temp = _storage[slot];
        _storage[slot] = null;

        return temp;
    }
}
