//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public partial class ProductData : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public List<Product> _ProductItems;

	public Product GetProduct(int iD) {
		int min = 0;
		int max = _ProductItems.Count;
		while (min < max) {
			int index = (min + max) >> 1;
			Product item = _ProductItems[index];
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
		Product GetProduct(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, Product> _GetProduct;
		public Product GetProduct(int ID) {
			return _GetProduct(ID);
		}
		public DataGetter(Func<int, Product> getProduct) {
			_GetProduct = getProduct;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetProduct);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Product {

	[SerializeField]
	public int _ID;

	[SerializeField]
	public string _Name;

	[SerializeField]
	public string _Unit;

	[SerializeField]
	public float _Emission_N2O;

	[SerializeField]
	public float _Emission_NH3;

	[SerializeField]
	public float _Emission_NO3;

	[SerializeField]
	public float _Emission_NOx;

	[SerializeField]
	public float _Price_Per_Unit;

	[NonSerialized]
	private int mVersion = 0;
	public Product Init(int version, ProductData.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}
}