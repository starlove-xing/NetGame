using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;

public class ButtonClick : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    public class DllTest
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
        public static bool GetOpenFileName1([In, Out] OpenFileName ofn)
        {
            return GetOpenFileName(ofn);
        }
    }

    #region Meau Buttons
    public void NetClick()
    {
        MeauUI.MeauClear();
        MeauUI.Net.SetActive(true);
        MeauUI.NetClear();
        MeauUI.NetUI.SetActive(true);
    }

    public void SettingClick()
    {
        MeauUI.MeauClear();
        MeauUI.Setting.SetActive(true);
        MeauUI.SettingClear();
        MeauUI.Name.value = GameInfo.MineJoin.Name;
        //MeauUI.Head.sprite = GameInfo.MineJoin.Icon;
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion

    #region NetWorkButtons

    public void ServerClick()
    {
        NetServer.GetInstance();
        if (!NetServer.IsServer)
        {
            Debug.Log("Server Start Failed!");
            return;
        }
        MeauUI.NetClear();
        MeauUI.Room.SetActive(true);
        gameObject.AddComponent<Server>();
        MeauUI.PlayerJoin("host",GameInfo.MineJoin.Name, GameInfo.MineJoin.Icon, 0);
    }

    public void ClientClick()
    {
        MeauUI.NetClear();
        MeauUI.Join.SetActive(true);
        gameObject.AddComponent<Client>();
    }

    public void JoinClick()
    {
        NetClient.hostIP = System.Net.IPAddress.Parse(MeauUI.hostIP.value);
        string message = Coding<JoinDTO>.encode(GameInfo.MineJoin);
        NetClient.GetInstance().SendMessage(Protocol.Join, 0, JoinProtocol.Join_Client, message);
        NetClient.EndSearch();
        //UIManager.JoinedGame();
    }

    public void RefreshClick()
    {
        MeauUI.HostRefresh();
    }

    public void StartNetGame()
    {
        NetServer.CloseNotice();
        NetServer.GetInstance().SendMessage(Protocol.Join, 0, JoinProtocol.Join_Server, "Start Game");
    }

    #endregion

    #region SettingButtons
    public void SettingOK()
    {
        MeauUI.SettingOK();
        IniFile ini = new IniFile("GameInfo.ini");
        ini.WriteString("GameInfo", "Name", GameInfo.MineJoin.Name);
    }

    public void BrowClick()
    {
        #region
        /*
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "图片文件|*.jpg";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
        ofn.title = "选择图片";
        ofn.defExt = "JPG";
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
        if (DllTest.GetOpenFileName(ofn))
        {

            StartCoroutine(WaitLoad(ofn.file));//加载图片到panle
            string path1 = ofn.file;
            string s = Path.GetExtension(path1);
            string path2 = Application.dataPath+"/Images/head"+s;
            File.Copy(path1, path2, true);
        }
    }

    IEnumerator WaitLoad(string fileName)
    {
        WWW wwwTexture = new WWW("file://" + Application.dataPath+"/Images/head.jpg");
        Debug.Log(wwwTexture.url);
        Texture2D image = wwwTexture.texture;
        SpriteRenderer spr = MeauUI.Head.GetComponent<SpriteRenderer>();
        Sprite icon = Sprite.Create(image, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        Debug.Log("OK?");
        MeauUI.HeadSet(icon);
        yield return wwwTexture;
    }
        */
        #endregion
    }
    #endregion

}