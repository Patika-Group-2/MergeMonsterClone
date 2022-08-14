using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [SerializeField] public List<Character> PlayerEntities;
    [SerializeField] public List<int> Rows;
    [SerializeField] public List<int> Columns;

}
