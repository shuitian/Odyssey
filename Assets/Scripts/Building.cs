using UnityEngine;
using System.Collections;
using Libgame.Characters;
using Libgame.Components;
using Libgame;

[RequireComponent(typeof(NeverMoveComponent))]
public class Building : HArmorFightCharacter {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("UpdateInfo")]
    public void SetBuilding()
    {
        Sql.SetHpComponentData(hpComponent, id);
    }
}
