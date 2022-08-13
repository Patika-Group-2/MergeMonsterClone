using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHitPoint = 100;
    [SerializeField] private int _hitPoints;
    [SerializeField] private HealthBar _healthBar;
    

    private void Awake()
    {
        _hitPoints = _maxHitPoint;
        _healthBar = GetComponentInChildren<HealthBar>();
        
    }

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;
        float percentage = (float)_hitPoints / (float)_maxHitPoint;
        StartCoroutine(_healthBar.ChangeHealthBar(percentage));

        if (_hitPoints <= 0 && this.gameObject.tag == "Enemy")
        {
            DieEnemy();
        }
        else if(_hitPoints <= 0 && this.gameObject.tag != "Enemy")
        {
            DieFriend();
        }
    }

    private void DieFriend()
    {
        Destroy(gameObject);
    }
    private void DieEnemy()
    {
        
        Destroy(gameObject);
    }
    

}
