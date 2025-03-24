using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIGameEndRoleButton : MonoBehaviour
{
    public static event Action<Sector> UIGameEndRoleButtonSelect;
    private Sector selection;
    public Image bgCircle;
    public Image icon;
    private Color originalColour;
    private float fadetime = 0.2f;
    public string sectorName;

    private TextMeshProUGUI budgetText;
    private TextMeshProUGUI imageText;
    private TextMeshProUGUI NUEText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI healthText;
    private SectorList sectorlist;
    public NationalData natData;

    private void Awake()
    {
        UIGameEndRoleButtonSelect += OnSelectionChange;
        originalColour = icon.color;
    }

    private void OnDestroy()
    {
        UIGameEndRoleButtonSelect -= OnSelectionChange;
    }

    public void OnCreateButton(Sector belongingSector, TextMeshProUGUI budget, TextMeshProUGUI image, TextMeshProUGUI nue, TextMeshProUGUI score, TextMeshProUGUI health, SectorList list)
    {
        selection = belongingSector;
        bgCircle.color = belongingSector.color.Value;
        icon.sprite = belongingSector.Icon;
        budgetText = budget;
        imageText = image;
        NUEText = nue;
        healthText = health;
        scoreText = score;
        sectorlist = list;
        sectorName = "Industry";
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
        if (selection.ID == 0)
        {
            sectorName = "Industry";
        }
        if (selection.ID == 1)
        {
            sectorName = "Agriculture";
        }
        if (selection.ID == 2)
        {
            sectorName = "Society";
        }
        if (selection.ID == 3)
        {
            sectorName = "Government";
        }
        scoreText.text = sectorName + "'s score: " + Mathf.RoundToInt(selection.score);
        //budget text
        if (selection != sectorlist.list[3] && selection != sectorlist.list[2])
        {
            budgetText.gameObject.SetActive(true);

            if (selection.Total_Production_Value == selection.TotalProductionValueRecord[0])
            {
                budgetText.text = "- Your revenue remained the same";
            }
            else if (selection.Total_Production_Value > selection.TotalProductionValueRecord[0])
            {
                float percentage = ((selection.Total_Production_Value - selection.TotalProductionValueRecord[0]) / selection.TotalProductionValueRecord[0]) * 100;
                budgetText.text = "- Your revenue increased by " + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selection.Total_Production_Value < selection.TotalProductionValueRecord[0])
            {
                float percentage = ((selection.TotalProductionValueRecord[0] - selection.Total_Production_Value) / selection.TotalProductionValueRecord[0]) * 100;
                budgetText.text = "- Your revenue decreased by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            budgetText.gameObject.SetActive(false);
        }

        //NUE text
        if (selection == sectorlist.list[1])
        {
            NUEText.gameObject.SetActive(true);

            if (selection.NUE == selection.NUERecord[1])
            {
                NUEText.text = "- Your N Use Efficiency remained the same";
            }
            else if (selection.NUE > selection.NUERecord[1])
            {
                float percentage = ((selection.NUE - selection.NUERecord[1]) / selection.NUERecord[1]) * 100;
                NUEText.text = "- Your N Use Efficiency increased by " + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selection.NUE < selection.NUERecord[1])
            {
                float percentage = ((selection.NUERecord[1] - selection.NUE) / selection.NUERecord[1]) * 100;
                NUEText.text = "- Your N Use Efficiency decreased by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            NUEText.gameObject.SetActive(false);
        }

        //image text;
        if (selection != sectorlist.list[2])
        {
            imageText.gameObject.SetActive(true);
            if (selection.Image > 10.49f)
            {
                imageText.text = "- Your role's image increased";
            }
            if (selection.Image < 9.5f)
            {
                imageText.text = "- Your role's image decreased";
            }
            if (selection.Image < 10.5f && selection.Image > 9.49f)
            {
                imageText.text = "- Your role's image stayed the same";
            }
        }
        else
        {
            imageText.gameObject.SetActive(false);
        }

        //health text
        if (selection == sectorlist.list[2])
        {
            healthText.gameObject.SetActive(true);
            if (selection.public_health >= 20)
            {
                float percentage = ((selection.public_health - 20) / 40) * 100;
                healthText.text = "- Public Health increased by " + Mathf.RoundToInt(percentage) + "%";
            }
            else
            {
                float percentage = ((20 - selection.public_health) / 40) * 100;
                healthText.text = "- Public Health decreased by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            healthText.gameObject.SetActive(false);
        }

        UIGameEndRoleButtonSelect(selection);
        icon.DOColor(Color.white, fadetime);
        this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), fadetime).SetEase(Ease.OutElastic);
    }
}