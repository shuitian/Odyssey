using UnityEngine;
using System.Collections;
using Libgame.Components;
using System.Collections.Generic;
using UnityEngine.UI;

public class ODResource : MonoBehaviour {

    private Dictionary<int, string> _resourceNames;
    private bool resourceNamesFlag = false;
    public Dictionary<int, string> resourceNames
    {
        get {
            if(resourceNamesFlag == false)
            {
                _resourceNames = Sql.GetResourceData();
                resourceNamesFlag = true;
            }
            return _resourceNames;
        }
    }

    static Dictionary<int, float> currentResources;

    void Start()
    {
        SetCurrentResourcesBySql();
    }

    static public void SetCurrentResourcesBySql()
    {
        currentResources = Sql.GetCurrentResources();
        Player.instance.SetCurrentResource(currentResources);
    }

    void OnDestroy()
    {
        Sql.CloseDB();
    }
}
