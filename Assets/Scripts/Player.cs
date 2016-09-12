using UnityEngine;
using System.Collections.Generic;
using Libgame.Characters;
using Libgame.Components;

[RequireComponent(typeof(NeverMoveComponent))]
[RequireComponent(typeof(ResourceComponent))]
[RequireComponent(typeof(GoldResource))]
public class Player : Character {

    /// <summary>
    /// 资源组件，private
    /// </summary>
    protected ResourceComponent _resourceComponent;

    /// <summary>
    /// 资源组件
    /// </summary>
    public ResourceComponent resourceComponent
    {
        get
        {
            if (_resourceComponent == null)
            {
                _resourceComponent = GetComponent<ResourceComponent>();
            }
            if (_resourceComponent == null)
            {
                _resourceComponent = gameObject.AddComponent<ResourceComponent>();
            }
            return _resourceComponent;
        }
    }
    public static Player instance;
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 能够负担得起
    /// </summary>
    /// <param name="resources">资源数据结构</param>
    /// <returns></returns>
    public bool CanAford(Dictionary<int, float> resources)
    {
        return resourceComponent.CanAford(resources);
    }

    /// <summary>
    /// 试着去负担
    /// </summary>
    /// <param name="resources">资源数据结构</param>
    /// <returns></returns>
    public bool TryToAford(Dictionary<int, float> resources)
    {
        return resourceComponent.TryToAford(resources);
    }

    /// <summary>
    /// 设置当前的资源
    /// </summary>
    /// <param name="resources">资源数据结构</param>
    public void SetCurrentResource(Dictionary<int, float> resources)
    {
        resourceComponent.SetCurrentResource(resources);
    }
}
