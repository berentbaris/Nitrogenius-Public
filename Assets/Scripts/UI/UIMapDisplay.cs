using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapDisplay : MonoBehaviour
{
    public DepositionData depDataGame;
    public GameObject mapCellPrefab;
    public List<GameObject> mapCellList = new List<GameObject>();

    public ColorVariable Netherlands;
    public ColorVariable N2000Below;
    public ColorVariable N2000Above;

    public void CreateMapDisplay()
    {
        foreach (Deposition deposition in depDataGame._DepositionItems)
        {
            InstantiateCell(deposition);
        }
        UpdateMapDisplay();
    }

    private void InstantiateCell(Deposition deposition)
    {
        GameObject obj = Instantiate(mapCellPrefab);
        mapCellList.Add(obj);
        obj.transform.SetParent(this.transform, false);
        obj.transform.localPosition = new Vector3(deposition._X, deposition._Y, 0);

        deposition._MapDisplayImage = obj.GetComponent<Image>();
        deposition._MapDisplayImage.color = Netherlands.Value;
    }

    public void UpdateMapDisplay()
    {
        foreach (KeyValuePair<Vector2Int, Deposition> item in depDataGame.MapDicN2000)
        {
            if (item.Value._Critical_Deposition >= item.Value._Total_Deposition)
            {
                item.Value._MapDisplayImage.color = N2000Below.Value;
            }
            else
            {
                item.Value._MapDisplayImage.color = N2000Above.Value;
            }
        }
    }
}