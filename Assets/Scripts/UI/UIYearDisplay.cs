using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIYearDisplay : MonoBehaviour
{
    public IntVariable currentYear;
    public TextMeshProUGUI yearText;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Year;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Year;
    }

    public void Display_Year(Sector sector)
    {
        yearText.text = currentYear.Value.ToString();
    }
}