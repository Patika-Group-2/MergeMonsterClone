using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Target { get; set; }
    public int Damage { get; set; }

    [SerializeField] private float _speed;
    
    private void Start()
    {
        transform.LookAt(Target);
    }

    private void Update()
    {
        MoveTowardsTarget();
    }
    public void MoveTowardsTarget()
    {
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector3.Distance(transform.position,
            Target.position);

        transform.position = Vector3.MoveTowards(transform.position,
            Target.position, Time.deltaTime * _speed);

        if(distance <= 0.5f)
        {
            Destroy(gameObject);
            Character target = Target.GetComponent<Character>();
            target.TakeDamage(Damage);
        }
    }
}
