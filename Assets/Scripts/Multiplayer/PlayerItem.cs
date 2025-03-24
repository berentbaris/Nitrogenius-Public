using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerNameText;
    public Image sectorIcon;
    public Image sectorBackground;
    public TextMeshProUGUI SectorNameText;
    public Image backgroundImage;
    public Color HighlightColor;
    public GameObject sectorSelector;
    private Player thisPlayer;
    public List<Sector> sectorList = new List<Sector>();

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public void SetPlayerInfo(Player player)
    {
        playerNameText.text = player.NickName;
        thisPlayer = player;
        UpdatePlayerItem(player);
    }

    public void SetPlayerSector(int sectorID)
    {
        if(IsSectorAvailable(sectorID) == false)
        {
            if (thisPlayer.CustomProperties.ContainsKey("Sector"))
            {
                sectorSelector.GetComponent<TMP_Dropdown>().value = (int)thisPlayer.CustomProperties["Sector"];
                return;
            }
            sectorSelector.GetComponent<TMP_Dropdown>().value = 0;
            return;
        }

        if (thisPlayer.CustomProperties.ContainsKey("Sector"))
        {
            switch (thisPlayer.CustomProperties["Sector"])
            {
                case 0:
                    PhotonNetwork.CurrentRoom.CustomProperties["Industry"] = 0;
                    break;
                case 1:
                    PhotonNetwork.CurrentRoom.CustomProperties["Agriculture"] = 0;
                    break;
                case 2:
                    PhotonNetwork.CurrentRoom.CustomProperties["Consumers"] = 0;
                    break;
                case 3:
                    PhotonNetwork.CurrentRoom.CustomProperties["Government"] = 0;
                    break;
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        }

        playerProperties["Sector"] = sectorID;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

        switch (sectorID)
        {
            case 0:
                PhotonNetwork.CurrentRoom.CustomProperties["Industry"] = 1;
                break;
            case 1:
                PhotonNetwork.CurrentRoom.CustomProperties["Agriculture"] = 1;
                break;
            case 2:
                PhotonNetwork.CurrentRoom.CustomProperties["Consumers"] = 1;
                break;
            case 3:
                PhotonNetwork.CurrentRoom.CustomProperties["Government"] = 1;
                break;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    public void SetLocalPlayer()
    {
        backgroundImage.color = HighlightColor;
        sectorSelector.SetActive(true);

        if (thisPlayer.CustomProperties.ContainsKey("Sector"))
        {
            sectorSelector.GetComponent<UIDropdownWithTitle>().AlreadySelected();
            sectorSelector.GetComponent<TMP_Dropdown>().value = (int)thisPlayer.CustomProperties["Sector"];
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == thisPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("Sector"))
        {
            sectorIcon.sprite = sectorList[(int)player.CustomProperties["Sector"]].Icon;
            sectorIcon.enabled = true;
            sectorBackground.color = sectorList[(int)player.CustomProperties["Sector"]].color.Value;
            SectorNameText.text = sectorList[(int)player.CustomProperties["Sector"]].name;
            playerProperties["Sector"] = (int)player.CustomProperties["Sector"];
        }
    }

    private bool IsSectorAvailable(int sector)
    {
        switch (sector)
        {
            case 0:
                if((int)PhotonNetwork.CurrentRoom.CustomProperties["Industry"] == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 1:
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Agriculture"] == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 2:
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Consumers"] == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                ;
            case 3:
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Government"] == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }
        return false;
    }
}