using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimatorO : MonoBehaviour
{
    [SerializeField] private Animator _meleeAnimator;

    private void Awake()
    {
        _meleeAnimator = GetComponent<Animator>();
        GetComponent<MeleeAttack>().OnAttack += AttackAnimation;
    }

    public void AttackAnimation()
    {
        _meleeAnimator.SetTrigger("Attack");
    }
}
