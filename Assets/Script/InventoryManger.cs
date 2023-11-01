using UnityEngine;

public class InventoryManger : MonoBehaviour
{
    private static InventoryManger instance;

    // 다음 씬에서도 게임 오브젝트를 사용하기 위해 DontDestroyOnLoad 사용
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // GameManager에 접근하는 메서드를 생성
    public static InventoryManger GetInstance()
    {
        return instance;
    }


    // 기타 인벤토리 관련 코드
}