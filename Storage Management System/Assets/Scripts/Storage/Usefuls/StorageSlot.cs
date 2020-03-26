using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageSlot : MonoBehaviour, IStorageSlot {

    ItemQuantity _itemQuantity;
    Image _itemIcon;
    TextMeshProUGUI _quantityText;


    void Start() {
        _itemIcon = GetComponentInChildren<Image>();
        _quantityText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateSlotDisplay() {
        _itemIcon.sprite = _itemQuantity.Item.Icon;
        UpdateQuantityText();
    }

    public void RemoveSlotDisplay() {
        _itemQuantity = null;
        _itemIcon.sprite = null;
        _quantityText.text = "";
    }

    public void SetItemQuantity(ItemQuantity itemQuantity) {
        _itemQuantity = itemQuantity;
        UpdateSlotDisplay();
    }

    public void SetQuantity(int amount) {
        _itemQuantity.Quantity = amount;
        UpdateQuantityText();
    }

    void UpdateQuantityText() {
        _quantityText.text = _itemQuantity.Quantity.ToString();
    }
}
