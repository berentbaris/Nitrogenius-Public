using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotseatPanel : MonoBehaviour
{
    public List<TMP_Dropdown> dropdownList = new List<TMP_Dropdown>();
    public IntVariable gamemode;
    public Button startButton;

    public void OpenHotSeatPanel()
    {
        gamemode.SetValue(1);

        foreach (TMP_Dropdown dropdown in dropdownList)
        {
            dropdown.value = 2;
        }
        OnDropDownValueChange();
    }

    public void OnDropDownValueChange()
    {
        startButton.interactable = false;

        int numberOfPlayers = 0;
        foreach (TMP_Dropdown dropdown in dropdownList)
        {
            if (dropdown.value == 0)
            {
                numberOfPlayers++;
            }
        }

        if (numberOfPlayers >= 2)
        {
            startButton.interactable = true;
        }
    }
}