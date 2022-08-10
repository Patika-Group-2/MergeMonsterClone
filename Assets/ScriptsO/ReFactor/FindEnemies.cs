using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemies : FindTargetO
{
    public override void DelistOnDestroy()
    {
        EntityManagerO.Instance.Players.Remove(this.gameObject);
    }

    public override void FindTarget()
    {
        float closestDistance = Mathf.Infinity;

        if (EntityManagerO.Instance.Enemies != null)
        {
            foreach (GameObject go in
                EntityManagerO.Instance.Enemies)
            {
                float currentDistance;
                currentDistance = Vector3.Distance(
                    transform.position, go.transform.position);

                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    ClosestTarget = go.transform;
                }
            }
        }
    }
}
