//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public partial class EffectData : ScriptableObject
{
	public ProductData productData;
	public SectorList sectorList;
	public ActivityData activityData;
	public ActionData actionData;
	public DepositionData depositionData;
	public Dictionary<int, Effect> effectDictionary = new Dictionary<int, Effect>();

	public void OnGameStart()
	{
		effectDictionary.Clear();

		foreach (Effect effect in _EffectItems)
		{
			if (effectDictionary.ContainsKey(effect.ID))
            {
				Debug.Log("this effect id already exists in dic: " + effect.ID);
            }
            else
            {
				effectDictionary.Add(effect.ID, effect);
			}
			effect.SetDataReferences(productData, sectorList, activityData, actionData, depositionData);
		}
	}

	public enum ObjectType
	{
		Default, Sector, Action, Activity, Product, Deposition,
	}

	public enum OperatorType
	{
		Default, Plus, Star, Equals,
	}

	public enum ExtentType
	{
		Default, Multiply, Ignore, Complement, Supplement,
	}

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public List<Effect> _EffectItems;

	public Effect GetEffect(int iD)
	{
		if (effectDictionary.ContainsKey(iD))
        {
			return effectDictionary[iD];
		}
		return null;
	}

	public void Reset()
	{
		mVersion++;
	}

	public interface IDataGetter
	{
		Effect GetEffect(int ID);
	}

	private class DataGetter : IDataGetter
	{
		private Func<int, Effect> _GetEffect;
		public Effect GetEffect(int ID)
		{
			return _GetEffect(ID);
		}
		public DataGetter(Func<int, Effect> getEffect)
		{
			_GetEffect = getEffect;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject
	{
		get
		{
			if (mDataGetterObject == null)
			{
				mDataGetterObject = new DataGetter(GetEffect);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Effect
{
	[SerializeField]
	private ProductData productData;
	[SerializeField]
	private SectorList sectorList;
	[SerializeField]
	private ActivityData activityData;
	[SerializeField]
	private ActionData actionData;
	[SerializeField]
	private DepositionData depositionData;

	public void SetDataReferences(ProductData pd, SectorList sl, ActivityData ayd, ActionData and, DepositionData dp)
	{
		productData = pd;
		sectorList = sl;
		activityData = ayd;
		actionData = and;
		depositionData = dp;
	}

	private float FindReference()
	{
		switch (Object)
		{
			case EffectData.ObjectType.Default:
				break;
			case EffectData.ObjectType.Activity:
				if (Property == "ProductVolume")
				{
					return activityData.GetActivity(ObjectID)._Product_Volume;
				}
				if (Property == "SalaryPerJob")
				{
					return activityData.GetActivity(ObjectID)._Salary_Per_Job;
				}
				if (Property == "JobsPerUnit")
				{
					return activityData.GetActivity(ObjectID)._Jobs_Per_Unit;
				}
				if (Property == "FacCostBuild")
				{
					return activityData.GetActivity(ObjectID)._Fac_Cost_Build;
				}
				if (Property == "FacCostEM")
				{
					return activityData.GetActivity(ObjectID)._Fac_Cost_EM;
				}
				if (Property == "FuelUsePerUnit")
				{
					return activityData.GetActivity(ObjectID)._Fuel_Per_Unit;
				}
				if (Property == "QuotumNOx")
				{
					return activityData.GetActivity(ObjectID)._Quotum_NOX;
				}
				if (Property == "FixedSubsidy")
				{
					return activityData.GetActivity(ObjectID)._Fixed_Subsidy;
				}
				if (Property == "ElectricityPerUnitStd")
				{
					return activityData.GetActivity(ObjectID)._Electricity_Per_Unit;
				}
				if (Property == "RawMaterialAmountPerUnit")
				{
					return activityData.GetActivity(ObjectID)._Raw_Mats_Per_Unit;
				}
				if (Property == "OtherMaterialAmountPerUnit")
				{
					return activityData.GetActivity(ObjectID)._Other_Mats_Per_Unit;
				}
				if (Property == "ElectricityPerUnitStd")
				{
					return activityData.GetActivity(ObjectID)._Electricity_Per_Unit;
				}
				break;
			case EffectData.ObjectType.Product:
				if (Property == "PricePerUnit")
				{
					return productData.GetProduct(ObjectID)._Price_Per_Unit;
				}
				if (Property == "Emission_NOx")
				{
					return productData.GetProduct(ObjectID)._Emission_NOx;
				}
				if (Property == "Emission_N2O")
				{
					return productData.GetProduct(ObjectID)._Emission_N2O;
				}
				if (Property == "Emission_NH3")
				{
					return productData.GetProduct(ObjectID)._Emission_NH3;
				}
				break;
			case EffectData.ObjectType.Action:
				if (Property == "DirectDummyCost")
				{
					return actionData.GetAction(ObjectID)._Cost;
				}
				break;
			default:
				break;
		}
		return -912254;
	}

	private float FindReferenceAfter()
	{
		if (Object == EffectData.ObjectType.Sector)
		{
			if (Property == "Budget")
			{
				return sectorList.list[ObjectID].Budget;
			}
			if (Property == "Image")
			{
				return sectorList.list[ObjectID].Image;
			}
		}
		if (Object == EffectData.ObjectType.Activity)
		{
			if (Property == "NH3_Emissions")
			{
				return activityData.GetActivity(ObjectID)._NH3_Emissions;
			}
		}
		return -912254;
	}

	private void SetReference(float set)
	{
		switch (Object)
		{
			case EffectData.ObjectType.Default:
				break;
			case EffectData.ObjectType.Activity:
				if (Property == "ProductVolume")
				{
					activityData.GetActivity(ObjectID)._Product_Volume = set;
				}
				if (Property == "NH3_Emissions")
				{
					activityData.GetActivity(ObjectID)._NH3_Emissions = set;
				}
				if (Property == "SalaryPerJob")
				{
					activityData.GetActivity(ObjectID)._Salary_Per_Job = set;
				}
				if (Property == "JobsPerUnit")
				{
					activityData.GetActivity(ObjectID)._Jobs_Per_Unit = set;
				}
				if (Property == "FacCostBuild")
				{
					activityData.GetActivity(ObjectID)._Fac_Cost_Build = set;
				}
				if (Property == "FixedSubsidy")
				{
					activityData.GetActivity(ObjectID)._Fixed_Subsidy = set;
				}
				if (Property == "FacCostEM")
				{
					activityData.GetActivity(ObjectID)._Fac_Cost_EM = set;
				}
				if (Property == "FuelUsePerUnit")
				{
					activityData.GetActivity(ObjectID)._Fuel_Per_Unit = set;
				}
				if (Property == "QuotumNOx")
				{
					activityData.GetActivity(ObjectID)._Quotum_NOX = set;
				}
				if (Property == "ElectricityPerUnitStd")
				{
					activityData.GetActivity(ObjectID)._Electricity_Per_Unit = set;
				}
				if (Property == "RawMaterialAmountPerUnit")
				{
					activityData.GetActivity(ObjectID)._Raw_Mats_Per_Unit = set;
				}
				if (Property == "OtherMaterialAmountPerUnit")
				{
					activityData.GetActivity(ObjectID)._Other_Mats_Per_Unit = set;
				}
				if (Property == "ElectricityPerUnitStd")
				{
					activityData.GetActivity(ObjectID)._Electricity_Per_Unit = set;
				}
				break;
			case EffectData.ObjectType.Sector:
				if (Property == "Budget")
				{
					sectorList.list[ObjectID].Budget = set;
				}
				if (Property == "Image")
				{
					sectorList.list[ObjectID].Image = set;
				}
				break;
			case EffectData.ObjectType.Product:
				if (Property == "PricePerUnit")
				{
					productData.GetProduct(ObjectID)._Price_Per_Unit = set;
				}
				if (Property == "Emission_NOx")
				{
					productData.GetProduct(ObjectID)._Emission_NOx = set;
				}
				if (Property == "Emission_N2O")
				{
					productData.GetProduct(ObjectID)._Emission_N2O = set;
				}
				if (Property == "Emission_NH3")
				{
					productData.GetProduct(ObjectID)._Emission_NH3 = set;
				}
				break;
			case EffectData.ObjectType.Action:
				if (Property == "DirectDummyCost")
				{
					actionData.GetAction(ObjectID)._Cost = set;
				}
				break;
			default:
				break;
		}
	}

	public void ApplyEffect()
	{
		var variable = FindReference();
		if (variable == -912254)
        {
			return;
        }
		if (_Property == "PricePerUnit")
		{
			if (_Extent == EffectData.ExtentType.Supplement)
			{
				variable = variable * (1 + _Value);
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				variable = variable * (1 - _Value);
			}
			if (_Extent == EffectData.ExtentType.Ignore)
			{
				if (_Operator == EffectData.OperatorType.Star)
				{
					variable = variable * (1 * _Value);
				}
				if (_Operator == EffectData.OperatorType.Equals)
				{
					variable = _Value;
				}
				if (_Operator == EffectData.OperatorType.Plus)
				{
					variable = variable + _Value;
				}
			}
			if (_Extent == EffectData.ExtentType.Multiply)
			{
				variable = variable + _Value;
			}
		}
		else
		{
			if (_Extent == EffectData.ExtentType.Supplement)
			{
				variable = variable * (1 + _Value);
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				variable = variable * (1 - _Value);
			}
			if (_Extent == EffectData.ExtentType.Ignore)
			{
				if (_Operator == EffectData.OperatorType.Star)
				{
					if (_Value > 1)
					{
						variable = variable * (1 * _Value);
					}
					if (_Value < 1)
					{
						variable = variable * (_Value * 1);
					}
				}
				if (_Operator == EffectData.OperatorType.Equals)
				{
					variable = _Value;
				}
				if (_Operator == EffectData.OperatorType.Plus)
				{
					variable = variable + _Value;
				}
			}
			if (_Extent == EffectData.ExtentType.Multiply)
			{
				variable = variable + _Value;
			}
		}
		SetReference(variable);
	}

	public void ApplyEffectAfter()
	{
		if (Object == EffectData.ObjectType.Deposition)
		{
			ApplyEffectDeposition();
			return;
		}
		if (Property == "Budget" || Property == "Image" || Property == "NH3_Emissions")
        {
			var variable = FindReferenceAfter();
			if (variable == -912254)
			{
				return;
			}

			if (_Extent == EffectData.ExtentType.Supplement)
			{
				variable = variable * (1 + _Value);
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				variable = variable * (1 - _Value);
			}
			if (_Extent == EffectData.ExtentType.Ignore)
			{
				if (_Operator == EffectData.OperatorType.Star)
				{
					variable = variable * _Value;
				}
				if (_Operator == EffectData.OperatorType.Equals)
				{
					variable = _Value;
				}
				if (_Operator == EffectData.OperatorType.Plus)
				{
					variable = variable + _Value;
				}
			}
			if (_Extent == EffectData.ExtentType.Multiply)
			{
				variable = variable + _Value;
			}
			SetReference(variable);
		}	
	}

	private void ApplyEffectDeposition()
	{
		if (Property == "NH3_Agr_NL")
		{
			if (_Extent == EffectData.ExtentType.Supplement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDic)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 + _Value);
				}
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDic)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 - _Value);
				}
			}
		}
		if (Property == "NH3_Agr_N2000")
		{
			if (_Extent == EffectData.ExtentType.Supplement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 + _Value);
				}
			}
			if (_Extent == EffectData.ExtentType.Ignore)
			{
				if (_Operator == EffectData.OperatorType.Plus)
				{
					foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000)
					{
						item.Value._NH3Agr = item.Value._NH3Agr + _Value;
					}
				}
				if (_Operator == EffectData.OperatorType.Equals)
				{
					foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000)
					{
						item.Value._NH3Agr = _Value;
					}
				}
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 - _Value);
				}
			}
		}
		if (Property == "NH3_Agr_N2000_Vicinity")
		{
			if (_Extent == EffectData.ExtentType.Supplement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000Vicinity)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 + _Value);
				}
			}
			if (_Extent == EffectData.ExtentType.Ignore)
			{
				if (_Operator == EffectData.OperatorType.Plus)
				{
					foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000Vicinity)
					{
						if (item.Value._NH3Agr < 0.0643387f)
						{
							item.Value._NH3Agr = 0;
						}
						else
						{
							item.Value._NH3Agr = item.Value._NH3Agr + _Value;
						}
					}
				}
			}
			if (_Extent == EffectData.ExtentType.Complement)
			{
				foreach (KeyValuePair<Vector2Int, Deposition> item in depositionData.MapDicN2000Vicinity)
				{
					item.Value._NH3Agr = item.Value._NH3Agr * (1 - _Value);
				}
			}
		}
	}

	[SerializeField]
	private int _ID;
	public int ID { get { return _ID; } }

	[SerializeField]
	private string _Property;
	public string Property { get { return _Property; } }

	[SerializeField]
	private string _Game_Object;
	public string Game_Object { get { return _Game_Object; } }

	[SerializeField]
	private int _ObjectID;
	public int ObjectID { get { return _ObjectID; } }

	[SerializeField]
	private EffectData.ObjectType _Object;
	public EffectData.ObjectType Object { get { return _Object; } }

	[SerializeField]
	private float _Value;
	public float Value { get { return _Value; } }

	[SerializeField]
	private EffectData.OperatorType _Operator;
	public EffectData.OperatorType Operator { get { return _Operator; } }

	[SerializeField]
	private EffectData.ExtentType _Extent;
	public EffectData.ExtentType Extent { get { return _Extent; } }

	[NonSerialized]
	private int mVersion = 0;
	public Effect Init(int version, EffectData.IDataGetter getter)
	{
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}
}