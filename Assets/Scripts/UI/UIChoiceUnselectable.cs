using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class UIChoiceUnselectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Action> ChoiceHovered;
    public static event Action<Action> ChoiceUnHovered;

    public Action belongingChoice;
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Image BgImage;
    private Color originalColor;
    [SerializeField] private Color highlightColor;
    public float fadeDuration = 0.1f;

    private void Awake()
    {
        originalColor = BgImage.color;
    }

    public void SetChoice(Action action)
    {
        belongingChoice = action;
        nameText.text = belongingChoice._Name;
        yearText.text = belongingChoice._Priority.ToString();

        if (belongingChoice._Cost == 0)
        {
            costText.text = "";
        }
        else if (belongingChoice._Cost < 0)
        {
            float absoluteValue = Mathf.Abs(belongingChoice._Cost);
            costText.text = "€ +" + absoluteValue;
        }
        else
        {
            int intcost = Mathf.RoundToInt(belongingChoice._Cost);
            costText.text = "€ " + intcost.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChoiceHovered(belongingChoice);
        BgImage.DOColor(highlightColor, fadeDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChoiceUnHovered(belongingChoice);
        BgImage.DOColor(originalColor, fadeDuration);
    }
}