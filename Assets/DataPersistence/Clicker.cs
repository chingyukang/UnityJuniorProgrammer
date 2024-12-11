using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerClickHandler {
    public Action OnClick = null;

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke();
    }
}
