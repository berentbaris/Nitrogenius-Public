using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class UIChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Action> ChoiceSelected;
    public static event Action<Action> ChoiceUnselected;
    public static event Action<Action> ChoiceHovered;
    public static event Action<Action> ChoiceUnHovered;

    public Action belongingChoice;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Toggle toggle;
    public Image BgImage;
    public Image circleImage;
    private Color originalColor;
    [SerializeField] private Color greenColor;
    private float fadeDuration = 0.3f;
    public Sound ChoiceSelectedSound;
    public Sound ChoiceUnselectedSound;
    private UIHighlighter highlighter;

    private void Awake()
    {
        ChoiceSelected += OnChoiceSelected;
        ChoiceUnselected += OnChoiceSelected;
        originalColor = BgImage.color;
        highlighter = GetComponent<UIHighlighter>();
    }

    private void OnDestroy()
    {
        ChoiceSelected -= OnChoiceSelected;
        ChoiceUnselected -= OnChoiceSelected;
    }

    private void OnChoiceSelected(Action action)
    {
        RefreshDisplay();
    }

    public void SetChoice(Action action)
    {
        belongingChoice = action;
        nameText.text = belongingChoice._Name;

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
            costText.text = "€ "  + intcost.ToString();
        }

        if (belongingChoice._Cost != belongingChoice._InitialCost)
        {
            float result = belongingChoice._Cost - belongingChoice._InitialCost;
            result /= Mathf.Abs(belongingChoice._InitialCost);
            result *= 100;
            costText.text = "<color=#72B136>" + Mathf.RoundToInt(result) + "% </color>" + costText.text;
        }
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {   
        if (toggle.isOn)
        {
            return;
        }

        else if (belongingChoice._Cost > belongingChoice._Belonging_Sector.Budget || belongingChoice._Belonging_Sector.Action_Limit_Per_Turn == 0)
        {
            toggle.interactable = false;
            nameText.DOColor(Color.gray, fadeDuration);
            costText.DOColor(Color.gray, fadeDuration);
            circleImage.DOColor(Color.gray, fadeDuration);
        }
        else
        {
            toggle.interactable = true;
            nameText.DOColor(Color.white, fadeDuration);
            costText.DOColor(Color.white, fadeDuration);
            circleImage.DOColor(Color.white, fadeDuration);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChoiceHovered(belongingChoice);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChoiceUnHovered(belongingChoice);
    }

    public void MultiplayerFreezeChoices()
    {
        toggle.interactable = false;
        //toggle.isOn = false;
    }

    public void OnToggleClick(bool isOn)
    {
        if (toggle.isOn)
        {
            BgImage.DOColor(greenColor, fadeDuration);
            highlighter.ChangeOriginalColor(greenColor);
            belongingChoice._Belonging_Sector.ChoiceSelected(belongingChoice);
            ChoiceSelected(belongingChoice);
            ChoiceSelectedSound.PlaySound();
            return;
        }

        else if (toggle.isOn == false)
        {
            BgImage.DOColor(originalColor, fadeDuration);
            highlighter.ChangeOriginalColor(originalColor);
            belongingChoice._Belonging_Sector.ChoiceUnselected(belongingChoice);
            ChoiceUnselected(belongingChoice);
            ChoiceUnselectedSound.PlaySound();
            return;
        }
    }
}