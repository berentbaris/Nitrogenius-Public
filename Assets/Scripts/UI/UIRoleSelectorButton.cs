using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIRoleSelectorButton : MonoBehaviour
{
    public static event Action<Sector> SelectionChange;
    public Sector selectedSector;
    public Image bgCircle;
    public Image icon;
    private Color originalColour;
    private float fadetime = 0.2f;
    public Sound SectorSelectSound;
    private Vector3 originalScale;
    private Vector3 SelectedScale;
    private float scaleAmount = 0.2f;

    private void Awake()
    {
        originalScale = gameObject.transform.localScale;
        SelectedScale = new Vector3(originalScale.x + scaleAmount, originalScale.y + scaleAmount, originalScale.z + scaleAmount);
        SelectionChange += OnSelectionChange;
        originalColour = icon.color;
    }

    private void OnDestroy()
    {
        SelectionChange -= OnSelectionChange;
    }

    public void OnSelect()
    {
        selectedSector.controllerAgent = Controller.Player;
        SelectionChange(selectedSector);
        icon.DOColor(Color.white, fadetime);
        this.transform.DOScale(SelectedScale, fadetime).SetEase(Ease.OutElastic);
        SectorSelectSound.PlaySound();
    }

    private void OnSelectionChange(Sector sector)
    {
        if (sector != selectedSector)
        {
            icon.DOColor(originalColour, fadetime);
            this.transform.DOScale(originalScale, fadetime).SetEase(Ease.OutElastic);
            selectedSector.controllerAgent = Controller.None;
        }
    }
}