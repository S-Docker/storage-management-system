using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Item", fileName = "New Item")]
public class ItemBase : ScriptableObject {
    [SerializeField]
    private int _id; public int Id { get { return _id; } }

    [SerializeField]
    private string _name; public string ItemName { get { return _name; } }

    [SerializeField]
    private string _description; public string Description { get { return _description; } }

    [SerializeField]
    private Sprite _icon; public Sprite Icon { get { return _icon; } }

    [SerializeField]
    [Range(1, 20)]
    private int _maximumStackSize; public int MaximumStackSize { get { return _maximumStackSize; } }
}
