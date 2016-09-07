using UnityEngine;
using System.Collections;

public class CoreCell : Cell {

    static public CoreCell instance;
    public GameObject border;
    void Awake()
    {
        instance = this;
    }

    public void OnClickCell(int x,int y)
    {
        if (border != null)
        {
            border.transform.position = new Vector2(x, y);
            border.SetActive(true);
        }
    }

	public void BuyCell(int x, int y)
    {
        
    }
}
