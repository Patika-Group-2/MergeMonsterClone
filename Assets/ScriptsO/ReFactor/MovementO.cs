using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementO : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] float _movementSpeed;
    public event Action<Transform> OnTargetClose;
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

            transform.position = Vector3.MoveTowards(transform.position,
               target.position, Time.deltaTime * _movementSpeed);
        }

        else
        {
            OnTargetClose?.Invoke(target);
        }
    }
}
