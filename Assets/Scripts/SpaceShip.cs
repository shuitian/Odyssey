using UnityEngine;
using System.Collections;
using Libgame;

public class SpaceShip : MonoBehaviour {

    static public SpaceShip instance;
    public GameObject border;
    public int[] cellPrice;
    public GameObject cellPrefab;
    public const int shipSize = 11;
    static public int[,] shipCells;
    public GameObject canBuyListObj;
    public ArrayList canBuyList;
    public GameObject buyedListobj;
    static public int ownedCellNum = 0;
    void Awake()
    {
        instance = this;
        canBuyList = new ArrayList();
        cellPrice = ODData.GetCellPrices();
        initShip();
    }

    void ClearExistCells()
    {
        int num = buyedListobj.transform.childCount;
        for(int i = 0;i< num; i++)
        {
            ObjectPool.Destroy(buyedListobj.transform.GetChild(i).gameObject);
        }
        num = canBuyListObj.transform.childCount;
        for (int i = 0; i < num; i++)
        {
            ObjectPool.Destroy(canBuyListObj.transform.GetChild(i).gameObject);
        }
        canBuyList.Clear();
    }

    public void initShip()
    {
        ClearExistCells();
        shipCells = ODData.GetMap();
        ownedCellNum = 0;
        for (int i = 0; i < shipSize; ++i)
        {
            for (int j = 0; j < shipSize; ++j)
            {
                if (cellPrefab)
                {
                    if (shipCells[i, j] == (int)CellType.Buyed)
                    {
                        CreateBuyedBorder(i, j);
                    }
                    else if (shipCells[i, j] == (int)CellType.CanBuy)
                    {
                        CreateCanBuyBorder(i, j);
                    }
                }
            }
        }
        UpdateAllCellPrice();
    }

    void CreateBuyedBorder(int x, int y)
    {
        if (buyedListobj)
        {
            GameObject cell = ObjectPool.Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, buyedListobj.transform);
            Cell cellComponent = cell.GetComponent<Cell>();
            cellComponent.SetCellType((CellType)shipCells[x, y]);
            cellComponent.x = x;
            cellComponent.y = y;

            ownedCellNum++;
        }
    }

    void CreateCanBuyBorder(int x, int y)
    {
        if (canBuyListObj)
        {
            GameObject cell = ObjectPool.Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, canBuyListObj.transform);
            Cell cellComponent = cell.GetComponent<Cell>();
            cellComponent.SetCellType((CellType)shipCells[x, y]);
            cellComponent.x = x;
            cellComponent.y = y;

            canBuyList.Add(cellComponent);
        }
    }

    public void UpdateAllCellPrice()
    {
        foreach (Cell cellComponent in canBuyList)
        {
            cellComponent.SetPrice(cellPrice[ownedCellNum - 8]);
        }
    }

    public void ShowCanBuyCells(bool isShow = true)
    {
        if (canBuyListObj)
        {
            canBuyListObj.SetActive(isShow);
        }
        ODUI.instance.OnClickShowBuyButton(isShow);
    }

    static public Cell currentCell;
    public void BuyCurrentCell()
    {
        if(currentCell)
            currentCell.BuyCell();
    }

    public void MoveCellToBuyedList(GameObject cellObj)
    {
        if(buyedListobj)
            cellObj.transform.SetParent(buyedListobj.transform);
    }

    int[,] addedValues = new int[4, 2]
        {
            { 1,0},
            { -1,0},
            { 0,1},
            { 0,-1}
        };

    public void CreateCellsNearBy(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            int x1 = x + addedValues[i, 0];
            int y1 = y + addedValues[i, 1];
            if (x1 >= 0 && x1 < shipSize && y1 >= 0 && y1 < shipSize)
            {
                if (SpaceShip.shipCells[x1, y1] == (int)CellType.CanNotBuy)
                {
                    SpaceShip.shipCells[x1, y1] = (int)CellType.CanBuy;
                    CreateCanBuyBorder(x1, y1);
                    ODData.UpdateCell(x1, y1, CellType.CanBuy);
                }
            }
        }
    }

    public void MoveBorder(int x, int y, bool showBorder = true)
    {
        if (border != null)
        {
            border.transform.position = new Vector2(x, y);
            border.SetActive(showBorder);
        }
    }
}
