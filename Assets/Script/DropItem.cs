using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    Player thePlayer;
    public GameObject fishPrefab; // 떨어뜨릴 아이템 프리팹    
    public Transform fishDropPoint; // 아이템을 떨어뜨릴 위치

    public GameObject cowPrefab; // 떨어뜨릴 아이템 프리팹
    public Transform cowDropPoint;

    public GameObject chickenPrefab; // 떨어뜨릴 아이템 프리팹
    public Transform chickenDropPoint;

    public GameObject[] Crops; //떨어뜨릴 작물 아이템

    public bool isDrop = false;

    public float dropChance = 0.5f; // 아이템을 떨어뜨릴 확률 (0.0에서 1.0 사이)
    // private bool hasDropped = false; // 이미 아이템을 떨어뜨렸는지 확인하는 변수

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    void Update()
    {

    }

    public void FishDrop()
    {
        // 1에서 3까지 랜덤한 아이템 수를 설정
        int itemCount = Random.Range(1, 4);

        // 아이템을 떨어뜨릴 확률 계산 및 떨어뜨리기
        for (int i = 0; i < itemCount; i++)
        {
            if (Random.value <= dropChance)
            {
                Instantiate(fishPrefab, fishDropPoint.position, Quaternion.identity);
                Debug.Log("Drop");
                if(itemCount > 0)
                {
                    isDrop = true;
                }
            }
        }
    }

    public void CowDrop()
    {
        // 1에서 3까지 랜덤한 아이템 수를 설정
        int itemCount = Random.Range(1, 4);

        // 아이템을 떨어뜨릴 확률 계산 및 떨어뜨리기
        for (int i = 0; i < itemCount; i++)
        {
            if (Random.value <= dropChance)
            {
                Instantiate(cowPrefab, cowDropPoint.position, Quaternion.identity);
                Debug.Log("CowDrop");
            }
        }

    }

    public void ChickenDrop()
    {
        // 1에서 3까지 랜덤한 아이템 수를 설정
        int itemCount = Random.Range(1, 4);

        // 아이템을 떨어뜨릴 확률 계산 및 떨어뜨리기
        for (int i = 0; i < itemCount; i++)
        {
            if (Random.value <= dropChance)
            {
                Instantiate(chickenPrefab, chickenDropPoint.position, Quaternion.identity);
                Debug.Log("Drop");
            }
        }

    }

    public GameObject CropDrop(int num)
    {
        for (int i = 0; i < Crops.Length; i++)
        {
            if(num == i)
            {
                return Crops[i];
            }
        }
        return null;
    }
}
