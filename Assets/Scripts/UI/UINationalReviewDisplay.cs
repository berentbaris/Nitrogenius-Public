using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINationalReviewDisplay : MonoBehaviour
{
    public IntVariable currentYear;
    public NationalData natData;
    private int target;
    public TextMeshProUGUI yearDisplayText;
    public TextMeshProUGUI Nat2000TargetText;
    public UIGraph Nat2000Graph;
    public UIGraph GDPPerCapitaGraph;

    public void OnNationalReview()
    {
        yearDisplayText.text = currentYear.Value.ToString();

        Nat2000Graph.DisplayGraph(natData._NationalItem._N2000_Below_Critical_Record, "Nat 2000");
        GDPPerCapitaGraph.DisplayGraph(natData._NationalItem.GDP_Per_Capita_Record, "GDP Per Capita");

        switch (currentYear.Value)
        {
            case 2025:
                target = 40;
                break;

            case 2030:
                target = 50;
                break;

            case 2035:
                target = 74;
                break;
        }

        if (natData._NationalItem._N2000_Below_Critical >= target)
        {
            Nat2000TargetText.text = "Netherlands has reached the Natura 2000 areas below critical load target of" + target + "% for " + currentYear.Value + "!";
        }
        else
        {
            Nat2000TargetText.text = "Netherlands have failed to reach the Natura 2000 areas below critical load target of " + target + "% for " + currentYear.Value + ".";
        }
    }
}