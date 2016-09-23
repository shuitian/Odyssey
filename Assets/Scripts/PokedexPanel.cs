using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Libgame;

public class PokedexPanel : MonoBehaviour {

    public Pokedex pokedex;
    public Transform gridTransform;
    public GameObject pokedexItemPrefab;

    public ArrayList itemShowList = new ArrayList();
    bool firstShowFlag = true;
    public void Show()
    {
        this.Show(Pokedex.instance.pokemons);
    }

    public void Show(PokedexItem[] pokemonsList)
    {
        SetGridItemSize();
        if (firstShowFlag)
        {
            foreach (PokedexItem item in pokemonsList)
            {
                GameObject obj = ObjectPool.Instantiate(pokedexItemPrefab, Vector3.zero, Quaternion.identity, gridTransform);
                ODPokemondexItemShow itemShow = obj.GetComponent<ODPokemondexItemShow>();
                itemShowList.Add(itemShow);
                itemShow.SetUIDate(item, false);
            }
            firstShowFlag = false;
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public GridLayoutGroup grid;
    public void SetGridItemSize()
    {
        grid.cellSize = new Vector2(1.0F / 4 * Screen.width, 4.0F / 5 * Screen.height);
    }
}
