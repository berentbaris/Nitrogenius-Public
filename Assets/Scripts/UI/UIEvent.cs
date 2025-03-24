using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Action> EventHovered;
    public static event Action<Action> EventUnHovered;

    public Action belongingChoice;
    public TextMeshProUGUI SectorNameText;
    public TextMeshProUGUI ActionNameText;
    public Image IconBg;
    public Image Icon;
    public Sector EventsSector;
    public ColorVariable eventColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventHovered(belongingChoice);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventUnHovered(belongingChoice);
    }

    public void SetChoice(Action action)
    {
        belongingChoice = action;

        IconBg.color = belongingChoice._Belonging_Sector.color.Value;
        Icon.sprite = belongingChoice._Belonging_Sector.Icon;
        ActionNameText.text = belongingChoice._Name;
        ActionNameText.color = Color.white;
        ActionNameText.fontStyle = FontStyles.Normal;

        if (belongingChoice._Belonging_Sector != EventsSector)
        {
            SectorNameText.text = belongingChoice._Belonging_Sector.name + " chose:";
        }
        else
        {
            SectorNameText.text = "Event:";
            ActionNameText.color = eventColor.Value;
        }

        if (action._Importance == true)
        {
            ActionNameText.fontStyle = FontStyles.Bold;
        }
    }
}
