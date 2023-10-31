using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsGrow : MonoBehaviour
{
    Animator animator;
    public bool GrowEnd = false;

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
        }
        else if (carrot)
        {
            animator.SetBool("carrot", true);
        }
        else if (parsnip)
        {
            animator.SetBool("parsnip", true);
        }
        else if (pumpkin)
        {
            animator.SetBool("pumpkin", true);
        }
    }

    IEnumerator FullGrow()
    {
        yield return new WaitForSeconds(21f);    //21초 대기
        GrowEnd = true;
    }
}
