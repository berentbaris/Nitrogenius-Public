using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using ScriptableEvents.Events;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public static MultiplayerManager instance;
    public SimpleScriptableEvent StartMultiplayerGameEvent;
    public SimpleScriptableEvent MultiplayerTurnEndEvent;
    public SimpleScriptableEvent MultiplayerNextTurnEvent;
    public SectorList sectorList;
    public Sector localPlayerSector;
    public BoolVariable aiEnabled;
    public BoolVariable eventsEnabled;
    public IntVariable gamemode;
    public IntScriptableEvent MultiplayerActionSubmitted;
    public IntScriptableEvent MultiplayerNextTurnSubmitted;
    public int playersMadeTheirChoices;
    public ActionData actionData;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnStartMultiplayer()
    {
        base.photonView.RPC("RPC_StartMultiplayerGame", RpcTarget.All);
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    [PunRPC]
    private void RPC_StartMultiplayerGame()
    {
        SceneManager.LoadScene(0);
        StartCoroutine(SceneLoadWait());
    }

    private IEnumerator SceneLoadWait()
    {
        yield return new WaitForSeconds(0.1f);
        gamemode.SetValue(2);
        aiEnabled.SetValue(false);
        eventsEnabled.SetValue((bool)PhotonNetwork.CurrentRoom.CustomProperties["eventsEnabled"]);
        localPlayerSector = sectorList.list[(int)PhotonNetwork.LocalPlayer.CustomProperties["Sector"]];
        localPlayerSector.controllerAgent = Controller.Player;
        StartMultiplayerGameEvent.Raise();
        StartCoroutine(SendMessagePeriodically());
    }

    public void PlayerSubmittedActions()
    {
        List<int> actionIDs = new List<int>();
        foreach (Action action in localPlayerSector.SelectedChoices)
        {
            actionIDs.Add(action._Action_ID);
        }
        object[] objectArray = actionIDs.ConvertAll<object>(item => (object)item).ToArray();
        base.photonView.RPC("RPC_PlayerSubmittedActions", RpcTarget.All, localPlayerSector.ID, objectArray as object);
    }

    [PunRPC]
    private void RPC_PlayerSubmittedActions(int sectorID, object[] actionIDs)
    {
        MultiplayerActionSubmitted.Raise(sectorID);
        playersMadeTheirChoices++;

        if (localPlayerSector.ID != sectorID)
        {
            foreach (int actionId in actionIDs)
            {
                sectorList.list[sectorID].SelectedChoices.Add(actionData.GetAction(actionId));
            }
        }

        if (playersMadeTheirChoices == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            MultiplayerTurnEndEvent.Raise();
            playersMadeTheirChoices = 0;
        }
    }

    public void OnLocalPlayerNextTurnSubmit()
    {
        base.photonView.RPC("RPC_NextTurnSubmitted", RpcTarget.All, localPlayerSector.ID);
    }

    [PunRPC]
    private void RPC_NextTurnSubmitted(int sectorID)
    {
        playersMadeTheirChoices++;
        MultiplayerNextTurnSubmitted.Raise(sectorID);

        if (playersMadeTheirChoices == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            MultiplayerNextTurnEvent.Raise();
            playersMadeTheirChoices = 0;
        }
    }

    private IEnumerator SendMessagePeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            base.photonView.RPC("RPC_SendDummyMessage", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_SendDummyMessage()
    {
        print("dummy message sent");
    }
}