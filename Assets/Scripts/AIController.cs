using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public SectorList sectorList;
    public AIData aiData;
    public ActionData actionData;
    public NationalData natdata;
    public IntVariable currentYear;
    public BoolVariable AIEnabledBool;
    private List<Action> AiActionsThisTurn = new List<Action>();

    public void MakeAiDecisions()
    {
        AiActionsThisTurn.Clear();

        if (AIEnabledBool.Value == false)
        {
            return;
        }

        foreach (AI aiAction in aiData._AIItems)
        {
            if (aiAction.Year == currentYear.Value && sectorList.list[aiAction.Role_ID].controllerAgent == Controller.AI)
            {
                AiActionsThisTurn.Add(actionData.GetAction(aiAction.Action_ID));
            }
        }

        //last push 
        if (currentYear.Value == 2033)
        {
            if (sectorList.list[3].controllerAgent != Controller.Player)
            {
                if (natdata._NationalItem._N2000_Below_Critical < 70)
                {
                    Action nationalBuyouts = actionData.GetAction(246);
                    if (sectorList.list[3].Actions.Contains(nationalBuyouts))
                    {
                        sectorList.list[3].SelectedChoices.Add(nationalBuyouts);
                    }
                }
            }
        }

        foreach (Action action in AiActionsThisTurn)
        {
            action._Belonging_Sector.SelectedChoices.Add(action);
        }
    }
}