//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class AIData : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public AI[] _AIItems;

	public AI GetAI(int iD) {
		int min = 0;
		int max = _AIItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			AI item = _AIItems[index];
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
		AI GetAI(int ID);
	}

	private class DataGetter : IDataGetter {
		private Func<int, AI> _GetAI;
		public AI GetAI(int ID) {
			return _GetAI(ID);
		}
		public DataGetter(Func<int, AI> getAI) {
			_GetAI = getAI;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetAI);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class AI {

	[SerializeField]
	private int _ID;
	public int ID { get { return _ID; } }

	[SerializeField]
	private int _Role_ID;
	public int Role_ID { get { return _Role_ID; } }

	[SerializeField]
	private int _Year;
	public int Year { get { return _Year; } }

	[SerializeField]
	private int _Action_ID;
	public int Action_ID { get { return _Action_ID; } }

	[NonSerialized]
	private int mVersion = 0;
	public AI Init(int version, AIData.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[AI]{{ID:{0}, Role_ID:{1}, Year:{2}, Action_ID:{3}}}",
			ID, Role_ID, Year, Action_ID);
	}

}

