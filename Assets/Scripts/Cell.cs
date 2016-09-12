using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum CellType
{
    CanNotBuy = -1,
    CanBuy = 0,
    Buyed = 1,
    Core = 2
}

public class Cell : MonoBehaviour {

    public int x;
    public int y;
    public Image backgroung;
    public SpriteRenderer building;
    public GameObject goldTextObj;
    public Text goldText;
    public CellType cellType = CellType.CanBuy;
    static public Sprite buyedSprite;
    static public Sprite canBuySprite;

    private float _price = 0;
    void Awake()
    {
        if(buyedSprite == null)
        {
            buyedSprite = Resources.Load<Sprite>("Texture/Buyed");
        }
        if (canBuySprite == null)
        {
            canBuySprite = Resources.Load<Sprite>("Texture/CanBuy");
        }
    }

    public void SetCellType(CellType type)
    {
        this.cellType = type;
        switch (type)
        {
            case CellType.CanBuy:
                if (canBuySprite)
                {
                    backgroung.sprite = canBuySprite;
                }
                if (goldTextObj)
                {
                    goldTextObj.SetActive(true);
                }
                break;
            case CellType.Buyed:
                if (buyedSprite)
                {
                    backgroung.sprite = buyedSprite;
                }
                if (goldTextObj)
                {
                    goldTextObj.SetActive(false);
                }
                break;
        }
    }

    [ContextMenu("BuyCell")]
    public void BuyCell()
    {
        if (Player.instance.TryToAford(new Dictionary<int, float>() { { 1, _price } }))
        {
            SetCellType(CellType.Buyed);
            SpaceShip.shipCells[x, y] = (int)CellType.Buyed;
            SpaceShip.ownedCellNum++;
            Sql.UpdateCell(x, y, CellType.Buyed);
            SpaceShip.instance.MoveCellToBuyedList(gameObject);
            SpaceShip.instance.CreateCellsNearBy(x, y);
            SpaceShip.instance.UpdateAllCellPrice();
        }
        else
        {
            print("金币不够");
        }
    }

    public void OnClickCell()
    {
        SpaceShip.currentCell = this;
        if(cellType == CellType.CanBuy)
        {
            ODUI.instance.ShowBuyButton(true);
        }
        else
        {
            ODUI.instance.ShowBuyButton(false);
        }
        if (cellType == CellType.Buyed)
        {
            ODUI.instance.OnClickShowBuildingButton(true);
        }
        else
        {
            ODUI.instance.OnClickShowBuildingButton(false);
        }
        SpaceShip.instance.MoveBorder((int)transform.position.x, (int)transform.position.y);
    }

    public void SetPrice(int price)
    {
        _price = price;
        goldText.text = price + "";
    }
}
