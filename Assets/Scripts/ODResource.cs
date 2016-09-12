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

    Dictionary<int, float> currentResources;
    void Awake()
    {
        currentResources = Sql.GetCurrentResources();
    }

    void Start()
    {
        Player.instance.SetCurrentResource(currentResources);
    }

    void OnDestroy()
    {
        Sql.CloseDB();
    }
}
