using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceFullList : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject choiceUnselectableCellPrefab;
    private List<GameObject> choiceCells = new List<GameObject>();
    public Scrollbar scrollBar;
    private int startingyear = 2022;
    
    public void PopulateList(Sector sector)
    {
        ClearList();
        List<Action> shownActions = new List<Action>();

        for (int i = startingyear; i < 2028; i++)
        {
            foreach (Action action in sector.Actions)
            {
                if (action._Priority <= i && !shownActions.Contains(action))
                {
                    GameObject obj = Instantiate(choiceUnselectableCellPrefab);
                    choiceCells.Add(obj);
                    obj.transform.SetParent(parentTransform, false);
                    UIChoiceUnselectable choice = obj.GetComponent<UIChoiceUnselectable>();
                    choice.SetChoice(action);
                    shownActions.Add(action);
                }
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