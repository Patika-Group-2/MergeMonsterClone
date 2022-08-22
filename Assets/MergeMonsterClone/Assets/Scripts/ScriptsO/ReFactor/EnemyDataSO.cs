using System.Collections.Generic;
using UnityEngine;

//So to store level data
[CreateAssetMenu (fileName = "EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] public List<Character> _enemyList;
    [SerializeField] public List<int> _rows;
    [SerializeField] public List<int> _columns;
    //how much money will come in case of winning
    [SerializeField] public float _coinWins;
}
