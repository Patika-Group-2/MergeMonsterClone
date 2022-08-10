using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAnimatorO : MonoBehaviour
{
    [SerializeField] private Animator _rangedAnimator;

    private void Awake()
    {
        _rangedAnimator = GetComponent<Animator>();
        GetComponent<RangedAttackO>().OnAttack += AttackAnimation;
    }

    public void AttackAnimation()
    {
        _rangedAnimator.SetTrigger("Attack");
    }
}
