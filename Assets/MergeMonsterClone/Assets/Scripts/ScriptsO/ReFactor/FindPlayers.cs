using UnityEngine;

public class FindPlayers : FindTarget
{
    public override void DelistOnDestroy()
    {
        EntityManager.Instance.Enemies.Remove(this.gameObject);
    }

    public override void FindTargets()
    {
        
        float closestDistance = Mathf.Infinity;

        if (EntityManager.Instance.Players != null)
        {
            foreach (GameObject go in
                EntityManager.Instance.Players)
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
