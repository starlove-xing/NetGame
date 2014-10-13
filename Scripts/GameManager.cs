using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Texture2D icon;
    public static IniFile ini;
    void Start()
    {
        GameInfo.MineJoin = new JoinDTO("Player");
        ini = new IniFile("GameInfo.ini");
        ini.WriteString("GameInfo", "Name", GameInfo.MineJoin.Name);
    }

    void Update()
    {

    }
}
