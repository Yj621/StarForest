using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    Player thePlayer;
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        thePlayer = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        sprite.flipX = true;
    }

    public void SpaceBar()
    {
        animator.SetBool("Space", true);
    }
    public void SpaceBarDone()
    {
        animator.SetBool("Space", false);
    }
    public void Progressbar()
    {        
        animator.SetBool("Fish", true); 
    }
    public void ProgressbarDone()
    {        
        animator.SetBool("Fish", false);
    }

}
