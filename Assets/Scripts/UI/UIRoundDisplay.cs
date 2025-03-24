using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRoundDisplay : MonoBehaviour
{
    public IntVariable turn;
    public IntVariable currentYear;
    public IntVariable gameLength;
    private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI yearDisplayText;


    void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
    }
    
    public void OnNewTurnStart()
    {
        displayText.text = (turn.Value + 1).ToString() + " of " + gameLength.Value.ToString();
        yearDisplayText.text = currentYear.Value.ToString();
    }
}