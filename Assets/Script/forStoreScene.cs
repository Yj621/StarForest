using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forStoreScene : MonoBehaviour
{
    public GameObject windowSell;
    private void Start()
    {
        // 초기에 Window_sell Panel을 비활성화
        if (windowSell != null)
        {
            windowSell.SetActive(false);
        }
    }

    // Slot 버튼 클릭 시 호출되는 함수
    public void OnSlotButtonClick()
    {
        // Slot 버튼을 클릭했을 때 실행하고 싶은 동작을 추가합니다.
        // 예: Window_sell Panel을 활성화
        if (windowSell != null)
        {
            windowSell.SetActive(true);
        }
    }

    // 판매 창에서 취소 버튼을 누르면 호출될 함수
    public void OnCancelClick()
    {
        // 취소 버튼을 눌렀을 때 실행하고 싶은 동작을 추가합니다.
        // 예: Window_sell Panel을 비활성화
        if (windowSell != null)
        {
            windowSell.SetActive(false);
        }
    }
}
