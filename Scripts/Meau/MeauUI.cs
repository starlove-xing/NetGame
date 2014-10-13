using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeauUI : MonoBehaviour
{
    #region Scenes UI
    public GameObject MeauPanel;
    public GameObject SinglePanel;
    public GameObject NetPanel;
    public GameObject SettingPanel;
    public GameObject NetWorkPanel;
    public GameObject JoinPlanel;
    public GameObject RoomPlanel;
    public InputField NameField;
    public Text RoomInfoText;
    public GameObject BtnStartNetGame;
    public GameObject PlayerListPlanel;
    public GameObject RoomList;
    public InputField HostField;
    //public Image HeadImage;

    public static GameObject Info;
    public static GameObject Host;
    #endregion

    #region 公开外部
    public static GameObject Meau;
    public static GameObject Single;
    public static GameObject Net;
    public static GameObject Setting;
    public static GameObject NetUI;
    public static GameObject Join;
    public static GameObject Room;
    public static InputField Name;
    public static Text RoomInfo;
    public static GameObject StartNetGame;
    public static GameObject PlayerList;
    public static GameObject HostList;
    public static InputField hostIP;
    //public static Image Head;
    #endregion

    void Start()
    {
        #region 初始化赋值
        Meau = MeauPanel;
        Single = SinglePanel;
        Net = NetPanel;
        Setting = SettingPanel;
        NetUI = NetWorkPanel;
        Join = JoinPlanel;
        Room = RoomPlanel;
        Name = NameField;
        PlayerList = PlayerListPlanel;
        StartNetGame = BtnStartNetGame;
        HostList = RoomList;
        hostIP = HostField;
        RoomInfo = RoomInfoText;
        MeauClear();
        //Head = HeadImage;
        Meau.SetActive(true);
        Info = Resources.Load("UIPrefab/PlayerInfo") as GameObject;
        Host = Resources.Load("UIPrefab/BtnRoom") as GameObject;
        #endregion

    }

    public static void MeauClear()
    {
        Meau.SetActive(false);
        Single.SetActive(false);
        Net.SetActive(false);
        Setting.SetActive(false);
    }

    public static void NetClear()
    {
        NetUI.SetActive(false);
        Room.SetActive(false);
        Join.SetActive(false);
        StartNetGame.SetActive(false);
    }

    public static void HostRefresh()
    {
        for (int i = 0; i < HostList.transform.childCount; i++)
        {
            Destroy(HostList.transform.GetChild(i).gameObject);
            Client.index = 0;
        }
        
    }

    public static void SettingOK()
    {
        GameInfo.MineJoin.Name = Name.value;
        //GameInfo.MineJoin.Icon = Head.sprite;
        MeauClear();
        Meau.SetActive(true);
    }

    public static void SettingClear()
    {
        Name.value = GameInfo.MineJoin.Name;
    }

    public static void RoomInfoSet(string info)
    {
        RoomInfo.text += info + "\n";
    }

    public static void JoinedGame()
    {
        NetClear();
        Room.SetActive(true);
    }

    public static void PlayerJoin(string hash, string name,Sprite icon,int index)
    {
        GameObject go = Instantiate(Info) as GameObject;
        go.name = hash;
        go.transform.parent = PlayerList.transform;
        go.transform.localPosition = new Vector3(-175 + index * 150, 25, 0);
        go.transform.GetComponentInChildren<Text>().text = name;
        //go.transform.GetComponentInChildren<Image>().sprite = icon;
    }

    public static void GetHostList(int index,string ip)
    {
        GameObject go = Instantiate(Host) as GameObject;
        go.transform.parent = HostList.transform;
        go.transform.localPosition = new Vector3(0, 110 - index * 35, 0);
        go.transform.GetComponentInChildren<Text>().text = ip;
        go.name = ip;
    }

    public static void HeadSet(Sprite icon)
    {
        //Head.sprite = icon;
    }

}
