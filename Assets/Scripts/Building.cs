using UnityEngine;
using System.Collections;
using Libgame.Characters;
using Libgame.Components;
using UnityEngine.UI;

[RequireComponent(typeof(NeverMoveComponent))]
public class Building : Pokemon
{
    public SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("UpdateInfo")]
    public void SetBuilding()
    {
        if (spriteRenderer)
        {
            spriteRenderer.sprite = ODResource.pokemonIcons[id];
        }
        Sql.SetPokemonData(this, id);
    }
}
