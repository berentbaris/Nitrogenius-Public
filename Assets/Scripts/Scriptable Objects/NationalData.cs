//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public partial class NationalData : ScriptableObject {

	public SectorList sectorList;
    public ActivityData ActivityData;
	public DepositionData DepositionData;
	public IntVariable Turn;
	public ActionData actionData;

	public void ClearRecords()
	{
		_NationalItem.GDP_Per_Capita_Record.Clear();
		_NationalItem.Happiness_Record.Clear();
		_NationalItem.Total_Value_Record.Clear();
		_NationalItem._N2000_Below_Critical_Record.Clear();
	}

	public void RecordPreviousTurnValues()
	{
		_NationalItem.GDP_Per_Capita_Record.Add(_NationalItem._GDP_Per_Capita);
		_NationalItem.Happiness_Record.Add(_NationalItem._Happiness);
		_NationalItem.Total_Value_Record.Add(_NationalItem._Total_Value);
		_NationalItem._N2000_Below_Critical_Record.Add(_NationalItem._N2000_Below_Critical);
	}

	public void CalculateNationalDeposition()
	{
		_NationalItem._National_N2O_Emissions = 0;
		foreach (Activity activity in ActivityData._ActivityItems)
		{
		_NationalItem._National_N2O_Emissions += activity._N2O_Emissions;
		}
		foreach (Deposition item in DepositionData._DepositionItems)
		{
			if (item._NL == true)
			{
				_NationalItem._Mean_Nox_Deposition += item._NOx_Deposition;
				_NationalItem._Mean_NH3_Deposition += item._NH3_Deposition;
			}
			if (item._Natura2000 == true)
			{
				_NationalItem._Mean_N2000_Nox_Deposition += item._NOx_Deposition;
				_NationalItem._Mean_N2000_NH3_Deposition += item._NH3_Deposition;
			}
		}
		_NationalItem._Mean_Nox_Deposition = _NationalItem._Mean_Nox_Deposition / 1520;
		_NationalItem._Mean_NH3_Deposition = _NationalItem._Mean_NH3_Deposition / 1520;
		_NationalItem._Mean_N2000_Nox_Deposition = _NationalItem._Mean_N2000_Nox_Deposition / 263;
		_NationalItem._Mean_N2000_NH3_Deposition = _NationalItem._Mean_N2000_NH3_Deposition / 263;
	}

	public void CalculateN2000Percentage()
    {
		_NationalItem._N2000_Below_Critical = 0;
		foreach (Deposition deposition in DepositionData._DepositionItems)
		{
			if (deposition._Total_Deposition < deposition._Critical_Deposition && deposition._Natura2000 == true)
			{
				_NationalItem._N2000_Below_Critical++;
			}
		}
		_NationalItem._N2000_Below_Critical = _NationalItem._N2000_Below_Critical / 263 * 100;
	}

	public void CalculateHappiness()
	{
		//Population
		_NationalItem._Population = _NationalItem._Population + (_NationalItem._Population / 100 * _NationalItem._Population_Growth_Rate);
		//GDP/Value
		_NationalItem._Total_Value = 0;
		_NationalItem._GDP = 0;
		foreach (Activity item in ActivityData._ActivityItems)
		{
			_NationalItem._Total_Value += item._Production_Value;
		}
		_NationalItem._GDP = _NationalItem._Total_Value * 1356759;
		/*for (int i = 1; i < Turn.Value; i++)
		{
			_NationalItem._GDP = _NationalItem._GDP - ((_NationalItem._GDP / 100) * 3.7f);
		}*/
		//GDP Per Capita
		_NationalItem._GDP_Per_Capita = _NationalItem._GDP / _NationalItem._Population;
		//Calculate Employement
		_NationalItem._Unemployment = 0;
		foreach (Activity activity in ActivityData._ActivityItems)
		{
			_NationalItem._Unemployment += activity._Product_Volume * activity._Jobs_Per_Unit;
		}
		_NationalItem._Unemployment = 95.15f - (95.15f / 10) + (_NationalItem._Unemployment * 0.00000145295f) * 0.97f;
		//Calculate Happiness
		_NationalItem._Happiness = 0;
		_NationalItem._Economic_Factor = 0;
		_NationalItem._Health_Factor = 0;
		_NationalItem._Water_Quality = 0;
		_NationalItem._Air_Quality = 0;
		_NationalItem._Stress = 0;
		_NationalItem._Environment_Factor = 0;
		_NationalItem._Biodiversity = 0;
		_NationalItem._Climate_Action = 0;
		_NationalItem._Economic_Factor = (((_NationalItem._GDP_Per_Capita * _NationalItem._Unemployment) - 4377620) / (6067620 - 4377620)) * (40 - 0) + 0;
		if (_NationalItem._Economic_Factor > 40)
		{
			_NationalItem._Economic_Factor = 40;
		}
		if (_NationalItem._Economic_Factor < 0)
		{
			_NationalItem._Economic_Factor = 0;
		}
		_NationalItem._Air_Quality = (((0 - _NationalItem._Mean_Nox_Deposition) / (3400 - 0)) * (10 - 0) + 10);
		if (_NationalItem._Air_Quality > 10)
		{
			_NationalItem._Air_Quality = 10;
		}
		if (_NationalItem._Air_Quality < 0)
		{
			_NationalItem._Air_Quality = 0;
		}
		_NationalItem._Water_Quality = (((0 - _NationalItem._Mean_NH3_Deposition) / (2800 - 0)) * (15 - 0) + 15);
		if (_NationalItem._Water_Quality > 15)
		{
			_NationalItem._Water_Quality = 15;
		}
		if (_NationalItem._Water_Quality < 0)
		{
			_NationalItem._Water_Quality = 0;
		}
		_NationalItem._Stress = ((((_NationalItem._GDP_Per_Capita * _NationalItem._Unemployment) - 1867620) / (7867620 - 1867620)) * (5 - 0) + 0);
		if (_NationalItem._Stress > 5)
		{
			_NationalItem._Stress = 5;
		}
		if (_NationalItem._Stress < 0)
		{
			_NationalItem._Stress = 0;
		}
		_NationalItem._Health_Factor = _NationalItem._Water_Quality + _NationalItem._Air_Quality + _NationalItem._Stress;
		foreach (Sector sector in sectorList.list)
		{
			sector.public_health = _NationalItem._Health_Factor;
		}
		_NationalItem._Biodiversity = (((_NationalItem._N2000_Below_Critical - 0) / (61 - 0)) * (15 - 0) + 0);
		if (_NationalItem._Biodiversity > 15)
		{
			_NationalItem._Biodiversity = 15;
		}
		if (_NationalItem._Biodiversity < 0)
		{
			_NationalItem._Biodiversity = 0;
		}
		_NationalItem._Climate_Action = (((13000 - _NationalItem._National_N2O_Emissions) / (72089 - 13000)) * (15 - 0) + 15);
		if (_NationalItem._Climate_Action > 15)
		{
			_NationalItem._Climate_Action = 15;
		}
		if (_NationalItem._Climate_Action < 0)
		{
			_NationalItem._Climate_Action = 0;
		}
		_NationalItem._Environment_Factor = _NationalItem._Biodiversity + _NationalItem._Climate_Action;
		_NationalItem._Happiness = _NationalItem._Economic_Factor + _NationalItem._Health_Factor + _NationalItem._Environment_Factor;
		foreach (Sector sector in sectorList.list)
		{
			if (sector.SelectedChoices.Contains(actionData.GetAction(211)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(212)))
				{
					_NationalItem._Happiness_Adjuster += 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(177)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(170)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(150)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(151)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(86)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(88)))
				{
					_NationalItem._Happiness_Adjuster += 0.5f;
				}
		}
		_NationalItem._Happiness = _NationalItem._Happiness + _NationalItem._Happiness_Adjuster;
	}

	public void CalculateSectorScore()
	{
		foreach (Sector sector in sectorList.list)
        {
			if (sector == sectorList.list[0])
			{
				sector.imageScore = (((sector.Image - 9) / (11 - 9)) * (10 - 0) + 0);
				if (sector.imageScore > 10)
				{
					sector.imageScore = 10;
				}
				if (sector.imageScore < 0)
				{
					sector.imageScore = 0;
				}
				sector.emissionScore = (((100 - (((sector.Nox_Emissions - sector.NoxRecord[0]) / sector.NoxRecord[0]) * 100)) / (100 + 100)) * (10 - 0) + 0);
				if (sector.emissionScore > 10)
				{
					sector.emissionScore = 10;
				}
				if (sector.emissionScore < 0)
				{
					sector.emissionScore = 0;
				}
				sector.score = ((((((sector.Total_Production_Value - sector.TotalProductionValueRecord[0]) / sector.TotalProductionValueRecord[0]) * 100) + 100) / (100 + 100)) * (80 - 0) + 0) + sector.imageScore + sector.emissionScore;
			}
			if (sector == sectorList.list[1])
			{
				sector.imageScore = (((sector.Image - 9) / (11 - 9)) * (10 - 0) + 0);
				if (sector.imageScore > 10)
				{
					sector.imageScore = 10;
				}
				if (sector.imageScore < 0)
				{
					sector.imageScore = 0;
				}
				sector.emissionScore = (((100 - (((sector.NH3_Emissions - sector.NH3Record[0]) / sector.NH3Record[0]) * 100)) / (100 + 100)) * (10 - 0) + 0);
				if (sector.emissionScore > 10)
				{
					sector.emissionScore = 10;
				}
				if (sector.emissionScore < 0)
				{
					sector.emissionScore = 0;
				}
				sector.score = ((((((sector.Total_Production_Value - sector.TotalProductionValueRecord[0]) / sector.TotalProductionValueRecord[0]) * 100) + 100) / (100 + 100)) * (80 - 0) + 0) + sector.imageScore + sector.emissionScore;
			}
			if (sector == sectorList.list[3])
			{
				sector.imageScore = (((sector.Image - 9) / (11 - 9)) * (50 - 0) + 0);
				if (sector.imageScore > 50)
				{
					sector.imageScore = 50;
				}
				if (sector.imageScore < 0)
				{
					sector.imageScore = 0;
				}
				sector.score = (((_NationalItem._Happiness - 0) / (100 - 0)) * (50 - 0) + 0) + sector.imageScore;
			}
			if (sector == sectorList.list[2])
			{
				sector.score = _NationalItem._Happiness;
			}
		}
	}	

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public National _NationalItem;

	public void Reset() {
		mVersion++;
	}
}

