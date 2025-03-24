//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class DepositionData : ScriptableObject
{

	public NationalData nationalData;
	public SourceList sourceList;
	public StampNH3Data StampNH3;
	public StampNOxHighData StampNOxHigh;
	public StampNOxLowData StampNOxLow;

	public Dictionary<Vector2Int, Deposition> MapDic = new Dictionary<Vector2Int, Deposition>();
	public Dictionary<Vector2Int, Deposition> MapDicN2000 = new Dictionary<Vector2Int, Deposition>();
	public Dictionary<Vector2Int, Deposition> MapDicN2000Vicinity = new Dictionary<Vector2Int, Deposition>();
	public List<Vector2Int> DirectionList = new List<Vector2Int>();


	public void OnGameStart()
	{
		CreateDictionary();
		Adjust_Emission_Coefficients();
		LocalizeEmissions();
		CalculateDeposition();
		FixDeposition();
	}

	private void CreateDictionary()
	{
		MapDic.Clear();
		MapDicN2000.Clear();
		MapDicN2000Vicinity.Clear();
		foreach (Deposition item in _DepositionItems)
		{
			if (item._NL == true)
			{
				Vector2Int newCoord = new Vector2Int(item._X, item._Y);
				MapDic.Add(newCoord, item);
				if (item._Natura2000 == true)
				{
					MapDicN2000.Add(newCoord, item);
					MapDicN2000Vicinity.Add(newCoord, item);
				}
			}
		}
		foreach (Vector2Int tile in MapDicN2000.Keys)
		{
			foreach (Vector2Int direction in DirectionList)
			{
				if (!MapDicN2000Vicinity.ContainsKey(tile + direction) && MapDic.ContainsKey(tile + direction))
				{
					MapDicN2000Vicinity.Add(tile + direction, MapDic[tile + direction]);
				}
			}
		}
	}

	public void LocalizeEmissions()
	{	
		foreach (Deposition item in _DepositionItems)
		{
			item._NH3_Emissions = sourceList.list[0].NH3_Emissions * item._NH3Agr / 100 + sourceList.list[1].NH3_Emissions * item._NH3Dom / 100 + sourceList.list[2].NH3_Emissions * item._NH3Ind / 100 + sourceList.list[3].NH3_Emissions * item._NH3Ind / 100 + sourceList.list[4].NH3_Emissions * item._NH3Traffic / 100;
			item._NOx_High_Emissions = sourceList.list[2].Nox_Emissions * item._NoxIndHigh / 100;
			item._NOx_Low_Emissions = sourceList.list[0].Nox_Emissions * item._NoxAgr / 100 + sourceList.list[1].Nox_Emissions * item._NoxDom / 100 + sourceList.list[3].Nox_Emissions * item._NoxIndLow / 100 + sourceList.list[4].Nox_Emissions * item._NoxTraffic / 100;
			nationalData._NationalItem._National_Nox_Emissions += item._NOx_High_Emissions + item._NOx_Low_Emissions;
			nationalData._NationalItem._National_NH3_Emissions += item._NH3_Emissions;
		}
	}

	public void CalculateDeposition()
	{
		foreach (KeyValuePair<Vector2Int, Deposition> item in MapDic)
		{
			item.Value._NH3_Deposition = 0;
			item.Value._NOx_Deposition = 0;
			item.Value._Total_Deposition = 0;
		}

		foreach (KeyValuePair<Vector2Int, Deposition> item in MapDic)
		{
			foreach (NH3_Stamp nh3_stamp in StampNH3._NH3_StampItems)
			{
				Vector2Int newCoord = new Vector2Int(nh3_stamp.x + item.Value._X, nh3_stamp.y + item.Value._Y);
				if (MapDic.TryGetValue(newCoord, out Deposition depo))
				{
					MapDic[newCoord]._NH3_Deposition += nh3_stamp.Deposition_Factor * (((item.Value._NH3_Emissions / 2324) * 1000000) * 0.05872f);
				}
			}
		}
		foreach (KeyValuePair<Vector2Int, Deposition> item in MapDic)
		{
			foreach (NOx_High_Stamp nox_high_stamp in StampNOxHigh._NOx_High_StampItems)
			{
				Vector2Int newCoord = new Vector2Int(nox_high_stamp.x + item.Value._X, nox_high_stamp.y + item.Value._Y);
				if (MapDic.TryGetValue(newCoord, out Deposition depo))
				{
					MapDic[newCoord]._NOx_Deposition += nox_high_stamp.Deposition_Factor * (((item.Value._NOx_High_Emissions / 17111) * 1000000) * 0.03333f);
				}
			}
		}
		foreach (KeyValuePair<Vector2Int, Deposition> item in MapDic)
		{
			foreach (NOx_Low_Stamp nox_low_stamp in StampNOxLow._NOx_Low_StampItems)
			{
				Vector2Int newCoord = new Vector2Int(nox_low_stamp.x + item.Value._X, nox_low_stamp.y + item.Value._Y);
				if (MapDic.TryGetValue(newCoord, out Deposition depo))
				{
					MapDic[newCoord]._NOx_Deposition += nox_low_stamp.Deposition_Factor * (((item.Value._NOx_Low_Emissions / 5144) * 1000000) * 0.03333f);
				}
			}
		}

		foreach (Deposition deposition in _DepositionItems)
		{
			deposition._NOx_Deposition = deposition._NOx_Deposition / 2500;
			deposition._NH3_Deposition = deposition._NH3_Deposition / 2500;
			deposition._Total_Deposition = (deposition._NOx_Deposition + deposition._NH3_Deposition);
		}
	}
	
	private void Adjust_Emission_Coefficients()
	{
		foreach (KeyValuePair<Vector2Int, Deposition> item in MapDicN2000Vicinity)
        {
            if (item.Value._Natura2000 == false)
            {
                item.Value._NH3Agr = item.Value._NH3Agr / 3;
            }
        }
        foreach (KeyValuePair<Vector2Int, Deposition> item in MapDic)
        {
            if (item.Value._Natura2000 == false)
            {
                item.Value._NH3Agr += 0.00967382657f;
            }
        }
        foreach (Deposition item in _DepositionItems)
        {
            if (item._Natura2000 == true)
			{
				item._NH3Agr = 0;
			}
			else
			{
				item._NH3Agr += 0.01046778042f;
			}
        }
	}

	public void FixDeposition()
	{
		foreach (Deposition deposition in _DepositionItems)
		{
			deposition._Deposition_Divider_NH3 = nationalData._NationalItem._National_NH3_Emissions / deposition._NH3_Deposition;
			deposition._Deposition_Divider_Nox = nationalData._NationalItem._National_Nox_Emissions / deposition._NOx_Deposition;
		}
	}

	public void DistributeEmissions()
	{
		foreach (Deposition deposition in _DepositionItems)
		{
			deposition._NH3_Deposition = nationalData._NationalItem._National_NH3_Emissions / deposition._Deposition_Divider_NH3;
			deposition._NOx_Deposition = nationalData._NationalItem._National_Nox_Emissions / deposition._Deposition_Divider_Nox;
			deposition._Total_Deposition = (deposition._NOx_Deposition + deposition._NH3_Deposition);
		}
	}

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public List<Deposition> _DepositionItems;

	public Deposition GetDeposition(int cell_ID)
	{
		int min = 0;
		int max = _DepositionItems.Count;
		while (min < max)
		{
			int index = (min + max) >> 1;
			Deposition item = _DepositionItems[index];
			if (item._Cell_ID == cell_ID) { return item.Init(mVersion, DataGetterObject); }
			if (cell_ID < item._Cell_ID)
			{
				max = index;
			}
			else
			{
				min = index + 1;
			}
		}
		return null;
	}

	public void Reset()
	{
		mVersion++;
	}

	public interface IDataGetter
	{
		Deposition GetDeposition(int Cell_ID);
	}

	private class DataGetter : IDataGetter
	{
		private Func<int, Deposition> _GetDeposition;
		public Deposition GetDeposition(int Cell_ID)
		{
			return _GetDeposition(Cell_ID);
		}
		public DataGetter(Func<int, Deposition> getDeposition)
		{
			_GetDeposition = getDeposition;
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
				mDataGetterObject = new DataGetter(GetDeposition);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Deposition
{

	[SerializeField]
	public int _Cell_ID;

	[SerializeField]
	public int _X;

	[SerializeField]
	public int _Y;

	[SerializeField]
	public bool _NL;

	[SerializeField]
	public bool _Natura2000;

	[SerializeField]
	public float _NH3Agr;

	[SerializeField]
	public float _NH3Dom;

	[SerializeField]
	public float _NH3Ind;

	[SerializeField]
	public float _NH3Traffic;

	[SerializeField]
	public float _NoxAgr;

	[SerializeField]
	public float _NoxDom;

	[SerializeField]
	public float _NoxIndHigh;

	[SerializeField]
	public float _NoxIndLow;

	[SerializeField]
	public float _NoxTraffic;

	[SerializeField]
	public float _NH3_Emissions;

	[SerializeField]
	public float _NH3_Deposition;

	[SerializeField]
	public float _NOx_High_Emissions;

	[SerializeField]
	public float _NOx_Low_Emissions;

	[SerializeField]
	public float _NOx_Deposition;

	[SerializeField]
	public float _Total_Deposition;

	[SerializeField]
	public float _Deposition_Divider_Nox;

	[SerializeField]
	public float _Deposition_Divider_NH3;

	[SerializeField]
	public int _Critical_Deposition;

	[SerializeField]
	public Image _MapDisplayImage;

	[NonSerialized]
	private int mVersion = 0;
	public Deposition Init(int version, DepositionData.IDataGetter getter)
	{
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}
}