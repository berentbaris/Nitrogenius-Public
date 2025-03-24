using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UICriticalLoadDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI percentageDisplay;
    public NationalData natData;
    public Image barFillImage;
    private float fillImageWidth;
    public Vector2 tooltipPosition;

    [SerializeField]
    BarColorDictionary BCDictionary = new BarColorDictionary();

    private void Awake()
    {
        fillImageWidth = barFillImage.rectTransform.sizeDelta.x;
        TurnController.DisplayActionSelectionScreen += DisplayPercentage;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayPercentage;
    }

    public void DisplayPercentage(Sector sector)
    {
        int roundedPercentage = Mathf.RoundToInt(natData._NationalItem._N2000_Below_Critical);
        percentageDisplay.text = roundedPercentage.ToString() + "%";

        int minValue = 0;
        foreach (KeyValuePair<int, Color> DicEntry in BCDictionary)
        {
            if (roundedPercentage < DicEntry.Key && roundedPercentage >= minValue)
            {
                barFillImage.color = DicEntry.Value;
                break;
            }
            else
            {
                minValue = DicEntry.Key;
            }
        }

        barFillImage.rectTransform.sizeDelta = new Vector2(natData._NationalItem._N2000_Below_Critical / 100 * fillImageWidth, barFillImage.rectTransform.sizeDelta.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TooltipSystem.ShowGraph(natData._NationalItem._N2000_Below_Critical_Record, tooltipPosition, "Nat 2000");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TooltipSystem.Hide();
    }
}

[Serializable]
public class BarColorDictionary : SerializableDictionary<int, Color> { }