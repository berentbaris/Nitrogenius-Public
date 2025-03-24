using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public GameObject lobbyGO;
    public GameObject roomGO;
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI LobbyLeaderText;
    public MultiplayerRoomList multiplayerRoomList;
    public Button startGameButton;
    public List<PlayerItem> playerList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    public MultiplayerManager manager;
    public BoolVariable eventsEnabled;

    ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        roomProperties["eventsEnabled"] = eventsEnabled.Value;
        roomProperties["Industry"] = 0;
        roomProperties["Agriculture"] = 0;
        roomProperties["Consumers"] = 0;
        roomProperties["Government"] = 0;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            BroadcastPropsChangeToAll = true,
            PlayerTtl = -1,
            //EmptyRoomTtl = 300000
        };
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName + "'s room", options);
        PhotonNetwork.KeepAliveInBackground = 600000f;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        multiplayerRoomList.PopulateList(roomList);
        print("roomn list update");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList();
        CheckIfGameCanStart();
    }

    public override void OnJoinedRoom()
    {
        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        LobbyLeaderText.text = "Lobby Leader: " + PhotonNetwork.MasterClient.NickName;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
            MasterClientCheck();
        }
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem playeritem in playerList)
        {
            Destroy(playeritem.gameObject);
        }
        playerList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            playerList.Add(newPlayerItem);
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.SetLocalPlayer();
            }
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Sector"))
        {
            switch (PhotonNetwork.LocalPlayer.CustomProperties["Sector"])
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
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
        playerProperties["Sector"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
    }

    public override void OnLeftRoom()
    {
        lobbyGO.SetActive(true);
        roomGO.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnStartGameButtonClick()
    {
        manager.OnStartMultiplayer();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        Debug.Log("master client is " + newMasterClient.NickName);
        LobbyLeaderText.text = "Lobby Leader: " + newMasterClient.NickName;
        MasterClientCheck();
    }

    private void MasterClientCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        print(PhotonNetwork.CurrentRoom.CustomProperties["Industry"].ToString() + PhotonNetwork.CurrentRoom.CustomProperties["Agriculture"].ToString() + PhotonNetwork.CurrentRoom.CustomProperties["Consumers"].ToString() + PhotonNetwork.CurrentRoom.CustomProperties["Government"].ToString());
        CheckIfGameCanStart();
    }

    private void CheckIfGameCanStart()
    {
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.CustomProperties["Sector"] == null)
            {
                startGameButton.interactable = false;
                return;
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            startGameButton.interactable = true;
            print("game can start");
        }
        else
        {
            startGameButton.interactable = false;
        }
    }
}