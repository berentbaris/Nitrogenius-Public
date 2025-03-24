using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerRoomList : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject roomCellPrefab;
    private Dictionary<RoomInfo, RoomItem> localRoomDictionary = new Dictionary<RoomInfo, RoomItem>();

    public void PopulateList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (localRoomDictionary.ContainsKey(room))
            {
                if (room.PlayerCount == 0)
                {
                    Destroy(localRoomDictionary[room].gameObject);
                    localRoomDictionary.Remove(room);
                }
                else
                {
                    localRoomDictionary[room].SetRoomPlayerCount(room.PlayerCount);
                }
            }

            else
            {
                GameObject roomGO = Instantiate(roomCellPrefab, parentTransform);
                RoomItem newRoom = roomGO.GetComponent<RoomItem>();
                newRoom.SetRoomName(room.Name);
                newRoom.SetRoomPlayerCount(room.PlayerCount);
                localRoomDictionary.Add(room, newRoom);
            }
        }
    }
}