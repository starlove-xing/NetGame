using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private ClientJoinHandler join;
    public static int index = 0;
    void Start()
    {
        NetClient.SearchHost(1984);
        gameObject.AddComponent<JoinHandler>();
        join = gameObject.GetComponent<ClientJoinHandler>();
        GameInfo.MineJoin.Name = GameManager.ini.ReadString("GameInfo", "Name", "Player");
        Debug.Log(GameInfo.MineJoin.Name);
        Debug.Log(NetClient.HostList.Count);
    }

    void Update()
    {
        if (NetClient.HostList.Count > 0 && NetClient.HostList.Count < index + 2)
        {
            MeauUI.GetHostList(index, NetClient.HostList[index]);
            index++;
        }
        if (NetClient.GetInstance().GetList().Count > 0)
        {
            Debug.Log(NetClient.GetInstance().GetList().Count);
            OnMessage(NetClient.GetInstance().GetList()[0]);
            NetClient.GetInstance().GetList().RemoveAt(0);
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

    }
}
