using UnityEngine;

public class FindEnemies : FindTarget
{
    //if character dies remove it from the list
    public override void DelistOnDestroy()
    {
        //EntityManager.Instance.Players.Remove(this.gameObject);
        LevelCreator.Instance.Players.Remove(GetComponent<Character>());
    }

    public override void FindTargets()
    {
        float closestDistance = Mathf.Infinity;

        if (LevelCreator.Instance.Enemies != null)
        {
            foreach (Character go in
                LevelCreator.Instance.Enemies)
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
