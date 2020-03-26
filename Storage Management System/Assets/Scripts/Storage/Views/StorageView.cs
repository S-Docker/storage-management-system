using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    GameObject _storageScreen;
    IStorageSlot[] _inventorySlots;

    public GameObject storageScreenPrefab;
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
        _storageScreen = Instantiate(storageScreenPrefab, transform);

        SetUpStorageContainerSize();
        GenerateStorageSlotDisplay();
    }

    void SetUpStorageContainerSize() {
        _slotHeight = Mathf.CeilToInt(storageSlotPrefab.GetComponent<RectTransform>().rect.height);
        _slotWidth = Mathf.CeilToInt(storageSlotPrefab.GetComponent<RectTransform>().rect.width);

        RectTransform _storageRect = _storageScreen.GetComponent<RectTransform>();
        float _storageWidth = _storageRect.rect.width;

        _maximumSlotsPerRow = GetMaximumSlotsPerRow(_storageWidth, _slotWidth);
        _slotsPerRow = _maximumSlots < _maximumSlotsPerRow ? _maximumSlots : _maximumSlotsPerRow; // will return either number of slots needed or maximum slots allowed per row, whichever is less
        _maximumNumOfRows = GetMaximumNumberOfRows(_maximumSlots);
        _xPadding = CalculateXPadding(_storageWidth, _slotsPerRow, _slotWidth);

        int storageHeightNeeded = GetStorageHeight(_maximumNumOfRows, _slotHeight, _yPadding);

        _storageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, storageHeightNeeded);
    }

    void GenerateStorageSlotDisplay() {
        for (int i = 0; i < _maximumSlots; i++) {
            var slot = Instantiate(storageSlotPrefab, Vector3.zero, Quaternion.identity, _storageScreen.transform);
            Vector3 newPosition = new Vector3(_xPadding + (i % _maximumSlotsPerRow) * (_slotWidth + _xPadding), -(_yPadding + (i / _maximumSlotsPerRow) * (_slotHeight + _yPadding)), 0f);
            slot.GetComponent<RectTransform>().localPosition = newPosition;
            _inventorySlots[i] = slot.GetComponent<IStorageSlot>();
        }
    }

    void UpdateSlotDisplay(int slot) {
        _inventorySlots[slot].UpdateSlotDisplay();
    }

    int GetMaximumSlotsPerRow(float storageWidth, int slotWidth) {
        return Mathf.FloorToInt(storageWidth / slotWidth);
    }

    int GetMaximumNumberOfRows(int maximumSlots) {
        int rows = Mathf.CeilToInt((float)maximumSlots / (float)_maximumSlotsPerRow); // converted to float to stop integer devision automatically rounding down
        return rows;
    }

    int GetStorageHeight(int numOfRows, int slotHeight, float yPadding) {
        return (int)Mathf.Ceil((numOfRows * (slotHeight + yPadding)) + yPadding); // additional yPadding added to pad bottom of storage after all slots
    }

    float CalculateXPadding(float storageWidth, int slotsPerRow, int slotWidth) {
        float totalSpaceAvailable = storageWidth - ((slotsPerRow * slotWidth));
        float spaceBetweenSlots = totalSpaceAvailable / (slotsPerRow + 1);

        return spaceBetweenSlots;
    }
}
