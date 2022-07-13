using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnim : MonoBehaviour
{
    public Animator animator;
    private string currentState;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        animator.Play("Idle");
        animator.ResetTrigger("Attack");
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
