using UnityEngine;
using System.Collections;

public class BtnRoom : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClick()
    {
        NetClient.hostIP = System.Net.IPAddress.Parse(name);
        string message = Coding<JoinDTO>.encode(GameInfo.MineJoin);
        NetClient.GetInstance().SendMessage(Protocol.Join, 0,JoinProtocol.Join_Client, message);
        NetClient.EndSearch();
        MeauUI.JoinedGame();
    }
}
