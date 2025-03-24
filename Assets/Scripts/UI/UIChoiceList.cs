using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceList : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject cellPrefab;
    private List<GameObject> choiceCells = new List<GameObject>();
    public IntVariable currentYear;
    public Scrollbar scrollBar;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += PopulateList;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= PopulateList;
    }

    public void PopulateList(Sector sector)
    {
        ClearList();
        foreach (Action action in sector.Actions)
        {
            if (action._Priority <= currentYear.Value)
            {
                GameObject obj = Instantiate(cellPrefab);
                choiceCells.Add(obj);
                obj.transform.SetParent(parentTransform, false);
                UIChoice choice = obj.GetComponent<UIChoice>();
                choice.SetChoice(action);
            }
        }
        scrollBar.value = 1;
    }

    private void ClearList()
    {
        foreach (GameObject UIChoiceGameobject in choiceCells)
        {
            Destroy(UIChoiceGameobject);
        }
        choiceCells.Clear();
    }
}