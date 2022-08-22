using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] float _movementSpeed;

    public event Action<Transform> OnTargetClose;
    public event Action OnMove;

    private float _distance;

    public void Move(Transform target)
    {
        if (target == null)
            return;

        _distance = Vector3.Distance(transform.position,
            target.position);

        if (_distance > _attackRange)
        {
            transform.LookAt(target);
            //call event
            OnMove?.Invoke();

            transform.position = Vector3.MoveTowards(transform.position,
               target.position, Time.deltaTime * _movementSpeed);
        }

        //if target in attackRange call OnTargetClose event
        else
        {
            OnTargetClose?.Invoke(target);
        }
    }
}
