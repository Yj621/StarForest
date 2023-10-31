using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameObject BuyPanel;
    public GameObject StorePanel; 
    // Start is called before the first frame update
    void Start()
    {
        StorePanel.SetActive(false);
        BuyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnBuy()
    {
        BuyPanel.SetActive(true);
        StorePanel.SetActive(false);
        Debug.Log("1");
    }
    public void OnBtnPanelCancle()
    {
        BuyPanel.SetActive(false);
    }
}
