using UnityEngine;
using System.Collections;
using Libgame.Components;
using System.Collections.Generic;

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

    void Awake()
    {
        //foreach(var item in resourceNames)
        //{
        //    print(item.Key + "=>" + item.Value);
        //}
    }

    void OnDestroy()
    {
        Sql.CloseDB();
    }
}
