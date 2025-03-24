using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    public ProductData prodDataInitial;
    public ProductData prodDataGame;

    public NationalData natDataInitial;
    public NationalData natDataGame;

    public ActionData actionDataInitial;
    public ActionData actionDataGame;

    public ActivityData activityDataInitial;
    public ActivityData activityDataGame;

    public DepositionData depositionDataInitial;
    public DepositionData depositionDataGame;

    public SectorList sectorList;
    public SourceList sourceList;
    public EffectData effectData;
    public IntVariable turn;
    public IntVariable currentYear;

    public BoolVariable TutoriaEnabledBool;
    public BoolVariable AiEnabledBool;
    public BoolVariable EventsEnabledBool;
    public ActionTest ActionTest;
    public ActionTestPrint ActionTestPrint;

    public void Start()
    {
        TutoriaEnabledBool.SetValue(false);
        AiEnabledBool.SetValue(true);
        EventsEnabledBool.SetValue(true);

        // sector data initialize
        foreach (Sector sector in sectorList.list)
        {
            sector.ClearRecords();
            sector.SelectedChoices.Clear();
            sector.Action_Limit_Per_Turn = 3;
            sector.controllerAgent = Controller.None;
            sector.Display_Budget = 0;
            sector.Budget = 0;
            sector.Image = 10;
            sector.N2O_Emissions = 0;
            sector.NH3_Emissions = 0;
            sector.Nox_Emissions = 0;
            sector.Total_Production_Value = 0;
            sector.prodScoreInitial = 0;
            sector.imageScoreInitial = 0;
            sector.emissionScoreInitial = 0;
            sector.healthScoreInitial = 0;
            sector.enviroScoreInitial = 0;
            sector.economyScoreInitial = 0;
        }
        sectorList.list[0].score = 50;
        sectorList.list[1].score = 50;
        sectorList.list[2].score = 50;
        sectorList.list[3].score = 50;
        // source data intialize
        foreach (Source source in sourceList.list)
        {
            source.NH3_Emissions = 0;
            source.Nox_Emissions = 0;
        }
        // product data initialize
        prodDataGame._ProductItems.Clear();
        for (int i = 0; i < prodDataInitial._ProductItems.Count; i++)
        {
            prodDataGame._ProductItems.Add(new Product());
            Product prodGame = prodDataGame._ProductItems[i];
            Product prodInit = prodDataInitial._ProductItems[i];
            prodGame._ID = prodInit._ID;
            prodGame._Name = prodInit._Name;
            prodGame._Unit = prodInit._Unit;
            prodGame._Emission_N2O = prodInit._Emission_N2O;
            prodGame._Emission_NH3 = prodInit._Emission_NH3 * 0.89f;
            prodGame._Emission_NO3 = prodInit._Emission_NO3;
            prodGame._Emission_NOx = prodInit._Emission_NOx * 0.89f;
            prodGame._Price_Per_Unit = prodInit._Price_Per_Unit;
        }

        // national data initialize
        natDataGame._NationalItem._Population = natDataInitial._NationalItem._Population;
        natDataGame._NationalItem._Population_Growth_Rate = natDataInitial._NationalItem._Population_Growth_Rate;
        natDataGame._NationalItem._GDP = natDataInitial._NationalItem._GDP;
        natDataGame._NationalItem._GDP_Per_Capita = 54905.29f;
        natDataGame._NationalItem._Unemployment = 94.86966f;
        natDataGame._NationalItem._Happiness = 50;
        natDataGame._NationalItem._Total_Value = 0;
        natDataGame._NationalItem._National_N2O_Emissions = 0;
        natDataGame._NationalItem._National_NH3_Emissions = 0;
        natDataGame._NationalItem._National_Nox_Emissions = 0;
        natDataGame._NationalItem._N_Imports = natDataInitial._NationalItem._N_Imports;
        natDataGame._NationalItem._N_Exports = natDataInitial._NationalItem._N_Exports;
        natDataGame._NationalItem._N2000_Below_Critical = 0;
        natDataGame._NationalItem._Happiness_Adjuster = 0;
        natDataGame._NationalItem._Economic_Factor = 19.674f;
        natDataGame._NationalItem._Health_Factor = 15.18878f;
        natDataGame._NationalItem._Environment_Factor = 15.42642f;
        natDataGame._NationalItem._Air_Quality = 5.030575f;
        natDataGame._NationalItem._Water_Quality = 7.373854f;
        natDataGame._NationalItem._Stress = 2.784355f;
        natDataGame._NationalItem._Climate_Action = 7.666024f;
        natDataGame._NationalItem._Biodiversity = 7.760395f;
        natDataGame._NationalItem._National_N2O_Emissions = 41890.49f;


        // action data initialize
        actionDataGame._ActionItems.Clear();
        for (int i = 0; i < actionDataInitial._ActionItems.Count; i++)
        {
            actionDataGame._ActionItems.Add(new Action());
            Action actionGame = actionDataGame._ActionItems[i];
            Action actionInit = actionDataInitial._ActionItems[i];
            actionGame._Action_ID = actionInit._Action_ID;
            actionGame._Name = actionInit._Name;
            actionGame._Belonging_Sector_ID = actionInit._Belonging_Sector_ID;
            actionGame._Belonging_Sector = actionInit._Belonging_Sector;
            actionGame._Priority = actionInit._Priority + 3;
            actionGame._Description = actionInit._Description;
            actionGame._Repetivity = actionInit._Repetivity;
            actionGame._Cost = actionInit._Cost;
            actionGame._Importance = actionInit._Importance;
            actionGame._NH3 = actionInit._NH3;
            actionGame._Nox = actionInit._Nox;
            actionGame._N2O = actionInit._N2O;
            actionGame._Image = actionInit._Image;
            actionGame._Economy = actionInit._Economy;
            actionGame._Production = actionInit._Production;
            actionGame._Health = actionInit._Health;
            actionGame._Environment = actionInit._Environment;
            actionGame._Emissions = actionInit._Emissions;
            actionGame._Effect_IDs = actionInit._Effect_IDs;
            actionGame._Effects = actionInit._Effects;
            actionGame._Image_Ref = actionInit._Image_Ref;
        }

        // activity data initialize
        activityDataGame._ActivityItems.Clear();
        for (int i = 0; i < activityDataInitial._ActivityItems.Count; i++)
        {
            activityDataGame._ActivityItems.Add(new Activity());
            Activity activityGame = activityDataGame._ActivityItems[i];
            Activity activityInit = activityDataInitial._ActivityItems[i];
            activityGame._ID = activityInit._ID;
            activityGame._Name = activityInit._Name;
            activityGame._Belonging_Sector_ID = activityInit._Belonging_Sector_ID;
            activityGame._Belonging_Sector = activityInit._Belonging_Sector;
            activityGame._Belonging_Emission_Sector_ID = activityInit._Belonging_Emission_Sector_ID;
            activityGame._Belonging_Emission_Sector = activityInit._Belonging_Emission_Sector;
            activityGame._Produced_Product_ID = activityInit._Produced_Product_ID;
            activityGame._Produced_Product = activityInit._Produced_Product;
            activityGame._Product_Volume = activityInit._Product_Volume;
            activityGame._Unit = activityInit._Unit;
            activityGame._Production_Value = activityInit._Production_Value;
            activityGame._Subsidy_Per_Unit = activityInit._Subsidy_Per_Unit;
            activityGame._Fixed_Subsidy = activityInit._Fixed_Subsidy;
            activityGame._Fixed_Tax = activityInit._Fixed_Tax;
            activityGame._Quotum_NOX = activityInit._Quotum_NOX;
            activityGame._Quotum_NH3 = activityInit._Quotum_NH3;
            activityGame._Quotum_N2O = activityInit._Quotum_N2O;
            activityGame._Quotum_N_Total = activityInit._Quotum_N_Total;
            activityGame._Quotum_PM = activityInit._Quotum_PM;
            activityGame._Quotum_VOC = activityInit._Quotum_VOC;
            activityGame._Quotum_SO2 = activityInit._Quotum_SO2;
            activityGame._Eco_Tax = activityInit._Eco_Tax;
            activityGame._Budget_Factor = activityInit._Budget_Factor;
            activityGame._Tax_Per_Unit_Factor = activityInit._Tax_Per_Unit_Factor;
            activityGame._Fuel_Type_ID = activityInit._Fuel_Type_ID;
            activityGame._Fuel_Type = activityInit._Fuel_Type;
            activityGame._Fuel_Per_Unit = activityInit._Fuel_Per_Unit;
            activityGame._Fuel_Unit = activityInit._Fuel_Unit;
            activityGame._Electricity_Type_ID = activityInit._Electricity_Type_ID;
            activityGame._Electricity_Type = activityInit._Electricity_Type;
            activityGame._Electricity_Per_Unit = activityInit._Electricity_Per_Unit;
            activityGame._Fac_Cost_EM = activityInit._Fac_Cost_EM;
            activityGame._Fac_Cost_Build = activityInit._Fac_Cost_Build;
            activityGame._Raw_Mats_Type_ID = activityInit._Raw_Mats_Type_ID;
            activityGame._Raw_Mats_Type = activityInit._Raw_Mats_Type;
            activityGame._Raw_Mats_Per_Unit = activityInit._Raw_Mats_Per_Unit;
            activityGame._Raw_Mats_Unit = activityInit._Raw_Mats_Unit;
            activityGame._Other_Mats_Type_ID = activityInit._Other_Mats_Type_ID;
            activityGame._Other_Mats_Type = activityInit._Other_Mats_Type;
            activityGame._Other_Mats_Per_Unit = activityInit._Other_Mats_Per_Unit;
            activityGame._Other_Mats_Unit = activityInit._Other_Mats_Unit;
            activityGame._Jobs_Per_Unit = activityInit._Jobs_Per_Unit;
            activityGame._Salary_Per_Job = activityInit._Salary_Per_Job;
            activityGame._Jobs_Total = activityInit._Jobs_Total;
            activityGame._Tax_Per_Unit = activityInit._Tax_Per_Unit;
            activityGame._Fuel_Cost = activityInit._Fuel_Cost;
            activityGame._Electricity_Cost = activityInit._Electricity_Cost;
            activityGame._Facility_Cost = activityInit._Facility_Cost;
            activityGame._Raw_Mats_Cost = activityInit._Raw_Mats_Cost;
            activityGame._Other_Mats_Cost = activityInit._Other_Mats_Cost;
            activityGame._Personnel_Cost = activityInit._Personnel_Cost;
            activityGame._Revenue = activityInit._Revenue;
            activityGame._Costs = activityInit._Costs;
            activityGame._Saldo = activityInit._Saldo;
            activityGame._Budget = activityInit._Budget;
            activityGame._N2O_Emissions = activityInit._N2O_Emissions;
            activityGame._NH3_Emissions = activityInit._NH3_Emissions;
            activityGame._Nox_Emissions = activityInit._Nox_Emissions;
        }

        // deposition data initialize
        depositionDataGame._DepositionItems.Clear();
        for (int i = 0; i < depositionDataInitial._DepositionItems.Count; i++)
        {
            depositionDataGame._DepositionItems.Add(new Deposition());
            Deposition depositionGame = depositionDataGame._DepositionItems[i];
            Deposition depositionInit = depositionDataInitial._DepositionItems[i];
            depositionGame._Cell_ID = depositionInit._Cell_ID;
            depositionGame._X = depositionInit._X;
            depositionGame._Y = depositionInit._Y;
            depositionGame._NL = depositionInit._NL;
            depositionGame._Natura2000 = depositionInit._Natura2000;
            depositionGame._NH3Agr = depositionInit._NH3Agr;
            depositionGame._NH3Dom = depositionInit._NH3Dom;
            depositionGame._NH3Ind = depositionInit._NH3Ind;
            depositionGame._NH3Traffic = depositionInit._NH3Traffic;
            depositionGame._NoxAgr = depositionInit._NoxAgr;
            depositionGame._NoxDom = depositionInit._NoxDom;
            depositionGame._NoxIndHigh = depositionInit._NoxIndHigh;
            depositionGame._NoxIndLow = depositionInit._NoxIndLow;
            depositionGame._NoxTraffic = depositionInit._NoxTraffic;
            depositionGame._NH3_Emissions = depositionInit._NH3_Emissions;
            depositionGame._NOx_High_Emissions = depositionInit._NOx_High_Emissions;
            depositionGame._NOx_Low_Emissions = depositionInit._NOx_Low_Emissions;
            depositionGame._Critical_Deposition = depositionInit._Critical_Deposition;
        }

        turn.SetValue(0);
        currentYear.SetValue(2025);
        effectData.OnGameStart();
        activityDataGame.OnGameStart();
        actionDataGame.OnGameStart();
        depositionDataGame.OnGameStart();

        natDataGame.ClearRecords();
        natDataGame.CalculateN2000Percentage();

        foreach (Sector sector in sectorList.list)
        {
            sector.RecordPreviousTurnValues();
        }
        
        //ActionTest.OnGameStart2();
        //ActionTestPrint.OnGameStart3();
    }
    public void Start2()
    {
        // sector data initialize
        foreach (Sector sector in sectorList.list)
        {
            sector.ClearRecords();
            sector.SelectedChoices.Clear();
            sector.Action_Limit_Per_Turn = 3;
            sector.controllerAgent = Controller.None;
            sector.Display_Budget = 0;
            sector.Budget = 0;
            sector.Image = 10;
            sector.N2O_Emissions = 0;
            sector.NH3_Emissions = 0;
            sector.Nox_Emissions = 0;
            sector.Total_Production_Value = 0;
        }
        sectorList.list[0].score = 50;
        sectorList.list[1].score = 50;
        sectorList.list[2].score = 50;
        sectorList.list[3].score = 50;
        // source data intialize
        foreach (Source source in sourceList.list)
        {
            source.NH3_Emissions = 0;
            source.Nox_Emissions = 0;
        }
        // product data initialize
        prodDataGame._ProductItems.Clear();
        for (int i = 0; i < prodDataInitial._ProductItems.Count; i++)
        {
            prodDataGame._ProductItems.Add(new Product());
            Product prodGame = prodDataGame._ProductItems[i];
            Product prodInit = prodDataInitial._ProductItems[i];
            prodGame._ID = prodInit._ID;
            prodGame._Name = prodInit._Name;
            prodGame._Unit = prodInit._Unit;
            prodGame._Emission_N2O = prodInit._Emission_N2O;
            prodGame._Emission_NH3 = prodInit._Emission_NH3 * 0.89f;
            prodGame._Emission_NO3 = prodInit._Emission_NO3;
            prodGame._Emission_NOx = prodInit._Emission_NOx * 0.89f;
            prodGame._Price_Per_Unit = prodInit._Price_Per_Unit;
        }

        // national data initialize
        natDataGame._NationalItem._Population = natDataInitial._NationalItem._Population;
        natDataGame._NationalItem._Population_Growth_Rate = natDataInitial._NationalItem._Population_Growth_Rate;
        natDataGame._NationalItem._GDP = natDataInitial._NationalItem._GDP;
        natDataGame._NationalItem._GDP_Per_Capita = 54900;
        natDataGame._NationalItem._Unemployment = 94.87f;
        natDataGame._NationalItem._Happiness = 50;
        natDataGame._NationalItem._Total_Value = 0;
        natDataGame._NationalItem._National_N2O_Emissions = 0;
        natDataGame._NationalItem._National_NH3_Emissions = 0;
        natDataGame._NationalItem._National_Nox_Emissions = 0;
        natDataGame._NationalItem._N_Imports = natDataInitial._NationalItem._N_Imports;
        natDataGame._NationalItem._N_Exports = natDataInitial._NationalItem._N_Exports;
        natDataGame._NationalItem._N2000_Below_Critical = 0;
        natDataGame._NationalItem._Happiness_Adjuster = 0;
        natDataGame._NationalItem._Economic_Factor = 20;
        natDataGame._NationalItem._Health_Factor = 15;
        natDataGame._NationalItem._Environment_Factor = 15;
        natDataGame._NationalItem._Air_Quality = 4.72f;
        natDataGame._NationalItem._Water_Quality = 7.72f;
        natDataGame._NationalItem._Stress = 2.78f;
        natDataGame._NationalItem._Climate_Action = 7.66f;
        natDataGame._NationalItem._Biodiversity = 7.29f;
        natDataGame._NationalItem._National_N2O_Emissions = 41890;


        // action data initialize
        /*actionDataGame._ActionItems.Clear();
        for (int i = 0; i < actionDataInitial._ActionItems.Count; i++)
        {
            actionDataGame._ActionItems.Add(new Action());
            Action actionGame = actionDataGame._ActionItems[i];
            Action actionInit = actionDataInitial._ActionItems[i];
            actionGame._Action_ID = actionInit._Action_ID;
            actionGame._Name = actionInit._Name;
            actionGame._Belonging_Sector_ID = actionInit._Belonging_Sector_ID;
            actionGame._Belonging_Sector = actionInit._Belonging_Sector;
            actionGame._Priority = actionInit._Priority;
            actionGame._Description = actionInit._Description;
            actionGame._Repetivity = actionInit._Repetivity;
            actionGame._Cost = actionInit._Cost;
            actionGame._Importance = actionInit._Importance;
            actionGame._NH3 = actionInit._NH3;
            actionGame._Nox = actionInit._Nox;
            actionGame._N2O = actionInit._N2O;
            actionGame._Image = actionInit._Image;
            actionGame._Effect_IDs = actionInit._Effect_IDs;
            actionGame._Effects = actionInit._Effects;
            actionGame._Image_Ref = actionInit._Image_Ref;
        }*/

        // activity data initialize
        activityDataGame._ActivityItems.Clear();
        for (int i = 0; i < activityDataInitial._ActivityItems.Count; i++)
        {
            activityDataGame._ActivityItems.Add(new Activity());
            Activity activityGame = activityDataGame._ActivityItems[i];
            Activity activityInit = activityDataInitial._ActivityItems[i];
            activityGame._ID = activityInit._ID;
            activityGame._Name = activityInit._Name;
            activityGame._Belonging_Sector_ID = activityInit._Belonging_Sector_ID;
            activityGame._Belonging_Sector = activityInit._Belonging_Sector;
            activityGame._Belonging_Emission_Sector_ID = activityInit._Belonging_Emission_Sector_ID;
            activityGame._Belonging_Emission_Sector = activityInit._Belonging_Emission_Sector;
            activityGame._Produced_Product_ID = activityInit._Produced_Product_ID;
            activityGame._Produced_Product = activityInit._Produced_Product;
            activityGame._Product_Volume = activityInit._Product_Volume;
            activityGame._Unit = activityInit._Unit;
            activityGame._Production_Value = activityInit._Production_Value;
            activityGame._Subsidy_Per_Unit = activityInit._Subsidy_Per_Unit;
            activityGame._Fixed_Subsidy = activityInit._Fixed_Subsidy;
            activityGame._Fixed_Tax = activityInit._Fixed_Tax;
            activityGame._Quotum_NOX = activityInit._Quotum_NOX;
            activityGame._Quotum_NH3 = activityInit._Quotum_NH3;
            activityGame._Quotum_N2O = activityInit._Quotum_N2O;
            activityGame._Quotum_N_Total = activityInit._Quotum_N_Total;
            activityGame._Quotum_PM = activityInit._Quotum_PM;
            activityGame._Quotum_VOC = activityInit._Quotum_VOC;
            activityGame._Quotum_SO2 = activityInit._Quotum_SO2;
            activityGame._Eco_Tax = activityInit._Eco_Tax;
            activityGame._Budget_Factor = activityInit._Budget_Factor;
            activityGame._Tax_Per_Unit_Factor = activityInit._Tax_Per_Unit_Factor;
            activityGame._Fuel_Type_ID = activityInit._Fuel_Type_ID;
            activityGame._Fuel_Type = activityInit._Fuel_Type;
            activityGame._Fuel_Per_Unit = activityInit._Fuel_Per_Unit;
            activityGame._Fuel_Unit = activityInit._Fuel_Unit;
            activityGame._Electricity_Type_ID = activityInit._Electricity_Type_ID;
            activityGame._Electricity_Type = activityInit._Electricity_Type;
            activityGame._Electricity_Per_Unit = activityInit._Electricity_Per_Unit;
            activityGame._Fac_Cost_EM = activityInit._Fac_Cost_EM;
            activityGame._Fac_Cost_Build = activityInit._Fac_Cost_Build;
            activityGame._Raw_Mats_Type_ID = activityInit._Raw_Mats_Type_ID;
            activityGame._Raw_Mats_Type = activityInit._Raw_Mats_Type;
            activityGame._Raw_Mats_Per_Unit = activityInit._Raw_Mats_Per_Unit;
            activityGame._Raw_Mats_Unit = activityInit._Raw_Mats_Unit;
            activityGame._Other_Mats_Type_ID = activityInit._Other_Mats_Type_ID;
            activityGame._Other_Mats_Type = activityInit._Other_Mats_Type;
            activityGame._Other_Mats_Per_Unit = activityInit._Other_Mats_Per_Unit;
            activityGame._Other_Mats_Unit = activityInit._Other_Mats_Unit;
            activityGame._Jobs_Per_Unit = activityInit._Jobs_Per_Unit;
            activityGame._Salary_Per_Job = activityInit._Salary_Per_Job;
            activityGame._Jobs_Total = activityInit._Jobs_Total;
            activityGame._Tax_Per_Unit = activityInit._Tax_Per_Unit;
            activityGame._Fuel_Cost = activityInit._Fuel_Cost;
            activityGame._Electricity_Cost = activityInit._Electricity_Cost;
            activityGame._Facility_Cost = activityInit._Facility_Cost;
            activityGame._Raw_Mats_Cost = activityInit._Raw_Mats_Cost;
            activityGame._Other_Mats_Cost = activityInit._Other_Mats_Cost;
            activityGame._Personnel_Cost = activityInit._Personnel_Cost;
            activityGame._Revenue = activityInit._Revenue;
            activityGame._Costs = activityInit._Costs;
            activityGame._Saldo = activityInit._Saldo;
            activityGame._Budget = activityInit._Budget;
            activityGame._N2O_Emissions = activityInit._N2O_Emissions;
            activityGame._NH3_Emissions = activityInit._NH3_Emissions;
            activityGame._Nox_Emissions = activityInit._Nox_Emissions;
        }

        // deposition data initialize
        depositionDataGame._DepositionItems.Clear();
        for (int i = 0; i < depositionDataInitial._DepositionItems.Count; i++)
        {
            depositionDataGame._DepositionItems.Add(new Deposition());
            Deposition depositionGame = depositionDataGame._DepositionItems[i];
            Deposition depositionInit = depositionDataInitial._DepositionItems[i];
            depositionGame._Cell_ID = depositionInit._Cell_ID;
            depositionGame._X = depositionInit._X;
            depositionGame._Y = depositionInit._Y;
            depositionGame._NL = depositionInit._NL;
            depositionGame._Natura2000 = depositionInit._Natura2000;
            depositionGame._NH3Agr = depositionInit._NH3Agr;
            depositionGame._NH3Dom = depositionInit._NH3Dom;
            depositionGame._NH3Ind = depositionInit._NH3Ind;
            depositionGame._NH3Traffic = depositionInit._NH3Traffic;
            depositionGame._NoxAgr = depositionInit._NoxAgr;
            depositionGame._NoxDom = depositionInit._NoxDom;
            depositionGame._NoxIndHigh = depositionInit._NoxIndHigh;
            depositionGame._NoxIndLow = depositionInit._NoxIndLow;
            depositionGame._NoxTraffic = depositionInit._NoxTraffic;
            depositionGame._NH3_Emissions = depositionInit._NH3_Emissions;
            depositionGame._NOx_High_Emissions = depositionInit._NOx_High_Emissions;
            depositionGame._NOx_Low_Emissions = depositionInit._NOx_Low_Emissions;
            depositionGame._Critical_Deposition = depositionInit._Critical_Deposition;
        }
        turn.SetValue(0);
        currentYear.SetValue(2025);
        effectData.OnGameStart();
        activityDataGame.OnGameStart();
        actionDataGame.OnGameStart();
        depositionDataGame.OnGameStart();

        natDataGame.ClearRecords();
        natDataGame.CalculateN2000Percentage();

        foreach (Sector sector in sectorList.list)
        {
            sector.RecordPreviousTurnValues();
        }
    }
}