using UnityEngine;
using Libgame;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ODGame : MonoBehaviour {

    public static bool isFirstStart
    {
        get
        {
            return PlayerPrefs.GetInt("isFirstStart", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("isFirstStart", value ? 1 : 0);
        }
    }

    public void ClearPlayerInfo()
    {
        Log.WriteLog("正在清楚用户信息中，请稍候");
        isFirstStart = true;
        ODData.ClearPokemons();
        ODData.ClearSpaceShip();
        ODData.InitResource();
        Application.LoadLevel(Application.loadedLevel);
        Log.WriteLog("清楚用户信息成功");
    }

    public GameObject firstStartPanel;
    void Start()
    {
        if (isFirstStart)
        {
            ODUI.instance.ShowFirstStartPanel(isFirstStart);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
