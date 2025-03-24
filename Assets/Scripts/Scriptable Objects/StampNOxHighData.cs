//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class StampNOxHighData : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public NOx_High_Stamp[] _NOx_High_StampItems;

	public NOx_High_Stamp GetNOx_High_Stamp(int iD) {
		int min = 0;
		int max = _NOx_High_StampItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			NOx_High_Stamp item = _NOx_High_StampItems[index];
			if (item.ID == iD) { return item.Init(mVersion, DataGetterObject); }
			if (iD < item.ID) {
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
		NOx_High_Stamp GetNOx_High_Stamp(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, NOx_High_Stamp> _GetNOx_High_Stamp;
		public NOx_High_Stamp GetNOx_High_Stamp(int ID) {
			return _GetNOx_High_Stamp(ID);
		}
		public DataGetter(Func<int, NOx_High_Stamp> getNOx_High_Stamp) {
			_GetNOx_High_Stamp = getNOx_High_Stamp;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetNOx_High_Stamp);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class NOx_High_Stamp {

	[SerializeField]
	private int _ID;
	public int ID { get { return _ID; } }

	[SerializeField]
	private int _X;
	public int x { get { return _X; } }

	[SerializeField]
	private int _Y;
	public int y { get { return _Y; } }

	[SerializeField]
	private int _Deposition_Factor;
	public int Deposition_Factor { get { return _Deposition_Factor; } }

	[NonSerialized]
	private int mVersion = 0;
	public NOx_High_Stamp Init(int version, StampNOxHighData.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}

}

