using UnityEngine;
using System.Collections;
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
        SetCellType(CellType.Buyed);
        SpaceShip.shipCells[x, y] = (int)CellType.Buyed;
        SpaceShip.ownedCellNum++;
        Sql.UpdateCell(x, y, CellType.Buyed);
        SpaceShip.instance.MoveCellToBuyedList(gameObject);
        SpaceShip.instance.CreateCellsNearBy(x,y);
        SpaceShip.instance.UpdateAllCellPrice();
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
        SpaceShip.instance.MoveBorder((int)transform.position.x, (int)transform.position.y);
    }

    public void SetPrice(int price)
    {
        goldText.text = price + "";
    }
}
