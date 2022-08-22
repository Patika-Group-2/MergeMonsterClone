using UnityEngine.Pool;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private Bullet _shotPrefab;
    public ObjectPool<Bullet> _pool;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
        }

        _pool = new ObjectPool<Bullet>(Create, OnTakeBulletFromPool, OnReturnBulletToPool);
    }

    public Bullet Create()
    {
        var shot = Instantiate(_shotPrefab, transform.position, Quaternion.identity);
        shot.SetPool(_pool);
        return shot;
    }

    public void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    public void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
