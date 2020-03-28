using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Script is required as scrolling is disabled when hovered over buttons unless a concrete implementation is included.
// This is due to having concrete implementations for other interaction which disables scrolling automatically.
public class AddScrollRectFunctionality : MonoBehaviour, IScrollHandler {

    ScrollRect _scroll;

    void Start() {
        _scroll = GetComponentInParent<ScrollRect>();
    }

    public void OnScroll(PointerEventData eventData) {
        _scroll.OnScroll(eventData);
    }
}
