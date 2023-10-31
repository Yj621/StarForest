using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsGrow : MonoBehaviour
{
    Animator animator;
    public bool GrowEnd = false;
    public int CropNo = -1;  //작물 drop할 때, 배열 번호

    //애니메이션
    public bool beetroot = false;
    public bool carrot = false; //일단 이거로
    public bool parsnip = false;
    public bool pumpkin = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
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
        if (beetroot)
        {
            animator.SetBool("beetroot", true);
            CropNo = 0;
        }
        else if (carrot)
        {
            animator.SetBool("carrot", true);
            CropNo = 1;
        }
        else if (parsnip)
        {
            animator.SetBool("parsnip", true);
            CropNo = 2;
        }
        else if (pumpkin)
        {
            animator.SetBool("pumpkin", true);
            CropNo = 3;
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
