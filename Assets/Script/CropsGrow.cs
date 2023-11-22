using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsGrow : MonoBehaviour
{
    Animator animator;
    GameObject player;
    public bool GrowEnd = false;
    public int CropNo = -1;  //작물 drop할 때, 배열 번호


    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        CropNo = player.GetComponent<Player>().SetCropNo;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGrow());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FullGrow());
    }


    IEnumerator StartGrow()
    {
        yield return new WaitForSeconds(5f);    //5초 대기
        if (CropNo == 0)    //beetroot
        {
            animator.SetBool("beetroot", true);
        }
        else if (CropNo == 1)    //carrot
        {
            animator.SetBool("carrot", true);
        }
        else if (CropNo == 2)    //parsnip
        {
            animator.SetBool("parsnip", true);
        }
        else if (CropNo == 3)    //pumpkin
        {
            animator.SetBool("pumpkin", true);
        }

        else if(CropNo == -1)
        {
            Debug.Log("선택된 작물이 없음");
        }
    }

    IEnumerator FullGrow()
    {
        yield return new WaitForSeconds(26f);    //26초 대기
        GrowEnd = true;
    }
}
