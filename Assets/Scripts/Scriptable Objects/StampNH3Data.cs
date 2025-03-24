//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class StampNH3Data : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public NH3_Stamp[] _NH3_StampItems;

	public NH3_Stamp GetNH3_Stamp(int iD) {
		int min = 0;
		int max = _NH3_StampItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			NH3_Stamp item = _NH3_StampItems[index];
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
		NH3_Stamp GetNH3_Stamp(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, NH3_Stamp> _GetNH3_Stamp;
		public NH3_Stamp GetNH3_Stamp(int ID) {
			return _GetNH3_Stamp(ID);
		}
		public DataGetter(Func<int, NH3_Stamp> getNH3_Stamp) {
			_GetNH3_Stamp = getNH3_Stamp;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetNH3_Stamp);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class NH3_Stamp {

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
	public NH3_Stamp Init(int version, StampNH3Data.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}

}

