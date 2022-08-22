using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public Transform Target { get; set; }
    public int Damage { get; set; }

    [SerializeField] private float _speed;

    IObjectPool<Bullet> _pool;

    private void Start()
    {
        transform.LookAt(Target);
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    public void MoveTowardsTarget()
    {
        //if target dies when bullet in the air, destroy bullet
        if (Target == null)
        {
            _pool.Release(this);
            return;
        }

        float distance = Vector3.Distance(transform.position,
            Target.position);

        transform.position = Vector3.MoveTowards(transform.position,
            Target.position, Time.deltaTime * _speed);

        if (distance <= 0.5f)
        {
            _pool.Release(this);
            Destroy(Instantiate(Resources.Load("FX/Particles/Particlee"), transform.position, Quaternion.Euler(-90, -90, 0)), 2f);
            
            //Reference to target character to apply damage
            Character target = Target.GetComponent<Character>();
            target.TakeDamage(Damage);
        }
    }
}
