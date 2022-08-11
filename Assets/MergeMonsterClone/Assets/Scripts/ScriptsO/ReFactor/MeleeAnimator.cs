using UnityEngine;

public class MeleeAnimator : MonoBehaviour
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
