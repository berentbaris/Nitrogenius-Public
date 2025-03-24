using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoleDisplay : MonoBehaviour
{
    public Image background;
    public Image icon;
    private Sector currentSector;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += ChangeRoleDisplay;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= ChangeRoleDisplay;
    }

    private void ChangeRoleDisplay(Sector sector)
    {
        background.color = sector.color.Value;
        icon.sprite = sector.Icon;
        currentSector = sector;
        gameObject.GetComponent<UITooltip>().Content = currentSector.Concerns;
        gameObject.GetComponent<UITooltip>().Header = currentSector.name;
    }
}