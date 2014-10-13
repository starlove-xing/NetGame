using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClientJoinHandler : MonoBehaviour
{
    public void OnMessage(SocketModel model)
    {
        switch (model.command)
        {
            case JoinProtocol.Join_Server:
                Join(model);
                break;
            case JoinProtocol.Exit_Server:
                Exit(model.message);
                break;
            case JoinProtocol.Join_Start:
                JoinStart(model);
                break;
        }
    }

    void Join(SocketModel model)
    {
        switch (model.area)
        {
            case 0:
                OtherJoin(model.message);
                break;
            default:
                MineJoin(model);
                break;
        }
    }

    void MineJoin(SocketModel model)
    {
        string message = model.message;
        BoolDTO dto = new BoolDTO();
        dto = Coding<BoolDTO>.decode(message);
        if (dto.value)
        {
            MeauUI.NetClear();
            MeauUI.Room.SetActive(true);
            Debug.Log("Join OK!");
            MeauUI.RoomInfoSet("Join OK!");
        }
        else
        {
            Debug.Log("You can't join this room!");
            Debug.Log("Join faiel");
        }
    }

    void JoinStart(SocketModel model)
    {
        if (model.area == GameInfo.MineJoin.Name.GetHashCode())
        {
            string message = model.message;
            List<string> dto = Coding<List<string>>.decode(message);
            for (int i = 0; i < dto.Count; i++)
            {
                Debug.Log(dto[i] + "is Joined Room!");
                MeauUI.RoomInfoSet(dto[i] + "is Joined Room!");
            }
        }
    }

    void OtherJoin(string message)
    {
        JoinDTO dto = new JoinDTO();
        dto = Coding<JoinDTO>.decode(message);
        Debug.Log(dto.Name + "is Joined Room!");
        MeauUI.RoomInfoSet(dto.Name + "is Joined Room!");
    }

    void Exit(string message)
    {
        Debug.Log(message + " is Left Game!");
        MeauUI.RoomInfoSet(message + " is Left Game!");
    }

    void GameStart(string message)
    {
        Debug.Log(message);
    }
}
