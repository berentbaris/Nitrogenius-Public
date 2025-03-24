using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class UIDropdownWithTitle : MonoBehaviour, ISelectHandler
{
    public TMP_Dropdown dropdown;
    bool wasNeverSelected = true;

    public void AlreadySelected()
    {
        wasNeverSelected = false;

        if (dropdown.options.Count == 5)
        {
            dropdown.options.RemoveAt(4);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (wasNeverSelected)
        {
            dropdown.options.RemoveAt(4);
            //dropdown.RefreshShownValue();
        }
        wasNeverSelected = false;
    }
}