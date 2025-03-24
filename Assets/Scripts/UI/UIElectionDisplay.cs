using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEditor;

public class UIElectionDisplay : MonoBehaviour
{
    public Party selectedParty;
    public Button OkButton;
    public IntVariable currentYear;
    public Sector society;

    private CanvasGroup canvasGroup;
    private ToggleGroup toggleGroup;

    private void Awake()
    {
        UIPartyVote.PartySelected += OnPartySelected;
        TurnController.DisplayActionSelectionScreen += CheckForElection;
        canvasGroup = GetComponent<CanvasGroup>();
        toggleGroup = GetComponent<ToggleGroup>();
    }

    private void OnDestroy()
    {
        UIPartyVote.PartySelected -= OnPartySelected;
        TurnController.DisplayActionSelectionScreen -= CheckForElection;
    }

    private void DisplayElectionPanel()
    {
        selectedParty = Party.None;
        OkButton.interactable = false;
        toggleGroup.SetAllTogglesOff();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPartySelected(Party party)
    {
        selectedParty = party;
        OkButton.interactable = true;
    }

    private void CheckForElection(Sector sector)
    {
        if ( currentYear.Value == 2024 || currentYear.Value == 2028)
        {
            if ( sector == society)
            {
                DisplayElectionPanel();
            }
        }
    }
}