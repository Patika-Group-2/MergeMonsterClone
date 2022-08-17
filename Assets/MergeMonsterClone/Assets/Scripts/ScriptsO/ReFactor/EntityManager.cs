using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] int _playerTypeCount = 4;
    public static EntityManager Instance;
    public List<GameObject> Players = null;
    public List<GameObject> Enemies = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //FindEnemies();
        //FindPlayers();
    }

    public void FindEnemies()
    {
        GameObject[] enemies =
             GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in enemies)
        {
            Enemies.Add(go);
        }
    }

    public void FindPlayers()
    {
        for (int i = 0; i < _playerTypeCount; i++)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("HumanLevel" + i.ToString());
            Players.AddRange(player);
        }
    }

    public void SetEntityList()
    {
        FindEnemies();
        FindPlayers();
    }

    public void ClearLists()
    {
        Enemies.Clear();
        Players.Clear();
    }
}
