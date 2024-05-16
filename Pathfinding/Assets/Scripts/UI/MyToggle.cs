using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyToggle : Toggle
{
    public override void OnSubmit(BaseEventData eventData)
    {
        if (isOn) return;
        base.OnSubmit(eventData);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isOn) return;
        base.OnPointerClick(eventData);
    }
}
