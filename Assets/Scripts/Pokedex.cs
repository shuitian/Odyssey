using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonIcon
{
    static bool initFlag = false;
    static Sprite[] loadSprites;
    public Sprite this[int index]
    {
        get
        {
            if (!initFlag)
            {
                loadSprites = new Sprite[Pokedex.maxLength + 1];
                initFlag = true;
            }
            if (index <= Pokedex.maxLength && index > 0) 
            {
                if (loadSprites[index] == null)
                {
                    string path = "Texture/Pokemons/" + ((index / 100 == 0) ? "0" : "") + ((index / 100 == 0 && (index / 10) % 10 == 0) ? "0" : "") + index;
                    MonoBehaviour.print(path);
                    loadSprites[index] = Resources.Load<Sprite>(path);
                }
                return loadSprites[index];
            }
            else
            {
                Debug.LogError("Get index: " + index + " from PokemonIcon error! The Length is " + Pokedex.maxLength + "!");
                return null;
            }
        }
    }
}

public class PokedexItem
{
    public int id;
    public string characterName;
    public float maxHp;
    public float hpRecover;
    public float attack;
    public float attackInterval;
    public float minAttackDis;
    public float maxAttackDis;
    public float armor;
    public float damageDerate;
    public float moveSpeed;

    public Sprite icon
    {
        get
        {
            return Pokedex.pokemonIcons[id];
        }
    }
}

public class Pokedex : MonoBehaviour {

    public const int maxLength = 151;
    public static PokemonIcon pokemonIcons = new PokemonIcon();
    public ArrayList startPokemons = new ArrayList();
    public ArrayList pokemons = new ArrayList();
    public static Pokedex instance;
    void Awake()
    {
        instance = this;
    }

    public void SetStartPokemons()
    {
        //ArrayList startPokemonsID = Sql.GetStartPokemonsID();
        startPokemons = Sql.GetStartPokemons();
        ODUI.instance.SetStartPokemons(startPokemons);
    }
}

