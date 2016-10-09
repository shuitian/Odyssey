using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
        ShowBuyButton(false);
        //OnClickShowBuildingButton(flag);

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

    public Button[] upgrateButtons;
    public Text[] upgrateTexts;
    public void SetUpgrateButtonsCallBack(Action<int> action, ArrayList pokemonIDs)
    {
        for (int i = 0; i < upgrateButtons.Length; ++i)
        {
            upgrateButtons[i].gameObject.SetActive(false);
        }
        int maxL = Mathf.Min(upgrateButtons.Length, upgrateTexts.Length, pokemonIDs.Count);
        for (int i = 0; i < maxL; ++i) 
        {
            upgrateButtons[i].gameObject.SetActive(true);
            upgrateTexts[i].text = "建造 " + ODData.GetPokedexItemById((int)pokemonIDs[i]).characterName;
            int _i = i;
            upgrateButtons[i].onClick.RemoveAllListeners();
            upgrateButtons[i].onClick.AddListener(() => action((int)pokemonIDs[_i]));
        }
    }
}
