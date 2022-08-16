using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] public List<Character> _enemyList;
    [SerializeField] public List<int> _rows;
    [SerializeField] public List<int> _columns;
    [SerializeField] public int _coinWins;
}
