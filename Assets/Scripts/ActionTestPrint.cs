using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ActionTestPrint : MonoBehaviour
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
	public float economydiff;
	public float envdiff;
	public float healthdiff;
	public float proddiff;
	public float imagediff;
    public float GDPdiff;
    public float N2000diff;
    public int i;
    public string filename = "";
    public int x;

    public void OnGameStart3()
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
			economydiff = (NationalData._NationalItem._Economic_Factor - 19.674f + 0.000001907349f);
			healthdiff = (NationalData._NationalItem._Health_Factor - 15.18877f - 0.000002861023f);
			envdiff = (NationalData._NationalItem._Environment_Factor - 15.42642f + 0.000001907349f);
			imagediff = (sectorList.list[item._Belonging_Sector_ID].Image - 10);
            GDPdiff = (NationalData._NationalItem._GDP_Per_Capita - 54905.2860938f - 0.0078125f);
            N2000diff = (NationalData._NationalItem._N2000_Below_Critical - 31.558934278f);
            Action actionInit = actionDataInitial._ActionItems[i];
			if (item._Action_ID == 244 || item._Action_ID == 246 || item._Action_ID == 232 || item._Action_ID == 231 || item._Action_ID == 90)
            {
                Debug.Log(item._Action_ID);
                Debug.Log(GDPdiff + "gdp");
                Debug.Log(N2000diff + "n2000");
				Debug.Log(envdiff + "env");
				Debug.Log(healthdiff + "health");
				Debug.Log(economydiff + "economy");
            }
			if (item._Belonging_Sector == sectorList.list[2])
			{
				/*Debug.Log(item._Action_ID);
				Debug.Log("Consumer");
				Debug.Log("Economy " + economydiff);
				Debug.Log("Health " + healthdiff);
				Debug.Log("Env " + envdiff);*/
                actionInit._ImageF = 0;
                actionInit._EconomyF = economydiff;
                actionInit._GDPF = GDPdiff;
                actionInit._HealthF = healthdiff;
                actionInit._EnvironmentF = envdiff;
                actionInit._N2000F = N2000diff;
                actionInit._EmissionsF = NationalData._NationalItem._Biodiversity;
			}
			if (item._Belonging_Sector == sectorList.list[3])
			{
				/*Debug.Log(item._Action_ID);
				Debug.Log("Govt");
				Debug.Log("Economy " + economydiff);
				Debug.Log("Health " + healthdiff);
				Debug.Log("Env " + envdiff);
				Debug.Log("Image " + imagediff);*/
                actionInit._ImageF = imagediff;
                actionInit._EconomyF = economydiff;
                actionInit._HealthF = healthdiff;
                actionInit._EnvironmentF = envdiff;
                actionInit._GDPF = GDPdiff;
                actionInit._N2000F = N2000diff;
			}
			if (item._Belonging_Sector == sectorList.list[0])
			{
                proddiff = (sectorList.list[item._Belonging_Sector_ID].Total_Production_Value - 581134.3f);
				/*Debug.Log(item._Action_ID);
				Debug.Log("Industry");
				Debug.Log("Prod " + proddiff);
				Debug.Log("Image " + imagediff);
                Debug.Log(sectorList.list[item._Belonging_Sector_ID].Total_Production_Value);*/
                actionInit._ImageF = imagediff;
                actionInit._EconomyF = economydiff;
                actionInit._HealthF = healthdiff;
                actionInit._EnvironmentF = envdiff;
                actionInit._GDPF = GDPdiff;
                actionInit._N2000F = N2000diff;
                actionInit._ProductionF = proddiff;
			}
			if (item._Belonging_Sector == sectorList.list[1])
			{
                proddiff = (sectorList.list[item._Belonging_Sector_ID].Total_Production_Value - 17330.55f);
				/*Debug.Log(item._Action_ID);
				Debug.Log("Farmer");
				Debug.Log("Prod " + proddiff);
				Debug.Log("Image " + imagediff);
                Debug.Log(sectorList.list[item._Belonging_Sector_ID].Total_Production_Value);*/
                actionInit._ImageF = imagediff;
                actionInit._EconomyF = economydiff;
                actionInit._HealthF = healthdiff;
                actionInit._EnvironmentF = envdiff;
                actionInit._GDPF = GDPdiff;
                actionInit._N2000F = N2000diff;
                actionInit._ProductionF = proddiff;
			}
        i = i + 1;
		}
        printtoexcel();
	}
    public void printtoexcel()
    {
        filename = Application.dataPath + "/actioneffects.csv";
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Cooldown,Executed_Turn,Action_ID,Name,Belonging_Sector_ID,Belonging_Sector,Priority,Importance,Repetivity,Cost,Initial Cost,NH3,NOx,N2O,Image,Image_F,Economy,Economy_F,GDP_F,Production,Production_F,Health,Health_F,Environment,Environment_F,N2000_F,Emissions,Emissions_F,Effect_IDs,Effects,Image_Ref,Description");
        tw.Close();
        tw = new StreamWriter(filename, true);
        x = 0;

        foreach (Action item in actionDataInitial._ActionItems)
        {
			item._Effect_IDs_String = "[";
			for (int i = 0; i < item._Effect_IDs.Length; i++)
			{
				item._Effect_IDs_String += item._Effect_IDs[i] + "nana";
			}
			item._Effect_IDs_String += "]";
            tw.WriteLine(item._Cooldown + "," + item._ExecutedTurn + "," + item._Action_ID + "," + item._Name + "," + item._Belonging_Sector_ID + "," + item._Belonging_Sector + "," + item._Priority + "," + item._Importance + "," + item._Repetivity + "," + item._Cost + "," + item._InitialCost + "," + item._NH3 + "," + item._Nox + "," + item._N2O + "," + item._Image + "," + item._ImageF + "," + item._Economy + "," + item._EconomyF + "," + item._GDPF + "," + item._Production + "," + item._ProductionF + "," + item._Health + "," + item._HealthF + "," + item._Environment + "," + item._EnvironmentF + "," + item._N2000F + "," + item._Emissions + "," + item._EmissionsF + "," + item._Effect_IDs_String + "," + "" + "," + item._Image_Ref + "," + item._Description);
        }
        tw.Close();
    }
}