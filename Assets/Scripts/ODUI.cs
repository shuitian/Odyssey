using UnityEngine;
using System.Collections;

public class ODUI : MonoBehaviour {

    static public ODUI instance;
    public GameObject buyButton;
    public GameObject showBuyButton;
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
        if (flag == false)
        {
            SpaceShip.instance.MoveBorder(0, 0, false);
            ShowBuyButton(false);
        }
        if (showBuyButton)
            showBuyButton.SetActive(!flag);
    }
}
