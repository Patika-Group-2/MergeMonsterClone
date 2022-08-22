using UnityEngine;

public class FindPlayers : FindTarget
{
    //if character dies remove it from the list
    public override void DelistOnDestroy()
    {
        //EntityManager.Instance.Enemies.Remove(this.gameObject);
        LevelCreator.Instance.Enemies.Remove(GetComponent<Character>());
    }

    public override void FindTargets()
    {
        
        float closestDistance = Mathf.Infinity;

        if (LevelCreator.Instance.Players != null)
        {
            foreach (Character go in
                LevelCreator.Instance.Players)
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
