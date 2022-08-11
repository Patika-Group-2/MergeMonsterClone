using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    [SerializeField] private Bullet _shotPrefab;
    
    public void Fire(Transform target, int damage)
    {
        var shot = Instantiate(_shotPrefab, transform.position, Quaternion.identity);
        Bullet bullet = shot.GetComponent<Bullet>();
        bullet.Target = target;
        bullet.Damage = damage;
    }
}
