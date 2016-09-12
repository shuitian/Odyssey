using UnityEngine;
using System.Collections;
using Libgame.Components;

public class GoldResource : SingleNoRecoverResourceComponent
{
    void Awake()
    {
        resourceId = 1;
        resourceName = "金币";
        ownAtFirst = 1000;
        AttachPointChangeCallBack(UpdateGoldNum);
    }

    void UpdateGoldNum(float before, float after)
    {
        print(123);
        ODUI.instance.UpdateGoldNum((int)after);
    }
}
