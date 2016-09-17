using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ODUI : MonoBehaviour {

    static public ODUI instance;
    public GameObject buyButton;
    public GameObject showBuyButton;
    public GameObject buildingButton;
    void Awake()
    {
        instance = this;
    }

	public void ShowBuyButton(bool isShow)
    {
        if (buyButton)
            buyButton.SetActive(isShow);
    }

    public void OnClickShowBuyButton(bool flag)
    {
        if (flag == false && (SpaceShip.currentCell == null || SpaceShip.currentCell.cellType == CellType.CanBuy)) 
        {
            SpaceShip.instance.MoveBorder(0, 0, false);
        }
        ShowBuyButton(flag);
        OnClickShowBuildingButton(flag);

        if (showBuyButton)
            showBuyButton.SetActive(!flag);
    }

    public void OnClickShowBuildingButton(bool flag)
    {
        if (buildingButton)
            buildingButton.SetActive(flag);
        //OnClickShowBuyButton(!flag);
    }

    public Text goldNumText;
    public void UpdateGoldNum(int num)
    {
        if (goldNumText)
        {
            goldNumText.text = num + "";
        }
    }

    public GameObject firstStartPanel;
    public void ShowFirstStartPanel(bool flag)
    {
        if (firstStartPanel)
        {
            firstStartPanel.SetActive(flag);
        }
        if (flag)
        {
            Pokedex.instance.SetStartPokemons();
        }
    }

    public ODPokemondexItemShow[] startPokemondexItemShows;
    public void SetStartPokemons(PokedexItem[] pokemons)
    {
        for (int i = 0; i < pokemons.Length && i < startPokemondexItemShows.Length; ++i)
        {
            startPokemondexItemShows[i].gameObject.SetActive(true);
            startPokemondexItemShows[i].SetUIDate(pokemons[i]);
        }
    }
}
