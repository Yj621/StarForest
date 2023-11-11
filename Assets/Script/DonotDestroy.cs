using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestroy : MonoBehaviour
{
    private static DonotDestroy instance;

    // 다른 스크립트에서 이 오브젝트에 접근할 때 사용할 속성
    public static DonotDestroy Instance
    {
        get
        {
            // 인스턴스가 없으면 생성하고 그렇지 않으면 기존의 인스턴스를 반환
            if (instance == null)
            {
                // 씬에 싱글톤 오브젝트가 없으면 생성
                instance = FindObjectOfType<DonotDestroy>();

                // 씬에 싱글톤 오브젝트가 없으면 새로 생성
                if (instance == null)
                {
                    GameObject DonotDestroy = new GameObject("DonotDestroy");
                    instance = DonotDestroy.AddComponent<DonotDestroy>();
                }

                // 씬 전환 시에 파괴되지 않도록 설정
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
}
