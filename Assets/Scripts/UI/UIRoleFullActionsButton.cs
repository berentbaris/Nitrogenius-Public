using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIRoleFullActionsButton : MonoBehaviour
{
    public static event Action<Sector> FullListRoleSelect;
    private Sector selection;
    public Image bgCircle;
    public Image icon;
    private Color originalColour;
    private float fadetime = 0.2f;
    private UIChoiceFullList fullChoicelist;

    private void Awake()
    {
        FullListRoleSelect += OnSelectionChange;
        originalColour = icon.color;
    }

    private void OnDestroy()
    {
        FullListRoleSelect -= OnSelectionChange;
    }

    public void OnCreateButton(UIChoiceFullList list, Sector belongingSector)
    {
        fullChoicelist = list;
        selection = belongingSector;
        bgCircle.color = belongingSector.color.Value;
        icon.sprite = belongingSector.Icon;
    }


    private void OnSelectionChange(Sector sector)
    {
        if (sector != selection)
        {
            icon.DOColor(originalColour, fadetime);
            this.transform.DOScale(new Vector3(1f, 1f, 1), fadetime).SetEase(Ease.OutElastic);
        }
    }

    public void OnSelect()
    {
        fullChoicelist.PopulateList(selection);
        FullListRoleSelect(selection);
        icon.DOColor(Color.white, fadetime);
        this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), fadetime).SetEase(Ease.OutElastic);
    }
}