using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterO : MonoBehaviour
{

    public abstract void FindClosestTarget();
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
}
