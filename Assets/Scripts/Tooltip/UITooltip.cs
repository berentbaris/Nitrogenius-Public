using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 tooltipPosition;
    public string Content;
    public string Header;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.ShowDefault(Content, tooltipPosition, Header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}