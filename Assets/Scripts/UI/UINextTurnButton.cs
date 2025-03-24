using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableEvents.Events;

public class UINextTurnButton : MonoBehaviour
{
    public IntVariable turn;
    public IntVariable gamelength;
    public IntVariable currentYear;
    public IntVariable gameMode;
    public BoolVariable NationalReviewEnabled;
    public SimpleScriptableEvent localPlayerSubmittedNextTurn;

    public UnityEvent NextTurnEvent;
    public UnityEvent GameEndEvent;
    public UnityEvent NationalReviewEvent;

    public void OnNextTurnButtonPress()
    {
        if (turn.Value >= gamelength.Value - 1)
        {
            GameEndEvent.Invoke();
        }

        if (NationalReviewEnabled.Value == true)
        {
            if (currentYear.Value == 2024 || currentYear.Value == 2029 || currentYear.Value == 2034)
            {
                NationalReviewEvent.Invoke();
                return;
            }
        }

        switch (gameMode.Value)
        {
            case 0:
                NextTurnEvent.Invoke();
                break;
            case 1:
                NextTurnEvent.Invoke();
                break;
            case 2:
                localPlayerSubmittedNextTurn.Raise();
                break;
        }
    }
}