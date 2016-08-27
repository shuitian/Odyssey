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

    public void SetBuilding(int id)
    {
        //CharacterComponent[] components = GetComponents<CharacterComponent>();
        //foreach(CharacterComponent com in components)
        //{

        //}
        //Building building =  Data.GetBuildingFromDatabase(id);
        //this.hpComponent.baseMaxHp = building.hpComponent.baseMaxHp;
    }
}
