using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI percentageDisplay;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += DisplayScore;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayScore;
    }

    private void DisplayScore(Sector sector)
    {
        percentageDisplay.text = "<size=24> " + Mathf.RoundToInt(sector.score).ToString() + "</size><size=14> / 100</size>";
    }
}
