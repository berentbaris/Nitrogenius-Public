using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreBreakdown : MonoBehaviour
{
    public Sector selection;
    public SectorList sectorlist;
    public NationalData natData;
    public GameObject breakdownElementPrefab;
    private List<GameObject> breakdownElements = new List<GameObject>();

    [Space(10)]
    public ScoreBreakdown production;
    public ScoreBreakdown emissions;
    public ScoreBreakdown image;
    public ScoreBreakdown economy;
    public ScoreBreakdown environment;
    public ScoreBreakdown health;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += DisplayBreakdown;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayBreakdown;
    }

    public void DisplayBreakdown(Sector sector)
    {
        selection = sector;
        ClearList();
        if (selection == sectorlist.list[0])
        {
            CalculateProductionScore();
            CalculateEmissionsInd();
            CalculateImageScore(10);
        }
        if (selection == sectorlist.list[1])
        {
            CalculateProductionScore();
            CalculateEmissionsAgr();
            CalculateImageScore(10);
        }
        if (selection == sectorlist.list[2])
        {
            CalculateEconomy(1);
            CalculateHealth(1);
            CalculateEnvironment(1);
        }
        if (selection == sectorlist.list[3])
        {
            CalculateImageScore(50);
            CalculateEconomy(2);
            CalculateHealth(2);
            CalculateEnvironment(2);
        }
    }

    private void InstantiateElement(ScoreBreakdown element, string score, int current, int initial, string extra = null)
    {
        GameObject obj = Instantiate(breakdownElementPrefab, transform);
        breakdownElements.Add(obj);
        obj.GetComponent<UIScoreBreakdownElement>().FillInValues(element, score, current, initial, extra);
    }

    private void ClearList()
    {
        foreach (GameObject ScoreBreakdownGameobject in breakdownElements)
        {
            Destroy(ScoreBreakdownGameobject); 
        }
        breakdownElements.Clear();
    }

    private void CalculateProductionScore()
    {
        float prodComparison = (((selection.Total_Production_Value - selection.TotalProductionValueRecord[0]) / selection.TotalProductionValueRecord[0]) * 100);

        string prodvalue = Mathf.RoundToInt(prodComparison).ToString();
        if (selection.Total_Production_Value < selection.TotalProductionValueRecord[0])
        {
            prodvalue += "%";
        }
        if (selection.Total_Production_Value > selection.TotalProductionValueRecord[0])
        {
            prodvalue = "+" + prodvalue + "%";
        }
        if (prodComparison > -0.5f && prodComparison < 0.5f)
        {
            prodvalue = "<i>stable</i>";
        }
        int productionScore = Mathf.RoundToInt(((prodComparison + 100) / (100 + 100)) * (80 - 0) + 0);
        if (selection.prodScoreInitial == 0)
        {
            selection.prodScoreInitial = productionScore;
        }
        InstantiateElement(production, productionScore + "</size><size=20> /80", productionScore, Mathf.RoundToInt(selection.prodScoreInitial), " - Production value: " + prodvalue);
    }

    private void CalculateImageScore(int cap)
    {
        string imageText = "";
        if (selection.Image > 9.49f && selection.Image < 10.5f)
        {
            imageText = "<b>OK</b>";
        }
        if (selection.Image > 8.99f && selection.Image < 9.5f)
        {
            imageText = "<b>BAD</b>";
        }
        if (selection.Image < 9)
        {
            imageText = "<b>TERRIBLE</b>";
        }
        if (selection.Image > 10.49f && selection.Image < 11)
        {
            imageText = "<b>GOOD</b>";
        }
        if (selection.Image > 10.99f)
        {
            imageText = "<b>GREAT</b>";
        }
        int publicImageScore = Mathf.RoundToInt((((selection.Image - 9) / (11 - 9)) * (cap - 0) + 0));
        if (publicImageScore > cap)
        {
            publicImageScore = cap;
        }
        if (publicImageScore < 0)
        {
            publicImageScore = 0;
        }
        if (selection.imageScoreInitial == 0)
        {
            selection.imageScoreInitial = publicImageScore;
        }
        InstantiateElement(image, publicImageScore + "</size><size=20> /" + cap, publicImageScore, Mathf.RoundToInt(selection.imageScoreInitial), " - Public image:  " + imageText);
    }

    private void CalculateEmissionsInd()
    {
        float noxComparison = (((selection.Nox_Emissions - selection.NoxRecord[0]) / selection.NoxRecord[0]) * 100);
        float nh3Comparison = (((selection.NH3_Emissions - selection.NH3Record[0]) / selection.NH3Record[0]) * 100);
        float n2oComparison = (((selection.N2O_Emissions - selection.N2ORecord[0]) / selection.N2ORecord[0]) * 100);

        string emitTextNox = Mathf.RoundToInt(noxComparison).ToString();
        if (selection.Nox_Emissions < selection.NoxRecord[0])
        {
            emitTextNox += "%";
        }
        if (selection.Nox_Emissions > selection.NoxRecord[0])
        {
            emitTextNox = "+" + emitTextNox + "%";
        }
        if (noxComparison > -0.5f && noxComparison < 0.5f)
        {
            emitTextNox = "<i>stable</i>";
        }

        string emitTextNH3 = Mathf.RoundToInt(nh3Comparison).ToString();
        if (selection.NH3_Emissions < selection.NH3Record[0])
        {
            emitTextNH3 += "%";
        }
        if (selection.NH3_Emissions > selection.NH3Record[0])
        {
            emitTextNH3 = "+" + emitTextNH3 + "%";
        }
        if (nh3Comparison > -0.5f && nh3Comparison < 0.5f)
        {
            emitTextNH3 = "<i>stable</i>";
        }

        string emitTextN2O = Mathf.RoundToInt(n2oComparison).ToString();
        if (selection.N2O_Emissions < selection.N2ORecord[0])
        {
            emitTextN2O += "%";
        }
        if (selection.N2O_Emissions > selection.N2ORecord[0])
        {
            emitTextN2O = "+" + emitTextN2O + "%";
        }
        if (n2oComparison > -0.5f && n2oComparison < 0.5f)
        {
            emitTextN2O = "<i>stable</i>";
        }
        int emissionsScore = Mathf.RoundToInt(((100 - noxComparison) / (100 + 100)) * (10 - 0) + 0);
        if (emissionsScore > 10)
        {
            emissionsScore = 10;
        }
        if (emissionsScore < 0)
        {
            emissionsScore = 0;
        }
        if (selection.emissionScoreInitial == 0)
        {
            selection.emissionScoreInitial = emissionsScore;
        }
        InstantiateElement(emissions, emissionsScore + "</size><size=20> /10", emissionsScore, Mathf.RoundToInt(selection.emissionScoreInitial), " - NOx emissions:  " + emitTextNox + "@ - NH3 emissions:  " + emitTextNH3 + "@ - N2O emissions:  " + emitTextN2O);
    }

    private void CalculateEmissionsAgr()
    {
        float noxComparison = (((selection.Nox_Emissions - selection.NoxRecord[0]) / selection.NoxRecord[0]) * 100);
        float nh3Comparison = (((selection.NH3_Emissions - selection.NH3Record[0]) / selection.NH3Record[0]) * 100);
        float n2oComparison = (((selection.N2O_Emissions - selection.N2ORecord[0]) / selection.N2ORecord[0]) * 100);

        string emitTextNox = Mathf.RoundToInt(noxComparison).ToString();
        if (selection.Nox_Emissions < selection.NoxRecord[0])
        {
            emitTextNox += "%";
        }
        if (selection.Nox_Emissions > selection.NoxRecord[0])
        {
            emitTextNox = "+" + emitTextNox + "%";
        }
        if (noxComparison > -0.5f && noxComparison < 0.5f)
        {
            emitTextNox = "<i>stable</i>";
        }

        string emitTextNH3 = Mathf.RoundToInt(nh3Comparison).ToString();
        if (selection.NH3_Emissions < selection.NH3Record[0])
        {
            emitTextNH3 += "%";
        }
        if (selection.NH3_Emissions > selection.NH3Record[0])
        {
            emitTextNH3 = "+" + emitTextNH3 + "%";
        }
        if (nh3Comparison > -0.5f && nh3Comparison < 0.5f)
        {
            emitTextNH3 = "<i>stable</i>";
        }

        string emitTextN2O = Mathf.RoundToInt(n2oComparison).ToString();
        if (selection.N2O_Emissions < selection.N2ORecord[0])
        {
            emitTextN2O += "%";
        }
        if (selection.N2O_Emissions > selection.N2ORecord[0])
        {
            emitTextN2O = "+" + emitTextN2O + "%";
        }
        if (n2oComparison > -0.5f && n2oComparison < 0.5f)
        {
            emitTextN2O = "<i>stable</i>";
        }
        int emissionsScore = Mathf.RoundToInt(((100 - nh3Comparison) / (100 + 100)) * (10 - 0) + 0);
        if (emissionsScore > 10)
        {
            emissionsScore = 10;
        }
        if (emissionsScore < 0)
        {
            emissionsScore = 0;
        }
        if (selection.emissionScoreInitial == 0)
        {
            selection.emissionScoreInitial = emissionsScore;
        }
        InstantiateElement(emissions, emissionsScore + "</size><size=20> /10", emissionsScore, Mathf.RoundToInt(selection.emissionScoreInitial), " - NOx emissions:  " + emitTextNox + "@ - NH3 emissions:  " + emitTextNH3 + "@ - N2O emissions:  " + emitTextN2O);
    }

    private void CalculateEconomy(int factor)
    {
        string gdpText = Mathf.RoundToInt(natData._NationalItem._GDP_Per_Capita / 1000).ToString();
        string unemploymentText = Math.Round(100 - natData._NationalItem._Unemployment).ToString();
        int economyScore = Mathf.RoundToInt(natData._NationalItem._Economic_Factor / factor);

        if (selection.economyScoreInitial == 0)
        {
            selection.economyScoreInitial = economyScore;
        }
        InstantiateElement(economy, economyScore + "</size><size=20> /" + 40 / factor, economyScore, Mathf.RoundToInt(selection.economyScoreInitial), " - GDP per capita:  " + gdpText + "k<size=12>@</size> - Unemployment:  " + unemploymentText + "%");
    }

    private void CalculateHealth(int factor)
    {
        int healthScore = Mathf.RoundToInt(natData._NationalItem._Health_Factor / factor);
        string waterqText = Mathf.RoundToInt(natData._NationalItem._Water_Quality / factor).ToString();
        string airqText = Mathf.RoundToInt(natData._NationalItem._Air_Quality / factor).ToString();
        string stressText = Mathf.RoundToInt(natData._NationalItem._Stress / factor).ToString();

        if (selection.healthScoreInitial == 0)
        {
            selection.healthScoreInitial = healthScore;
        }
        InstantiateElement(health, healthScore + "</size><size=20> /" + 30 / factor, healthScore, Mathf.RoundToInt(selection.healthScoreInitial), " - Water quality:  " + waterqText + "</size><size=24> /" + 15f / factor + "@</size> - Air quality:  " + airqText + "</size><size=24> /" + 10 / factor + "@</size> - Stress:  " + stressText + "</size><size=24> /" + 5f / factor);
    }

    private void CalculateEnvironment(int factor)
    {
        int envScore = Mathf.RoundToInt(natData._NationalItem._Environment_Factor / factor);
        string bioText = Mathf.RoundToInt(natData._NationalItem._Biodiversity / factor).ToString();
        string ccText = Mathf.RoundToInt(natData._NationalItem._Climate_Action / factor).ToString();

        if (selection.enviroScoreInitial == 0)
        {
            selection.enviroScoreInitial = envScore;
        }
        InstantiateElement(environment, envScore + "</size><size=20> /" + 30 / factor, envScore, Mathf.RoundToInt(selection.enviroScoreInitial), " - Biodiversity:  " + bioText + "</size><size=24> /" + 15f / factor + "@</size> - Climate action:  " + ccText + "</size><size=24> /" + 15f / factor);
    }

    /*private string ChangeComparison(string initial, float ValueA, float ValueB)
    {
        if (selection.N2O_Emissions < selection.N2ORecord[0])
        {
            initial += "%";
        }
        if (selection.N2O_Emissions > selection.N2ORecord[0])
        {
            initial = "+" + initial + "%";
        }
        if (n2oComparison > -0.5f && n2oComparison < 0.5f)
        {
            initial = "<i>stable</i>";
        }
        return initial;
    }*/
}