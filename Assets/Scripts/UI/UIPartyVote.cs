using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPartyVote : MonoBehaviour
{
    public static event Action<Party> PartySelected;
    public Party party;

    public void OnToggleValueChange(bool value)
    {
        if (value)
        {
            PartySelected(party);
        }
    }
}

public enum Party
{
    Conservative,
    Green,
    None
}