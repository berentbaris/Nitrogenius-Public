using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Sector eventSector;
    public Sector governmentSector;
    public IntVariable currentYear;
    public NationalData natData;
    public ActionData ActionData;
    public SectorList sectorList;
    public BoolVariable EventsEnabledBool;

    public void checkeventEvent()
    {
        if (EventsEnabledBool.Value == false)
        {
            return;
        }

        if (currentYear.Value == 2027)
        {
            Action lawsuit_action = ActionData.GetAction(238);
            eventSector.SelectedChoices.Add(lawsuit_action);
        }
        if (currentYear.Value == 2030)
        {
            if (natData._NationalItem._N2000_Below_Critical >= 50)
            {
                Action on_target_2025 = ActionData.GetAction(248);
                if (eventSector.Actions.Contains(on_target_2025))
                {
                    eventSector.SelectedChoices.Add(on_target_2025);
                }
            }
            else
            {
                Action not_on_target_2025 = ActionData.GetAction(249);
                if (eventSector.Actions.Contains(not_on_target_2025))
                {
                    eventSector.SelectedChoices.Add(not_on_target_2025);
                }

            }
        }
        if (currentYear.Value == 2030)
        {
            if (natData._NationalItem._N2000_Below_Critical <= 50)
            {
                Action targeted_buyouts = ActionData.GetAction(252);
                if (eventSector.Actions.Contains(targeted_buyouts))
                {
                    eventSector.SelectedChoices.Add(targeted_buyouts);
                }
                Action gp_wins_action = ActionData.GetAction(239);
                if (eventSector.Actions.Contains(gp_wins_action))
                {
                    eventSector.SelectedChoices.Add(gp_wins_action);
                }
            }
            else
            {
                Action gp_loses_action = ActionData.GetAction(240);
                if (eventSector.Actions.Contains(gp_loses_action))
                {
                    eventSector.SelectedChoices.Add(gp_loses_action);
                }
            }
        }
        //2028 protests
        if (currentYear.Value == 2031)
        {
            Action targeted_buyouts = ActionData.GetAction(252);
            if (!eventSector.Actions.Contains(targeted_buyouts))
            {
                Action protests = ActionData.GetAction(253);
                if (eventSector.Actions.Contains(protests))
                {
                    eventSector.SelectedChoices.Add(protests);
                }
            }
        }
        //housing crisis
        Action action = ActionData.GetAction(242);
        if (!governmentSector.Actions.Contains(action))
        {
            Action housing_crisis_action = ActionData.GetAction(243);
            if (eventSector.Actions.Contains(housing_crisis_action))
            {
                eventSector.SelectedChoices.Add(housing_crisis_action);
            }
        }
        //transnational deposition
        if (currentYear.Value == 2033)
        {
            Action foreign_deposition_action = ActionData.GetAction(241);
            if (eventSector.Actions.Contains(foreign_deposition_action))
            {
                eventSector.SelectedChoices.Add(foreign_deposition_action);
            }
        }

        //2034 protests
        if (currentYear.Value == 2034)
        {
            Action targeted_buyouts_2 = ActionData.GetAction(252);
            if (!eventSector.Actions.Contains(targeted_buyouts_2))
            {
                    Action protests2034 = ActionData.GetAction(257);
                    if (eventSector.Actions.Contains(protests2034))
                    {
                        eventSector.SelectedChoices.Add(protests2034);
                    }
            }
        }

        foreach (Action chosenAction in eventSector.SelectedChoices)
        {
            foreach (Effect effect in chosenAction._Effects)
            {
                effect.ApplyEffect();
                effect.ApplyEffectAfter();
            }
        }
    }
}