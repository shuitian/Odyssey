using UnityEngine;
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
        Debug.Log("正在清楚用户信息中，请稍候");
        isFirstStart = true;
        Sql.ClearPokemons();
        Sql.ClearSpaceShip();
        Sql.InitResource();
        Application.LoadLevel(Application.loadedLevel);
        Debug.Log("清楚用户信息成功");
    }

    public GameObject firstStartPanel;
    void Start()
    {
        if (isFirstStart)
        {
            ODUI.instance.ShowFirstStartPanel(isFirstStart);
        }
    }

    static Dictionary<string, int> _settingDic = new Dictionary<string, int>();
    static bool settingFlag = false;
    static public Dictionary<string, int> settingDic
    {
        get
        {
            if (!settingFlag)
            {
                LoadSetting();
                settingFlag = true;
            }
            return _settingDic;
        }
    }
    static void LoadSetting()
    {
        TextAsset txt = Resources.Load<TextAsset>("Bytes/setting");
        JObject jo = (JObject)JsonConvert.DeserializeObject(txt.text);
        IEnumerable<JProperty> properties = jo.Properties();
        foreach(JProperty jp in properties)
        {
            int value = int.Parse(jp.Value.ToString());
            _settingDic[jp.Name] = value;
        }
    }
}
