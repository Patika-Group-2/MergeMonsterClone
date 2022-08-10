using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayers : FindTargetO
{
    public override void DelistOnDestroy()
    {
        EntityManagerO.Instance.Enemies.Remove(this.gameObject);
    }

    public override void FindTarget()
    {
        
        float closestDistance = Mathf.Infinity;

        if (EntityManagerO.Instance.Players != null)
        {
            foreach (GameObject go in
                EntityManagerO.Instance.Players)
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
