using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTest : MonoBehaviour
{
    public EffectData effectData;
	public SectorList sectorList;
	public ActivityData ActivityData;
	public DepositionData DepositionData;
	public NationalData NationalData;
	public SourceList sourceList;
	public DataInitializer DataInitializer;
    public ActionData ActionData;
    public ActionData actionDataInitial;
	public Dictionary<int, Action> actionDictionary = new Dictionary<int, Action>();
    public int i;

    public void OnGameStart2()
	{
        i = 0;
		foreach (Action item in ActionData._ActionItems)
		{
			DataInitializer.Start2();
			foreach (Sector sector in sectorList.list)
        	{
        	sector.Display_Budget = 0;
        	sector.Budget = sector.Budget / 4;
        	sector.NH3_Emissions = 0;
           	sector.Nox_Emissions = 0;
           	sector.N2O_Emissions = 0;
           	sector.Total_Production_Value = 0;
           	sector.Fertilizer_Input = 0;
           	sector.Fertilizer_Output = 0;
           	sector.NUE = 0;
       		}
       		foreach (Source source in sourceList.list)
       		{
           	source.NH3_Emissions = 0;
           	source.Nox_Emissions = 0;
       		}
       		NationalData._NationalItem._National_Total_Deposition = 0;
       		NationalData._NationalItem._National_NH3_Emissions = 0;
       		NationalData._NationalItem._National_Nox_Emissions = 0;
			foreach (Effect effect in item._Effects)
			{
               effect.ApplyEffect();
			}
			foreach (Activity activity in ActivityData._ActivityItems)
       		{
           		activity.Calculate();
       		}
            ActivityData.CalculateBudget();
			foreach (Effect effect in item._Effects)
            {
            	effect.ApplyEffectAfter();
            }
			ActivityData.CalculateSectorEmissions();
       		ActivityData.CalculateSourceEmissions();
       		DepositionData.LocalizeEmissions();
            if (item._Action_ID == 244)
            {
                DepositionData.CalculateDeposition();
                DepositionData.FixDeposition();
            }
       		DepositionData.DistributeEmissions();
       		NationalData.CalculateNationalDeposition();
            NationalData.CalculateN2000Percentage();
       		NationalData.CalculateHappiness();
			NationalData.CalculateSectorScore();
			item._EconomyF = (NationalData._NationalItem._Economic_Factor - 19.674f) / .25f;
			item._HealthF = (NationalData._NationalItem._Health_Factor - 15.18877f) / .187f;
			item._EnvironmentF = (NationalData._NationalItem._Environment_Factor - 15.42642f) / .3f;
			item._ImageF = (sectorList.list[item._Belonging_Sector_ID].Image - 10) / .1f;
            item._MeanF = ((NationalData._NationalItem._Mean_Nox_Deposition + NationalData._NationalItem._Mean_NH3_Deposition) - 3113.156f);
            item._N2000F = (NationalData._NationalItem._N2000_Below_Critical - 31.55894f);
            item._GDPF = (NationalData._NationalItem._GDP_Per_Capita - 54905.29f);
            if (item._Action_ID == 244 || item._Action_ID == 254 || item._Action_ID == 246 || item._Action_ID == 245 || item._Action_ID == 21 || item._Action_ID == 22)
            {
                Debug.Log(item._Action_ID);
                Debug.Log(item._MeanF);
                Debug.Log(item._GDPF);
                Debug.Log(item._N2000F);
            }
            item._EconomyF = Mathf.Round(item._EconomyF * 100f) / 100f;
            item._HealthF = Mathf.Round(item._HealthF * 100f) / 100f;
            item._EnvironmentF = Mathf.Round(item._EnvironmentF * 100f) / 100f;
            item._ImageF = Mathf.Round(item._ImageF * 100f) / 100f;
            item._MeanF = Mathf.Round(item._MeanF * 100f) / 100f;
            item._N2000F = Mathf.Round(item._N2000F * 100f) / 100f;
            item._GDPF = Mathf.Round(item._GDPF * 100f) / 100f;

            if (item._EconomyF < 0.01f && item._EconomyF > -0.01f)
            {
                item._EconomyF = 0;
            }
            if (item._HealthF < 0.01f && item._HealthF > -0.01f)
            {
                item._HealthF = 0;
            }
            if (item._EnvironmentF < 0.01f && item._EnvironmentF > -0.01f)
            {
                item._EnvironmentF = 0;
            }
            Action actionInit = actionDataInitial._ActionItems[i];
			if (item._Belonging_Sector == sectorList.list[2])
			{
				Debug.Log(item._Action_ID);
                actionInit._EconomyF = item._EconomyF;
                actionInit._HealthF = item._HealthF;
                actionInit._EnvironmentF = item._EnvironmentF;
                actionInit._ImageF = item._ImageF;
                actionInit._ProductionF = item._ProductionF;
                actionInit._Image = "";
                actionInit._Production = "";
                actionInit._Emissions = "";
                if (item._EconomyF >= 3)
                {
                    actionInit._Economy = "++++";
                }
                if (item._EconomyF >= 2 && item._EconomyF < 3)
                {
                    actionInit._Economy = "+++";
                }
                if (item._EconomyF >= 1 && item._EconomyF < 2)
                {
                    actionInit._Economy = "++";
                }
                if (item._EconomyF > 0 && item._EconomyF < 1)
                {
                    actionInit._Economy = "+";
                }
                if (item._EconomyF == 0)
                {
                    actionInit._Economy = "";
                }
                if (item._EconomyF <= -3)
                {
                    actionInit._Economy = "----";
                }
                if (item._EconomyF <= -2 && item._EconomyF > -3)
                {
                    actionInit._Economy = "---";
                }
                if (item._EconomyF <= -1 && item._EconomyF > -2)
                {
                    actionInit._Economy = "--";
                }
                if (item._EconomyF < 0 && item._EconomyF > -1)
                {
                    actionInit._Economy = "-";
                }
                if (item._EnvironmentF >= 3)
                {
                    actionInit._Environment = "++++";
                }
                if (item._EnvironmentF >= 2 && item._EnvironmentF < 3)
                {
                    actionInit._Environment = "+++";
                }
                if (item._EnvironmentF >= 1 && item._EnvironmentF < 2)
                {
                    actionInit._Environment = "++";
                }
                if (item._EnvironmentF > 0 && item._EnvironmentF < 1)
                {
                    actionInit._Environment = "+";
                }
                if (item._EnvironmentF == 0)
                {
                    actionInit._Environment = "";
                }
                if (item._EnvironmentF <= -3)
                {
                    actionInit._Environment = "----";
                }
                if (item._EnvironmentF <= -2 && item._EnvironmentF > -3)
                {
                    actionInit._Environment = "---";
                }
                if (item._EnvironmentF <= -1 && item._EnvironmentF > -2)
                {
                    actionInit._Environment = "--";
                }
                if (item._EnvironmentF < 0 && item._EnvironmentF > -1)
                {
                    actionInit._Environment = "-";
                }

                if (item._HealthF >= 3)
                {
                    actionInit._Health = "++++";
                }
                if (item._HealthF >= 2 && item._HealthF < 3)
                {
                    actionInit._Health = "+++";
                }
                if (item._HealthF >= 1 && item._HealthF < 2)
                {
                    actionInit._Health = "++";
                }
                if (item._HealthF > 0 && item._HealthF < 1)
                {
                    actionInit._Health = "+";
                }
                if (item._HealthF == 0)
                {
                    actionInit._Health = "";
                }
                if (item._HealthF <= -3)
                {
                    actionInit._Health = "----";
                }
                if (item._HealthF <= -2 && item._HealthF > -3)
                {
                    actionInit._Health = "---";
                }
                if (item._HealthF <= -1 && item._HealthF > -2)
                {
                    actionInit._Health = "--";
                }
                if (item._HealthF < 0 && item._HealthF > -1)
                {
                    actionInit._Health = "-";
                }
			}
			if (item._Belonging_Sector == sectorList.list[3])
			{
				Debug.Log(item._Action_ID);
                actionInit._EconomyF = item._EconomyF;
                actionInit._HealthF = item._HealthF;
                actionInit._EnvironmentF = item._EnvironmentF;
                actionInit._ImageF = item._ImageF;
                actionInit._ProductionF = item._ProductionF;
                actionInit._Production = "";
                actionInit._Emissions = "";
                if (item._EconomyF >= 3)
                {
                    actionInit._Economy = "++++";
                }
                if (item._EconomyF >= 2 && item._EconomyF < 3)
                {
                    actionInit._Economy = "+++";
                }
                if (item._EconomyF >= 1 && item._EconomyF < 2)
                {
                    actionInit._Economy = "++";
                }
                if (item._EconomyF > 0 && item._EconomyF < 1)
                {
                    actionInit._Economy = "+";
                }
                if (item._EconomyF == 0)
                {
                    actionInit._Economy = "";
                }
                if (item._EconomyF <= -3)
                {
                    actionInit._Economy = "----";
                }
                if (item._EconomyF <= -2 && item._EconomyF > -3)
                {
                    actionInit._Economy = "---";
                }
                if (item._EconomyF <= -1 && item._EconomyF > -2)
                {
                    actionInit._Economy = "--";
                }
                if (item._EconomyF < 0 && item._EconomyF > -1)
                {
                    actionInit._Economy = "-";
                }
                if (item._EnvironmentF >= 3)
                {
                    actionInit._Environment = "++++";
                }
                if (item._EnvironmentF >= 2 && item._EnvironmentF < 3)
                {
                    actionInit._Environment = "+++";
                }
                if (item._EnvironmentF >= 1 && item._EnvironmentF < 2)
                {
                    actionInit._Environment = "++";
                }
                if (item._EnvironmentF > 0 && item._EnvironmentF < 1)
                {
                    actionInit._Environment = "+";
                }
                if (item._EnvironmentF == 0)
                {
                    actionInit._Environment = "";
                }
                if (item._EnvironmentF <= -3)
                {
                    actionInit._Environment = "----";
                }
                if (item._EnvironmentF <= -2 && item._EnvironmentF > -3)
                {
                    actionInit._Environment = "---";
                }
                if (item._EnvironmentF <= -1 && item._EnvironmentF > -2)
                {
                    actionInit._Environment = "--";
                }
                if (item._EnvironmentF < 0 && item._EnvironmentF > -1)
                {
                    actionInit._Environment = "-";
                }
                if (item._HealthF >= 3)
                {
                    actionInit._Health = "++++";
                }
                if (item._HealthF >= 2 && item._HealthF < 3)
                {
                    actionInit._Health = "+++";
                }
                if (item._HealthF >= 1 && item._HealthF < 2)
                {
                    actionInit._Health = "++";
                }
                if (item._HealthF > 0 && item._HealthF < 1)
                {
                    actionInit._Health = "+";
                }
                if (item._HealthF == 0)
                {
                    actionInit._Health = "";
                }
                if (item._HealthF <= -3)
                {
                    actionInit._Health = "----";
                }
                if (item._HealthF <= -2 && item._HealthF > -3)
                {
                    actionInit._Health = "---";
                }
                if (item._HealthF <= -1 && item._HealthF > -2)
                {
                    actionInit._Health = "--";
                }
                if (item._HealthF < 0 && item._HealthF > -1)
                {
                    actionInit._Health = "-";
                }
                if (item._ImageF >= 3)
                {
                    actionInit._Image = "++++";
                }
                if (item._ImageF >= 2 && item._ImageF < 3)
                {
                    actionInit._Image = "+++";
                }
                if (item._ImageF >= 1 && item._ImageF < 2)
                {
                    actionInit._Image = "++";
                }
                if (item._ImageF > 0 && item._ImageF < 1)
                {
                    actionInit._Image = "+";
                }
                if (item._ImageF == 0)
                {
                    actionInit._Image = "";
                }
                if (item._ImageF <= -3)
                {
                    actionInit._Image = "----";
                }
                if (item._ImageF <= -2 && item._ImageF > -3)
                {
                    actionInit._Image = "---";
                }
                if (item._ImageF <= -1 && item._ImageF > -2)
                {
                    actionInit._Image = "--";
                }
                if (item._ImageF < 0 && item._ImageF > -1)
                {
                    actionInit._Image = "-";
                }
			}
			if (item._Belonging_Sector == sectorList.list[0])
			{
                item._ProductionF = (sectorList.list[item._Belonging_Sector_ID].Total_Production_Value - 581134.3f) / 5811.343f;
				Debug.Log(item._Action_ID);
                Debug.Log(sectorList.list[item._Belonging_Sector_ID].Total_Production_Value);
                actionInit._EconomyF = item._EconomyF;
                actionInit._HealthF = item._HealthF;
                actionInit._EnvironmentF = item._EnvironmentF;
                actionInit._ImageF = item._ImageF;
                actionInit._ProductionF = item._ProductionF;
                actionInit._Environment = "";
                actionInit._Economy = "";
                actionInit._Emissions = actionInit._Nox;
                /*
                if (item._EnvironmentF >= 3)
                {
                    actionInit._Emissions = "----";
                }
                if (item._EnvironmentF >= 2 && item._EnvironmentF < 3)
                {
                    actionInit._Emissions = "---";
                }
                if (item._EnvironmentF >= 1 && item._EnvironmentF < 2)
                {
                    actionInit._Emissions = "--";
                }
                if (item._EnvironmentF > 0 && item._EnvironmentF < 1)
                {
                    actionInit._Emissions = "-";
                }
                if (item._EnvironmentF == 0)
                {
                    actionInit._Emissions = "";
                }
                if (item._EnvironmentF <= -3)
                {
                    actionInit._Emissions = "++++";
                }
                if (item._EnvironmentF <= -2 && item._EnvironmentF > -3)
                {
                    actionInit._Emissions = "+++";
                }
                if (item._EnvironmentF <= -1 && item._EnvironmentF > -2)
                {
                    actionInit._Emissions = "++";
                }
                if (item._EnvironmentF < 0 && item._EnvironmentF > -1)
                {
                    actionInit._Emissions = "+";
                }
                */
                if (item._ProductionF >= 3)
                {
                    actionInit._Production = "++++";
                }
                if (item._ProductionF >= 2 && item._ProductionF < 3)
                {
                    actionInit._Production = "+++";
                }
                if (item._ProductionF >= 1 && item._ProductionF < 2)
                {
                    actionInit._Production = "++";
                }
                if (item._ProductionF > 0 && item._ProductionF < 1)
                {
                    actionInit._Production = "+";
                }
                if (item._ProductionF == 0)
                {
                    actionInit._Production = "";
                }
                if (item._ProductionF <= -3)
                {
                    actionInit._Production = "----";
                }
                if (item._ProductionF <= -2 && item._ProductionF > -3)
                {
                    actionInit._Production = "---";
                }
                if (item._ProductionF <= -1 && item._ProductionF > -2)
                {
                    actionInit._Production = "--";
                }
                if (item._ProductionF < 0 && item._ProductionF > -1)
                {
                    actionInit._Production = "-";
                }
                if (item._ImageF >= 3)
                {
                    actionInit._Image = "++++";
                }
                if (item._ImageF >= 2 && item._ImageF < 3)
                {
                    actionInit._Image = "+++";
                }
                if (item._ImageF >= 1 && item._ImageF < 2)
                {
                    actionInit._Image = "++";
                }
                if (item._ImageF > 0 && item._ImageF < 1)
                {
                    actionInit._Image = "+";
                }
                if (item._ImageF == 0)
                {
                    actionInit._Image = "";
                }
                if (item._ImageF <= -3)
                {
                    actionInit._Image = "----";
                }
                if (item._ImageF <= -2 && item._ImageF > -3)
                {
                    actionInit._Image = "---";
                }
                if (item._ImageF <= -1 && item._ImageF > -2)
                {
                    actionInit._Image = "--";
                }
                if (item._ImageF < 0 && item._ImageF > -1)
                {
                    actionInit._Image = "-";
                }
			}
			if (item._Belonging_Sector == sectorList.list[1])
			{
                item._ProductionF = (sectorList.list[item._Belonging_Sector_ID].Total_Production_Value - 17330.55f) / 173.3055f;
				Debug.Log(item._Action_ID);
                Debug.Log(sectorList.list[item._Belonging_Sector_ID].Total_Production_Value);
                actionInit._EconomyF = item._EconomyF;
                actionInit._HealthF = item._HealthF;
                actionInit._EnvironmentF = item._EnvironmentF;
                actionInit._ImageF = item._ImageF;
                actionInit._ProductionF = item._ProductionF;
                actionInit._Environment = "";
                actionInit._Economy = "";
                actionInit._Emissions = actionInit._NH3;
                /*
                if (item._EnvironmentF >= 3)
                {
                    actionInit._Emissions = "----";
                }
                if (item._EnvironmentF >= 2 && item._EnvironmentF < 3)
                {
                    actionInit._Emissions = "---";
                }
                if (item._EnvironmentF >= 1 && item._EnvironmentF < 2)
                {
                    actionInit._Emissions = "--";
                }
                if (item._EnvironmentF > 0 && item._EnvironmentF < 1)
                {
                    actionInit._Emissions = "-";
                }
                if (item._EnvironmentF == 0)
                {
                    actionInit._Emissions = "";
                }
                if (item._EnvironmentF <= -3)
                {
                    actionInit._Emissions = "++++";
                }
                if (item._EnvironmentF <= -2 && item._EnvironmentF > -3)
                {
                    actionInit._Emissions = "+++";
                }
                if (item._EnvironmentF <= -1 && item._EnvironmentF > -2)
                {
                    actionInit._Emissions = "++";
                }
                if (item._EnvironmentF < 0 && item._EnvironmentF > -1)
                {
                    actionInit._Emissions = "+";
                }
                */
                if (item._ProductionF >= 3)
                {
                    actionInit._Production = "++++";
                }
                if (item._ProductionF >= 2 && item._ProductionF < 3)
                {
                    actionInit._Production = "+++";
                }
                if (item._ProductionF >= 1 && item._ProductionF < 2)
                {
                    actionInit._Production = "++";
                }
                if (item._ProductionF > 0 && item._ProductionF < 1)
                {
                    actionInit._Production = "+";
                }
                if (item._ProductionF == 0)
                {
                    actionInit._Production = "";
                }
                if (item._ProductionF <= -3)
                {
                    actionInit._Production = "----";
                }
                if (item._ProductionF <= -2 && item._ProductionF > -3)
                {
                    actionInit._Production = "---";
                }
                if (item._ProductionF <= -1 && item._ProductionF > -2)
                {
                    actionInit._Production = "--";
                }
                if (item._ProductionF < 0 && item._ProductionF > -1)
                {
                    actionInit._Production = "-";
                }
                if (item._ImageF >= 3)
                {
                    actionInit._Image = "++++";
                }
                if (item._ImageF >= 2 && item._ImageF < 3)
                {
                    actionInit._Image = "+++";
                }
                if (item._ImageF >= 1 && item._ImageF < 2)
                {
                    actionInit._Image = "++";
                }
                if (item._ImageF > 0 && item._ImageF < 1)
                {
                    actionInit._Image = "+";
                }
                if (item._ImageF == 0)
                {
                    actionInit._Image = "";
                }
                if (item._ImageF <= -3)
                {
                    actionInit._Image = "----";
                }
                if (item._ImageF <= -2 && item._ImageF > -3)
                {
                    actionInit._Image = "---";
                }
                if (item._ImageF <= -1 && item._ImageF > -2)
                {
                    actionInit._Image = "--";
                }
                if (item._ImageF < 0 && item._ImageF > -1)
                {
                    actionInit._Image = "-";
                }
			}
        i = i + 1;
		}
	}
}