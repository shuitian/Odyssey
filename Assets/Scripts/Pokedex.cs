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
                    //MonoBehaviour.print(path);
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

    public bool discovery = false;
    public Sprite icon
    {
        get
        {
            return Pokedex.pokemonIcons[id];
        }
    }
}

public class Pokedex : MonoBehaviour {

    public PokedexItem this[int index]
    {
        get
        {
            if (index < maxLength)
            {
                if(pokemons[index] == null)
                {
                    
                }
                return pokemons[index];
            }
            else
            {
                Debug.LogError("Get index: " + index + " from Pokedex error! The Length is " + maxLength + "!");
                return null;
            }
        }
    }

    public const int maxLength = 151;
    public static PokemonIcon pokemonIcons = new PokemonIcon();
    public PokedexItem[] startPokemons;
    public ArrayList startPokemonsID;
    public PokedexItem[] pokemons = new PokedexItem[maxLength];
    public static Pokedex instance;
    void Awake()
    {
        instance = this;
        pokemons = (PokedexItem[])ODData.GetPokedex().ToArray(typeof(PokedexItem));

        startPokemonsID = ODData.GetStartPokemonsID();
        startPokemons = new PokedexItem[startPokemonsID.Count];
        for (int i = 0; i < startPokemonsID.Count; i++) 
        {
            startPokemons[i] = pokemons[(int)startPokemonsID[i]-1];
        }
    }

    public void SetStartPokemons()
    {
        //ArrayList startPokemonsID = Sql.GetStartPokemonsID();
        //startPokemons = Sql.GetStartPokemons();
        ODUI.instance.SetStartPokemons(startPokemons);
    }

    public void ChooseStartPokemon(PokedexItem item)
    {
        ODGame.isFirstStart = false;
        print("你选择了 " + item.characterName + " 作为你的初始神奇宝贝！");
        ODUI.instance.ShowFirstStartPanel(false);
        pokemons[item.id-1].discovery = true;
        ODData.OwnPokemon(item.id);
    }
}

