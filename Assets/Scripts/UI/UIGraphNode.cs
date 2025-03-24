using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGraphNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float NodeValue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.ShowDefault("", Vector2.zero, NodeValue.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}