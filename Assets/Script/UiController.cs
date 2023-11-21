using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject BuyPanel;
    // public GameObject SellPanel;
    public GameObject StorePanel; 
    public GameObject EndingCheckPanel;
    // Start is called before the first frame update
    void Start()
    {
        StorePanel.SetActive(false);
        BuyPanel.SetActive(false);
        // SellPanel.SetActive(false);
        EndingCheckPanel.SetActive(false);

    }

    public void OnBtnBuy()
    {
        BuyPanel.SetActive(true);
        StorePanel.SetActive(false);
    }
    public void OnBtnBuyCancle()
    {
        BuyPanel.SetActive(false);
    }
    public void OnBtnStorePanelCancle()
    {
        StorePanel.SetActive(false);
    }
    public void OnBtnSell()
    {
        StorePanel.SetActive(false);
        //판매 구현 해주세욤 인벤토리 열리고 판매 가능하게...
    }
    // public void OnBtnInvenSell()
    // {
    //     SellPanel.SetActive(true);
    //     InventoryPanel.SetActive(false);
    // }
    // public void OnBtnSellCancle()
    // {
    //     SellPanel.SetActive(false);
    // }
}
