using UnityEngine;
using System.Collections;
using Libgame.Components;
using System.Collections.Generic;
using UnityEngine.UI;

public class PokemonIcon
{
    static int length = 151;
    static bool initFlag = false;
    static Sprite[] loadSprites;
    public Sprite this[int index]
    {
        get
        {
            if (!initFlag)
            {
                loadSprites = new Sprite[length+1];
                initFlag = true;
            }
            if (index <= length)
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
                Debug.LogError("Get index: " + index + " from PokemonIcon error! The Length is " + length + "!");
                return null;
            }
        }
    }
}

public class ODResource : MonoBehaviour {

    public static PokemonIcon pokemonIcons = new PokemonIcon();
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
