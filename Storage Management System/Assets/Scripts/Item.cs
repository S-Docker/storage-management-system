using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Item", fileName = "New Item")]
public class Item : ScriptableObject {
    [SerializeField]
    private int id;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [Range(1, 20)]
    [SerializeField]
    private int maximumStackSize;
}
