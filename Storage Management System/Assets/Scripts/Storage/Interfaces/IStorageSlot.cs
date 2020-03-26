using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IStorageSlot {

    void UpdateSlotDisplay();
    void RemoveSlotDisplay();
    void SetItemQuantity(ItemQuantity itemQuantity);
    void SetQuantity(int amount);
}
