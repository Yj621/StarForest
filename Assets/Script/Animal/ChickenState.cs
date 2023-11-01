using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState : MonoBehaviour
{
    Animator animator;
    Player thePlayer;
    int previousChickenHappiness = 0;
    DropItem theDropItem;

    void Start()
    {
        animator = GetComponent<Animator>();
        thePlayer = FindObjectOfType<Player>();
        //Debug.Log("thePlayer.chickenHappiness in start() : " + thePlayer.chickenHappiness);
        theDropItem = FindObjectOfType<DropItem>();

    }

    void Update()
    {
        if (thePlayer != null)
        {
            //Debug.Log("the player state : "+thePlayer.gameObject);
        }
        //Debug.Log("thePlayer.chickenHappiness : " + thePlayer.chickenHappiness);
        // cowHappiness가 변경되었을 때만 애니메이션 상태 업데이트
        if (thePlayer.chickenHappiness != previousChickenHappiness)
        {
            UpdateAnimationState(thePlayer.chickenHappiness);
            previousChickenHappiness = thePlayer.chickenHappiness; // chickenHappiness 업데이트

        }

    }

    // cowHappiness에 따라 애니메이션 상태를 업데이트하는 함수
    void UpdateAnimationState(int chickenHappiness)
    {
        // 이전 애니메이션 상태를 초기화
        HappyDone();
        Happy02Done();
        BadDone();
        Bad02Done();


        switch (chickenHappiness)
        {
            case 0:
            case 1:
            case 2:
                Bad();
                Debug.Log("Chicken : Bad");
                break;
            case 3:
            case 4:
            case 5:
                Bad02();
                Debug.Log("Chicken : Bad02");
                break;
            case 6:
            case 7:
            case 8:
                Happy02();
                Debug.Log("Chicken : Happy02");
                break;
            default:
                Happy();
                Debug.Log("Chicken : Happy");
                break;
        }

    }

    // 나머지 애니메이션 함수는 그대로 유지

    // 나머지 애니메이션 함수는 그대로 유지

    public void Happy()
    {
        animator.SetBool("Happy", true);
        theDropItem.ChickenDrop();
    }
    public void HappyDone()
    {
        animator.SetBool("Happy", false);
    }

    public void Happy02()
    {
        animator.SetBool("Happy02", true);
    }
    public void Happy02Done()
    {
        animator.SetBool("Happy02", false);
    }

    public void Bad02()
    {
        animator.SetBool("Bad02", true);
    }
    public void Bad02Done()
    {
        animator.SetBool("Bad02", false);
    }

    public void Bad()
    {
        animator.SetBool("Bad", true);
    }
    public void BadDone()
    {
        animator.SetBool("Bad", false);
    }

}