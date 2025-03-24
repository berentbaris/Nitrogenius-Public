using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRemainingActionsDisplay : MonoBehaviour
{
    public List<Image> circleImages = new List<Image>();
    public Sprite circleSprite;
    public Sprite checkSprite;
    public Vector2 tooltipPosition;
    public Color selectedColor;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += OnDisplayActionSelectionScreen;
        UIChoice.ChoiceSelected += OnChoiceMade;
        UIChoice.ChoiceUnselected += OnChoiceMade;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= OnDisplayActionSelectionScreen;
        UIChoice.ChoiceSelected -= OnChoiceMade;
        UIChoice.ChoiceUnselected -= OnChoiceMade;
    }

    private void OnDisplayActionSelectionScreen(Sector sector)
    {
        RefreshDisplay(sector);
    }

    private void OnChoiceMade(Action action)
    {
        RefreshDisplay(action._Belonging_Sector);
    }

    private void RefreshDisplay(Sector sector)
    {
        for (int i = 0; i < circleImages.Count; i++)
        {
            if (i < sector.Action_Limit_Per_Turn)
            {
                circleImages[i].sprite = circleSprite;
                circleImages[i].color = selectedColor;
                //circleImages[i].color = Color.white;
            }
            else
            {
                circleImages[i].sprite = checkSprite;
                circleImages[i].color = selectedColor;
            }
        }
    }

    public void ResetSprites()
    {
        foreach (Image image in circleImages)
        {
            image.sprite = circleSprite;
            image.color = Color.white;
        }
    }
}