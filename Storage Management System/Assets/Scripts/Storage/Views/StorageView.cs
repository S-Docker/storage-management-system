using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageView : MonoBehaviour 
    {
    GameObject _storageScreen;
    IStorageSlot[] _inventorySlots;

    public GameObject storageSlotPrefab;

    int _slotHeight, _slotWidth;
    public int _maximumSlots;

    int _slotsPerRow;
    int _maximumSlotsPerRow;
    int _maximumNumOfRows;

    float _xPadding;
    float _yPadding = 5;

    float _storageHeight;

    public void StorageViewConstructor(int maximumSlots) {
        _maximumSlots = maximumSlots;
    }

    public void Start() {
        InitialiseViewSetup();
    }

    public void InitialiseViewSetup() {
        _inventorySlots = new IStorageSlot[_maximumSlots];

        GenerateStorageSlotDisplay();
    }

    void GenerateStorageSlotDisplay() {
        for (int i = 0; i < _maximumSlots; i++) {
            var slot = Instantiate(storageSlotPrefab, Vector3.zero, Quaternion.identity, transform);

            slot.AddComponent<EventTrigger>();

            AddTriggerEvent(slot, EventTriggerType.PointerDown, new InputHandler(StorageController.OnClick));
            AddTriggerEvent(slot, EventTriggerType.BeginDrag, new InputHandler(StorageController.OnMouseDrag));
            AddTriggerEvent(slot, EventTriggerType.EndDrag, new InputHandler(StorageController.OnMouseDragEnd));

            _inventorySlots[i] = slot.GetComponent<IStorageSlot>();
        }
    }

    void UpdateSlotDisplay(int slot) {
        _inventorySlots[slot].UpdateSlotDisplay();
    }

    /// <summary>
    /// Creates a new event trigger entry and adds it to the existing event trigger on the storage slot.
    /// 
    /// delegate InputHandler is taken as parameter and used to call methods inside StorageController that correspond
    /// to the trigger type.
    /// </summary>
    /// <param name="slot">slot to add event to</param>
    /// <param name="type">type of trigger event</param>
    /// <param name="del">delegate method to be called</param>
    void AddTriggerEvent(GameObject slot, EventTriggerType type, InputHandler del) {
        EventTrigger slotTrigger = slot.GetComponent<EventTrigger>();

        EventTrigger.Entry triggerEntry = new EventTrigger.Entry {
            eventID = type
        };

        //UnityAction<BaseEventData> data = new UnityAction<BaseEventData>(StorageController.InputReceived);
        triggerEntry.callback.AddListener((data) => { del(data, type, this); });
        slotTrigger.triggers.Add(triggerEntry);
    }
}
