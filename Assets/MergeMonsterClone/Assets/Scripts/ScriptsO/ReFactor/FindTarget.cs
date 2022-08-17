using UnityEngine;

public abstract class FindTarget : MonoBehaviour
{
    public Transform ClosestTarget { get; protected set; }

    public abstract void FindTargets();
    public abstract void DelistOnDestroy();
}
