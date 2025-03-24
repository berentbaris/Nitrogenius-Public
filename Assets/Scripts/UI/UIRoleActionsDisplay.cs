using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoleActionsDisplay : MonoBehaviour
{
    public CanvasGroup RoleActionsCanGroup;
    public SectorList sectorlist;
    public GameObject FullListRoleSelectorPrefab;
    public List<GameObject> roleSelectorList = new List<GameObject>();
    public Transform parentTransform;
    public UIChoiceFullList fullChoicelist;

    public void OnRoleActionsScreenActivate()
    {
        ClearList();
        RoleActionsCanGroup.alpha = 1;
        RoleActionsCanGroup.interactable = true;
        RoleActionsCanGroup.blocksRaycasts = true;

        List<Sector> playerSectors = new List<Sector>();

        foreach (Sector sector in sectorlist.list)
        {
            if (sector.controllerAgent == Controller.Player)
            {
                playerSectors.Add(sector);
            }
        }

        for (int i = 0; i < playerSectors.Count; i++)
        {
            GameObject obj = Instantiate(FullListRoleSelectorPrefab);
            obj.transform.SetParent(parentTransform, false);
            UIRoleFullActionsButton RoleIcon = obj.GetComponent<UIRoleFullActionsButton>();
            roleSelectorList.Add(obj);
            RoleIcon.OnCreateButton(fullChoicelist, playerSectors[i]);
            if (i == 0)
            {
                RoleIcon.OnSelect();
            }
        }
    }

    private void ClearList()
    {
        foreach (GameObject roleSelector in roleSelectorList)
        {
            Destroy(roleSelector);
        }
        roleSelectorList.Clear();
    }
}