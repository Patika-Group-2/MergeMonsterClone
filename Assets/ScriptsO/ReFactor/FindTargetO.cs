using System;
using UnityEngine;

public abstract class FindTargetO : MonoBehaviour
{
    public Transform ClosestTarget { get; protected set; }

    public abstract void FindTarget();
    public abstract void DelistOnDestroy();
}
