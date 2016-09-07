using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum CellType
{
    CAN_BUY,
    BUYED
}

public class Cell : MonoBehaviour {

    public SpriteRenderer backgroung;
    public SpriteRenderer building;
    public GameObject goldTextObj;
    public Text goldText;
    public CellType cellType = CellType.CAN_BUY;
    static public Sprite buyedSprite;
    void Awake()
    {
        if(buyedSprite == null)
        {
            buyedSprite = Resources.Load<Sprite>("Texture/Buyed");
        }
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("BuyCell")]
    void BuyCell()
    {
        if (buyedSprite)
        {
            backgroung.sprite = buyedSprite;
        }
        if (goldTextObj)
        {
            goldTextObj.SetActive(false);
        }
    }
}
