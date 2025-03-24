using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField NicknameInputField;
    public Button connectButton;
    public TextMeshProUGUI connectButtonText;

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        connectButton.interactable = false;
        connectButtonText.text = "Connecting...";

        if (NicknameInputField.text.Length >= 1)
        {
            PhotonNetwork.NickName = NicknameInputField.text;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}