[Serializable]
public class National {

	[SerializeField]
	public float _Population;

	[SerializeField]
	public float _Population_Growth_Rate;

	[SerializeField]
	public float _GDP;

	[SerializeField]
	public float _Total_Value;
	public List<float> Total_Value_Record = new List<float>();

	[SerializeField]
	public float _GDP_Per_Capita;
    public List<float> GDP_Per_Capita_Record = new List<float>();

	[SerializeField]
	public float _Unemployment;

	[SerializeField]
	public float _Economic_Factor;

	[SerializeField]
	public float _Health_Factor;

	[SerializeField]
	public float _Air_Quality;

	[SerializeField]
	public float _Water_Quality;

	[SerializeField]
	public float _Stress;

	[SerializeField]
	public float _Environment_Factor;

	[SerializeField]
	public float _Climate_Action;

	[SerializeField]
	public float _Biodiversity;

	[SerializeField]
	public float _Happiness;
    public List<float> Happiness_Record = new List<float>();

	[SerializeField]
	public float _Happiness_Adjuster;

	[SerializeField]
	public float _National_N2O_Emissions;

	[SerializeField]
	public float _National_NH3_Emissions;

	[SerializeField]
	public float _National_Nox_Emissions;

	[SerializeField]
	public float _National_Total_Deposition;

	[SerializeField]
	public float _Mean_Nox_Deposition;

	[SerializeField]
	public float _Mean_NH3_Deposition;

	[SerializeField]
	public float _Mean_N2000_Nox_Deposition;

	[SerializeField]
	public float _Mean_N2000_NH3_Deposition;

	[SerializeField]
	public float _N_Imports;

	[SerializeField]
	public float _N_Exports;

	[SerializeField]
	public float _N2000_Below_Critical;
	public List<float> _N2000_Below_Critical_Record = new List<float>();
}