using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMultiplayerActionsSubmittedDisplay : MonoBehaviour
{
    public GameObject Agriculture;
    public GameObject Industry;
    public GameObject Government;
    public GameObject Society;

    public void PlayerSubmittedActions(int sectorID)
    {
        switch (sectorID)
        {
            case 0:
                Industry.SetActive(true);
                break;
            case 1:
                Agriculture.SetActive(true);
                break;
            case 2:
                Society.SetActive(true);
                break;
            case 3:
                Government.SetActive(true);
                break;
        }
    }

    public void OnTurnStart()
    {
        Industry.SetActive(false);
        Agriculture.SetActive(false);
        Society.SetActive(false);
        Government.SetActive(false);
    }
}