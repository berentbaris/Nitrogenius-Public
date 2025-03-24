//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class StampNOxLowData : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public NOx_Low_Stamp[] _NOx_Low_StampItems;

	public NOx_Low_Stamp GetNOx_Low_Stamp(int iD) {
		int min = 0;
		int max = _NOx_Low_StampItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			NOx_Low_Stamp item = _NOx_Low_StampItems[index];
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
		NOx_Low_Stamp GetNOx_Low_Stamp(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, NOx_Low_Stamp> _GetNOx_Low_Stamp;
		public NOx_Low_Stamp GetNOx_Low_Stamp(int ID) {
			return _GetNOx_Low_Stamp(ID);
		}
		public DataGetter(Func<int, NOx_Low_Stamp> getNOx_Low_Stamp) {
			_GetNOx_Low_Stamp = getNOx_Low_Stamp;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetNOx_Low_Stamp);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class NOx_Low_Stamp {

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
	public NOx_Low_Stamp Init(int version, StampNOxLowData.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}

}

