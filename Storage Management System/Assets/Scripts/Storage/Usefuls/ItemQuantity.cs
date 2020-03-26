using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuantity : MonoBehaviour
{
    private ItemBase _item; public ItemBase Item { get { return _item; } set { _item = value; } }
    private int _quantity; public int Quantity { get { return _quantity; } set { _quantity = value; } }

    public ItemQuantity(ItemBase item, int quantity) {
        _item = item;
        _quantity = quantity;
    }
}
