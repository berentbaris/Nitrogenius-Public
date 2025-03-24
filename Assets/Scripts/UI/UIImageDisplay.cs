using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIImageDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image_icon;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sector SelectedSector;
    private string adjective;
    public Sector societySector;
    public Vector2 tooltipPosition;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Image;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Image;
    }

    public void Display_Image(Sector sector)
    {
        gameObject.SetActive(true);
        SelectedSector = sector;
        if (sector == societySector)
        {
            gameObject.SetActive(false);
            return;
        }

        if (SelectedSector.Image > 9.49f && SelectedSector.Image < 10.5f)
        {
            image_icon.sprite = image3;
            adjective = "OK";
        }
        if (SelectedSector.Image > 8.99f && SelectedSector.Image < 9.5f)
        {
            image_icon.sprite = image2;
            adjective = "BAD";
        }
        if (SelectedSector.Image < 9)
        {
            image_icon.sprite = image1;
            adjective = "TERRIBLE";
        }
        if (SelectedSector.Image > 10.49f && SelectedSector.Image < 11)
        {
            image_icon.sprite = image4;
            adjective = "GOOD";
        }
        if (SelectedSector.Image > 10.99f)
        {
            image_icon.sprite = image5;
            adjective = "GREAT";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.ShowDefault("Your current public image is " + adjective, tooltipPosition, "Public Image");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
