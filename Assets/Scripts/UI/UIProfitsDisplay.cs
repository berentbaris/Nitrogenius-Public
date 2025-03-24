using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIProfitsDisplay : MonoBehaviour
{
    private Sector selectedSector;
    public SectorList sectorList;
    public TextMeshProUGUI profitsText;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Profits;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Profits;
    }

    public void Display_Profits(Sector sector)
    {
        selectedSector = sector;

        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
            profitsText.gameObject.SetActive(true);

            if (selectedSector.Total_Production_Value == selectedSector.TotalProductionValueRecord[0])
            {
                profitsText.text = "Production: no change";
            }
            else if (selectedSector.Total_Production_Value > selectedSector.TotalProductionValueRecord[0])
            {
                float percentage = ((selectedSector.Total_Production_Value - selectedSector.TotalProductionValueRecord[0]) / selectedSector.TotalProductionValueRecord[0]) * 100;
                profitsText.text = "Production: +" + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.Total_Production_Value < selectedSector.TotalProductionValueRecord[0])
            {
                float percentage = ((selectedSector.TotalProductionValueRecord[0] - selectedSector.Total_Production_Value) / selectedSector.TotalProductionValueRecord[0]) * 100;
                profitsText.text = "Production: -" + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            profitsText.gameObject.SetActive(false);
        }
    }
}
