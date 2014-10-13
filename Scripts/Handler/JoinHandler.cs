using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class JoinHandler : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void OnMessage(SocketModel model)
    {
        switch (model.command)
        {
            case JoinProtocol.Join_Client:
                Join(model.message);
                break;
            case JoinProtocol.Exit_Client:
                Exit(model.message);
                break;
        }
    }
    public void Join(string message)
    {
        Debug.Log("Recieve Join!");
        BoolDTO success = new BoolDTO();
        JoinDTO dto = Coding<JoinDTO>.decode(message);
        if (Server.Clients.Contains(dto.Name)&&dto.Name!=GameInfo.MineJoin.Name)
        {
            success.value = false;
            string res = Coding<BoolDTO>.encode(success);
            NetServer.GetInstance().SendMessage(Protocol.Join, dto.Name.GetHashCode(), JoinProtocol.Join_Server, res);
            Debug.Log(dto.Name+" is Trying Join!");
        }
        else
        {
            success.value = true;
            string res = Coding<BoolDTO>.encode(success);
            Debug.Log("Player " + dto.Name + " Joined Game!");
            MeauUI.RoomInfoSet("Player " + dto.Name + " Joined Game!");
            MeauUI.PlayerJoin(dto.Name,dto.Name, dto.Icon, 1);
            Server.Clients.Add(dto.Name);
            NetServer.GetInstance().SendMessage(Protocol.Join, dto.Name.GetHashCode(), JoinProtocol.Join_Server, res);
            NetServer.GetInstance().SendMessage(Protocol.Join, 0, JoinProtocol.Join_Server, res);
            string msg = Coding<List<string>>.encode(Server.Clients);
            NetServer.GetInstance().SendMessage(Protocol.Join, dto.Name.GetHashCode(), JoinProtocol.Join_Start, msg);
        }
    }
    internal void Exit(string message)
    {
        JoinDTO dto = Coding<JoinDTO>.decode(message);
        NetServer.GetInstance().SendMessage(Protocol.Join, 0, JoinProtocol.Exit_Server, dto.Name);
        Debug.Log(dto.Name + " is Left Game!");

    }

    internal void Start(string message)
    {
        Debug.Log(message);
    }
}
