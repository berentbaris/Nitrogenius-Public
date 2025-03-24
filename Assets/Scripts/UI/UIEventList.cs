using System.Collections.Generic;
using UnityEngine;

public class UIEventList : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject UIEventPrefab;
    public SectorList sectorlist;
    public List<GameObject> UIEventGameObjects = new List<GameObject>();

    public void OnEndTurn()
    {
        ClearGOs();

        foreach (Sector sector in sectorlist.list)
        {
            foreach (Action action in sector.SelectedChoices)
            {
                GameObject obj = Instantiate(UIEventPrefab);
                UIEventGameObjects.Add(obj);
                obj.transform.SetParent(parentTransform, false);
                obj.GetComponent<UIEvent>().SetChoice(action);

                if (action._Importance == true)
                {
                    obj.transform.SetAsFirstSibling();
                }
            }
        }
    }

    private void ClearGOs()
    {
        foreach (GameObject UIEventGameobject in UIEventGameObjects)
        {
            Destroy(UIEventGameobject);
        }
        UIEventGameObjects.Clear();
    }
}