using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowState : MonoBehaviour
{
    Animator animator;
    Player thePlayer;
    int previousCowHappiness = 0;
    DropItem theDropItem;

    void Start()
    {
        animator = GetComponent<Animator>();
        thePlayer = FindObjectOfType<Player>();
        theDropItem = FindObjectOfType<DropItem>();
    }
    void Update()
    {
        // cowHappiness가 변경되었을 때만 애니메이션 상태 업데이트
        if (thePlayer.cowHappiness != previousCowHappiness)
        {
            UpdateAnimationState(thePlayer.cowHappiness);
            previousCowHappiness = thePlayer.cowHappiness; // cowHappiness 업데이트
        }

    }

    // cowHappiness에 따라 애니메이션 상태를 업데이트하는 함수
    void UpdateAnimationState(int cowHappiness)
    {
        // 이전 애니메이션 상태를 초기화
        HappyDone();
        Happy02Done();
        BadDone();
        Bad02Done();


        switch (cowHappiness)
        {
            case 0:
            case 1:
            case 2:
                Bad();
                // Debug.Log("Cow : Bad");
                break;
            case 3:
            case 4:
            case 5:
                Bad02();
                // Debug.Log("Cow : Bad02");
                break;
            case 6:
            case 7:
            case 8:
                Happy02();
                // Debug.Log("Cow : Happy02");
                break;
            default:
                Happy();
                // Debug.Log("Cow : Happy");
                break;
        }

    }

    public void Happy()
    {
        animator.SetBool("Happy", true);
        theDropItem.CowDrop();
        
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
