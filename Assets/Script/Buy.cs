using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buy : MonoBehaviour
{
    public Sprite obj;  //사려는 물품
    public int Price;   //가격
    private TMP_Text money; //가진 돈(Text)

    public GameObject EndingCheckPanel;


    public void Clicked()
    {
        this.money = GameObject.Find("txt_Coin").GetComponent<TextMeshProUGUI>();

        int haveMoney = int.Parse(money.text);

        if (haveMoney >= Price) //돈 부족한지 확인
        {
            if(obj.name == "spr_deco_coracle_land") //사려는 오브젝트가 배인지 확인
            {
                EndingCheckPanel.SetActive(true);
            }
            else
            {
                this.money.text = (haveMoney - Price).ToString();
                //인벤토리에 해당 아이템 추가
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다!");
        }
    }
    public void onBtnBuyBoat()
    {
        SceneManager.LoadScene("EndingScene");
        //Debug.Log("Ending");
    }
    public void onBtnCancelBoat()
    {
        EndingCheckPanel.SetActive(false);
    }
}