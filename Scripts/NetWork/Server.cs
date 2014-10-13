using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour
{
    public JoinHandler join;
    public static List<string> Clients = new List<string>();

    void Start()
    {
        gameObject.AddComponent<JoinHandler>();
        join = gameObject.GetComponent<JoinHandler>();
        NetServer.Notice(1984);
        GameInfo.MineJoin.Name = GameManager.ini.ReadString("GameInfo", "Name", "Player");
        Debug.Log(GameInfo.MineJoin.Name);
    }
    void Update()
    {
        if (NetServer.GetList().Count > 0)
        {
            OnMessage(NetServer.GetList()[0]);
            NetServer.GetList().RemoveAt(0);
        }
        if (Server.Clients.Count > 0)
        {
            MeauUI.StartNetGame.SetActive(true);
        }
        else
        {
            MeauUI.StartNetGame.SetActive(false);
        }
    }
    void OnMessage(SocketModel model)
    {
        switch (model.type)
        {
           case Protocol.Join:
                join.OnMessage(model);
                break;
        }
    }
    void OnDestroy()
    {
        NetServer.GetInstance().Stop();
        NetServer.CloseNotice();
    }
}
