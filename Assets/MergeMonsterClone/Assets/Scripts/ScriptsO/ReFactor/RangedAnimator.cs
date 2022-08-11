using UnityEngine;

public class RangedAnimator : MonoBehaviour
{
    [SerializeField] private Animator _rangedAnimator;

    private void Awake()
    {
        _rangedAnimator = GetComponent<Animator>();
        GetComponent<RangedAttack>().OnAttack += AttackAnimation;
    }

    public void AttackAnimation()
    {
        _rangedAnimator.SetTrigger("Attack");
    }
}
