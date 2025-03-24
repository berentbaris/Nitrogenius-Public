using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static UnityEditor.PlayerSettings;

public class UIScoreBreakdownElement : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI extraText;
    public Image iconImage;
    private UITooltip tooltip;
    public float width;

    private void Awake()
    {
        tooltip = GetComponent<UITooltip>();
    }

    public void FillInValues(ScoreBreakdown element, string number, int current, int initial, string extra = null)
    {
        nameText.text = element.elementName;
        numberText.text = number;
        iconImage.sprite = element.icon;
        iconImage.enabled = true;
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, element.height);


        if (extra != null)
        {
            extraText.text = extra;
            extraText.enabled = true;
        }
        else
        {
            extraText.enabled = false;
        }

        extraText.text = extraText.text.Replace("@", "" + Environment.NewLine);
        tooltip.Content = "Initial: " + initial + "<br>" + "Current: " + current;
        tooltip.Header = element.elementName + " Score:";
        tooltip.tooltipPosition = GetComponent<RectTransform>().position;
    }
}