//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public partial class ActivityData : ScriptableObject {

	public ProductData data;
	public SectorList sectorList;
	public SourceList sourceList;
	public IntVariable Turn;

	public void OnGameStart()
    {
		foreach (Activity item in _ActivityItems)
        {
			foreach (Source source in sourceList.list)
			{
				if (source.ID == item._Belonging_Emission_Sector_ID)
				{
					item._Belonging_Emission_Sector = source;
				}
			}
			item._Belonging_Sector = sectorList.list[item._Belonging_Sector_ID];
			item._Produced_Product = data.GetProduct(item._Produced_Product_ID);
			item._Fuel_Type = data.GetProduct(item._Fuel_Type_ID);
			item._Electricity_Type = data.GetProduct(item._Electricity_Type_ID);
			item._Raw_Mats_Type = data.GetProduct(item._Raw_Mats_Type_ID);
			item._Other_Mats_Type = data.GetProduct(item._Other_Mats_Type_ID);
			item.Calculate();
		}
		CalculateBudget();
		CalculateSectorEmissions();
		CalculateSourceEmissions();
	}

	public void CalculateBudget()
	{
		foreach (Activity item in _ActivityItems)
		{
			//Industry Budget
			if (item._Belonging_Sector_ID == 0)
			{
				sectorList.list[0].Display_Budget += item._Budget;
				sectorList.list[0].Total_Production_Value += item._Production_Value;
			}
			//Agriculture Budget
			if (item._Belonging_Sector_ID == 1)
			{
				sectorList.list[1].Display_Budget += item._Budget;
				sectorList.list[1].Total_Production_Value += item._Production_Value;
			}
			//Consumers Budget
			sectorList.list[2].Display_Budget = sectorList.list[2].Display_Budget + (item._Personnel_Cost - item._Personnel_Cost * 0.13f) * 0.05f;
			//Government Budget
			sectorList.list[3].Display_Budget = sectorList.list[3].Display_Budget + (((item._Product_Volume * (item._Tax_Per_Unit - item._Subsidy_Per_Unit) + (item._Fixed_Tax - item._Fixed_Subsidy)) + item._Personnel_Cost * 0.13f) + sectorList.list[2].Budget / 0.7f * 0.3f) * 0.03f;
		}
	sectorList.list[0].Budget += sectorList.list[0].Display_Budget - 700;
	sectorList.list[1].Budget += sectorList.list[1].Display_Budget + 700;
	sectorList.list[2].Budget += sectorList.list[2].Display_Budget / 6;
	sectorList.list[3].Budget += sectorList.list[3].Display_Budget / 1.2f;
	}

	public void CalculateSectorEmissions()
	{
		foreach (Activity item in _ActivityItems)
		{
			//Industry Emissions
			if (item._Belonging_Sector_ID == 0)
			{
				sectorList.list[0].N2O_Emissions = sectorList.list[0].N2O_Emissions + item._N2O_Emissions;
				sectorList.list[0].NH3_Emissions = sectorList.list[0].NH3_Emissions + item._NH3_Emissions;
				sectorList.list[0].Nox_Emissions = sectorList.list[0].Nox_Emissions + item._Nox_Emissions;	
			}
			//Agriculture Emissions
			if (item._Belonging_Sector_ID == 1)
			{
				sectorList.list[1].N2O_Emissions = sectorList.list[1].N2O_Emissions + item._N2O_Emissions;
				sectorList.list[1].NH3_Emissions = sectorList.list[1].NH3_Emissions + item._NH3_Emissions;
				sectorList.list[1].Nox_Emissions = sectorList.list[1].Nox_Emissions + item._Nox_Emissions;
				if (item._Raw_Mats_Type_ID == 3)
				{
					if (item._ID != 33)
					{
						sectorList.list[1].Fertilizer_Input += item._Product_Volume * item._Raw_Mats_Per_Unit;
						if (item._ID == 34)
						{
							sectorList.list[1].Fertilizer_Output += item._Product_Volume * (0.41f * 0.01f);
						}
						else if (item._ID == 35)
						{
							sectorList.list[1].Fertilizer_Output += item._Product_Volume * (0.26f * 0.01f);
						}
						else if (item._ID == 36)
						{
							sectorList.list[1].Fertilizer_Output += item._Product_Volume * (2.41f * 0.01f);
						}
						else if (item._ID == 37)
						{
							sectorList.list[1].Fertilizer_Output += item._Product_Volume * (0.52f * 0.01f);
						}
						else if (item._ID == 28)
						{
							sectorList.list[1].Fertilizer_Output += item._Product_Volume * (0.51f * 0.01f);
						}
					}
				}
				sectorList.list[1].NUE = sectorList.list[1].Fertilizer_Output / sectorList.list[1].Fertilizer_Input;
			}
			//Society Emissions
			if (item._Belonging_Sector_ID == 2)
			{
				sectorList.list[2].N2O_Emissions = sectorList.list[2].N2O_Emissions + item._N2O_Emissions;
				sectorList.list[2].NH3_Emissions = sectorList.list[2].NH3_Emissions + item._NH3_Emissions;
				sectorList.list[2].Nox_Emissions = sectorList.list[2].Nox_Emissions + item._Nox_Emissions;
			}
			//Government Emissions
			if (item._Belonging_Sector_ID == 3)
			{
				sectorList.list[3].N2O_Emissions = sectorList.list[3].N2O_Emissions + item._N2O_Emissions;
				sectorList.list[3].NH3_Emissions = sectorList.list[3].NH3_Emissions + item._NH3_Emissions;
				sectorList.list[3].Nox_Emissions = sectorList.list[3].Nox_Emissions + item._Nox_Emissions;
			}
		}
	}

	public void CalculateSourceEmissions()
	{
		foreach (Activity item in _ActivityItems)
		{
			item._Belonging_Emission_Sector.NH3_Emissions += item._NH3_Emissions;
			item._Belonging_Emission_Sector.Nox_Emissions += item._Nox_Emissions;
		}
		foreach (Source source in sourceList.list)
		{
			source.Nox_Emissions = source.Nox_Emissions * 0.6f;
		}
	}


	[NonSerialized]
	private int mVersion = 1;

	public List<Activity> _ActivityItems;

	public Activity GetActivity(int iD) {
		int min = 0;
		int max = _ActivityItems.Count;
		while (min < max) {
            int index = (min + max) >> 1;
			Activity item = _ActivityItems[index];
			if (item._ID == iD) { return item.Init(mVersion, DataGetterObject); }
			if (iD < item._ID) {
				max = index;
			} else {
				min = index + 1;
			}
		}
		return null;
	}

	public void Reset() {
		mVersion++;
	}
	public interface IDataGetter {
		Activity GetActivity(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, Activity> _GetActivity;
		public Activity GetActivity(int ID) {
			return _GetActivity(ID);
		}
		public DataGetter(Func<int, Activity> getActivity) {
			_GetActivity = getActivity;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetActivity);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Activity {

	public void Calculate()
	{
		//N2O Emissions
		_N2O_Emissions = (_Produced_Product._Emission_N2O * _Product_Volume);
		if (_Fuel_Type != null)
		{
			_N2O_Emissions += (_Fuel_Type._Emission_N2O * _Fuel_Per_Unit * _Product_Volume);
		}
		if (_Electricity_Type != null)
		{
			_N2O_Emissions += (_Electricity_Type._Emission_N2O * _Electricity_Per_Unit * _Product_Volume);
		}
		if (_Raw_Mats_Type != null)
		{
			_N2O_Emissions += (_Raw_Mats_Type._Emission_N2O * _Raw_Mats_Per_Unit * _Product_Volume);
		}
		if (_Other_Mats_Type != null)
		{
			_N2O_Emissions += (_Other_Mats_Type._Emission_N2O * _Other_Mats_Per_Unit * _Product_Volume);
		}

		//NH3 Emissions
		_NH3_Emissions = (_Produced_Product._Emission_NH3 * _Product_Volume);
		if (_Fuel_Type != null)
		{
			_NH3_Emissions += (_Fuel_Type._Emission_NH3 * _Fuel_Per_Unit * _Product_Volume);
		}
		if (_Electricity_Type != null)
		{
			_NH3_Emissions += (_Electricity_Type._Emission_NH3 * _Electricity_Per_Unit * _Product_Volume);
		}
		if (_Raw_Mats_Type != null)
		{
			_NH3_Emissions += (_Raw_Mats_Type._Emission_NH3 * _Raw_Mats_Per_Unit * _Product_Volume);
		}
		if (_Raw_Mats_Type_ID == 3)
		{
		}
		if (_Other_Mats_Type != null)
		{
			_NH3_Emissions += (_Other_Mats_Type._Emission_NH3 * _Other_Mats_Per_Unit * _Product_Volume);
		}

		//Nox Emissions
		_Nox_Emissions = (_Produced_Product._Emission_NOx * _Product_Volume);
		if (_Fuel_Type != null)
		{
			_Nox_Emissions += (_Fuel_Type._Emission_NOx * _Fuel_Per_Unit * _Product_Volume);
		}
		if (_Electricity_Type != null)
		{
			_Nox_Emissions += (_Electricity_Type._Emission_NOx * _Electricity_Per_Unit * _Product_Volume);
		}
		if (_Raw_Mats_Type != null)
		{
			_Nox_Emissions += (_Raw_Mats_Type._Emission_NOx * _Raw_Mats_Per_Unit * _Product_Volume);
		}
		if (_Other_Mats_Type != null)
		{
			_Nox_Emissions += (_Other_Mats_Type._Emission_NOx * _Other_Mats_Per_Unit * _Product_Volume);
		}

		//Tax Per Unit
		_Tax_Per_Unit = _Produced_Product._Price_Per_Unit * _Tax_Per_Unit_Factor;

		//Fuel Cost
		if (_Fuel_Type != null)
		{
			_Fuel_Cost = _Product_Volume * _Fuel_Per_Unit * _Fuel_Type._Price_Per_Unit;
		}

		//Electricity Cost
		if (_Electricity_Type != null)
		{
			_Electricity_Cost = _Product_Volume * _Electricity_Per_Unit * _Electricity_Type._Price_Per_Unit;
		}

		//Facility Cost
		_Facility_Cost = (float)0.11 * _Fac_Cost_Build + (float)0.163 * _Fac_Cost_EM;

		//Raw Mats Cost
		if (_Raw_Mats_Type != null)
		{
			_Raw_Mats_Cost = _Product_Volume * _Raw_Mats_Per_Unit * _Raw_Mats_Type._Price_Per_Unit;
		}

		//Other Mats Cost
		if (_Other_Mats_Type != null)
		{
			_Other_Mats_Cost = _Product_Volume * _Other_Mats_Per_Unit * _Other_Mats_Type._Price_Per_Unit;
		}

		//Personnel Cost
		_Personnel_Cost = _Product_Volume * _Jobs_Per_Unit * _Salary_Per_Job / 1000000;

		//Revenue
		_Revenue = (_Produced_Product._Price_Per_Unit - _Tax_Per_Unit + _Subsidy_Per_Unit) * _Product_Volume;

		//Costs
		_Costs = _Fuel_Cost + _Electricity_Cost + _Facility_Cost + _Raw_Mats_Cost + _Other_Mats_Cost + _Personnel_Cost;

		//Saldo
		_Saldo = _Revenue - _Costs;

		//Budget
		_Budget = _Saldo * _Budget_Factor;

		//Value
		_Production_Value = _Product_Volume * _Produced_Product._Price_Per_Unit;
	}

	[SerializeField]
	public int _ID;

	[SerializeField]
	public string _Name;

	[SerializeField]
	public int _Belonging_Sector_ID;

	[SerializeField]
	public Sector _Belonging_Sector;

	[SerializeField]
	public int _Belonging_Emission_Sector_ID;

	[SerializeField]
	public Source _Belonging_Emission_Sector;

	[SerializeField]
	public int _Produced_Product_ID;

	[SerializeField]
	public Product _Produced_Product;

	[SerializeField]
	public float _Product_Volume;

	[SerializeField]
	public string _Unit;

	[SerializeField]
	public float _Production_Value;

	[SerializeField]
	public float _Subsidy_Per_Unit;

	[SerializeField]
	public float _Fixed_Subsidy;

	[SerializeField]
	public float _Fixed_Tax;

	[SerializeField]
	public float _Quotum_NOX;

	[SerializeField]
	public float _Quotum_NH3;

	[SerializeField]
	public float _Quotum_N2O;

	[SerializeField]
	public float _Quotum_N_Total;

	[SerializeField]
	public float _Quotum_PM;

	[SerializeField]
	public float _Quotum_VOC;

	[SerializeField]
	public float _Quotum_SO2;

	[SerializeField]
	public float _Eco_Tax;

	[SerializeField]
	public float _Budget_Factor;

	[SerializeField]
	public float _Tax_Per_Unit_Factor;

	[SerializeField]
	public int _Fuel_Type_ID;

	[SerializeField]
	public Product _Fuel_Type;

	[SerializeField]
	public float _Fuel_Per_Unit;

	[SerializeField]
	public string _Fuel_Unit;

	[SerializeField]
	public int _Electricity_Type_ID;

	[SerializeField]
	public Product _Electricity_Type;

	[SerializeField]
	public float _Electricity_Per_Unit;

	[SerializeField]
	public float _Fac_Cost_EM;

	[SerializeField]
	public float _Fac_Cost_Build;

	[SerializeField]
	public int _Raw_Mats_Type_ID;

	[SerializeField]
	public Product _Raw_Mats_Type;

	[SerializeField]
	public float _Raw_Mats_Per_Unit;

	[SerializeField]
	public string _Raw_Mats_Unit;

	[SerializeField]
	public int _Other_Mats_Type_ID;

	[SerializeField]
	public Product _Other_Mats_Type;

	[SerializeField]
	public float _Other_Mats_Per_Unit;

	[SerializeField]
	public string _Other_Mats_Unit;

	[SerializeField]
	public float _Jobs_Per_Unit;

	[SerializeField]
	public float _Salary_Per_Job;

	[SerializeField]
	public int _Jobs_Total;

	[SerializeField]
	public float _Tax_Per_Unit;

	[SerializeField]
	public float _Fuel_Cost;

	[SerializeField]
	public float _Electricity_Cost;

	[SerializeField]
	public float _Facility_Cost;

	[SerializeField]
	public float _Raw_Mats_Cost;

	[SerializeField]
	public float _Other_Mats_Cost;

	[SerializeField]
	public float _Personnel_Cost;

	[SerializeField]
	public float _Revenue;

	[SerializeField]
	public float _Costs;

	[SerializeField]
	public float _Saldo;

	[SerializeField]
	public float _Budget;

	[SerializeField]
	public float _N2O_Emissions;

	[SerializeField]
	public float _NH3_Emissions;

	[SerializeField]
	public float _Fertilizer_Total;

	[SerializeField]
	public float _Nox_Emissions;

	[NonSerialized]
	private int mVersion = 0;
	public Activity Init(int version, ActivityData.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}
}