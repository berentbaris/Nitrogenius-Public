using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI PlayerCountText;

    public void SetRoomName(string roomName)
    {
        roomNameText.text = roomName;
    }

    public void SetRoomPlayerCount(int count)
    {
        PlayerCountText.text = count + "/4";
    }

    public void OnClick()
    {
        PhotonNetwork.JoinRoom(roomNameText.text);
    }
}