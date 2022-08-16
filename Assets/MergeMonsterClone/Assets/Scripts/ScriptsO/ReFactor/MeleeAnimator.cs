using UnityEngine;

public class MeleeAnimator : MonoBehaviour
{
    [SerializeField] private Animator _meleeAnimator;

    private void Awake()
    {
        _meleeAnimator = GetComponent<Animator>();
        GetComponent<MeleeAttack>().OnAttack += AttackAnimation;
        GetComponent<Movement>().OnMove += Move;
        GetComponent<Movement>().OnTargetClose += Stop;
    }

    public void AttackAnimation()
    {
        _meleeAnimator.SetTrigger("Attack");
    }

    void Move()
    {
        _meleeAnimator.SetBool("IsMoving", true);
    }

    void Stop(Transform target)
    {
        _meleeAnimator.SetBool("IsMoving", false);
    }
}
