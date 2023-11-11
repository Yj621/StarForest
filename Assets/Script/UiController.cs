using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject BuyPanel;
    // public GameObject SellPanel;
    public GameObject StorePanel; 
    public GameObject InventoryPanel;
    public GameObject EndingCheckPanel;
    // Start is called before the first frame update
    void Start()
    {
        StorePanel.SetActive(false);
        BuyPanel.SetActive(false);
        // SellPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        EndingCheckPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
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
        InventoryPanel.SetActive(true);
        StorePanel.SetActive(false);
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
