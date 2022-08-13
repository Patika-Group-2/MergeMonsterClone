using UnityEngine;

public class FindEnemies : FindTarget
{
    public override void DelistOnDestroy()
    {
        EntityManager.Instance.Players.Remove(this.gameObject);
    }

    public override void FindTargets()
    {
        float closestDistance = Mathf.Infinity;

        if (EntityManager.Instance.Enemies != null)
        {
            foreach (GameObject go in
                EntityManager.Instance.Enemies)
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
