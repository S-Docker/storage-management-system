using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

delegate void InputHandler(BaseEventData data, EventTriggerType type, StorageView view);

public class StorageController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }



    public static void InputReceived(BaseEventData data, EventTriggerType type, StorageView view) {
        PointerEventData newdata = (PointerEventData)data;
        Debug.Log(newdata.button);
        Debug.Log(Input.GetMouseButtonDown(0));
        Debug.Log(view);
        Debug.Log(type == EventTriggerType.PointerDown);
    }

    public static void OnClick(BaseEventData data, EventTriggerType type, StorageView view) {
        Debug.Log(Input.GetMouseButtonDown(0));
        Debug.Log(view);
    }

    public static void OnMouseDrag(BaseEventData data, EventTriggerType type, StorageView view) {
        Debug.Log("Drag Start");
    }

    public static void OnMouseDragEnd(BaseEventData data, EventTriggerType type, StorageView view) {
        Debug.Log("Drag end");
    }
}
