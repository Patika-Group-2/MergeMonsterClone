using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManagerO : MonoBehaviour
{
    public static EntityManagerO Instance;
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

        foreach(GameObject go in enemies)
        {
            Enemies.Add(go);
        }
    }

    public void FindPlayers()
    {
        GameObject[] players =
            GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in players)
        {
            Players.Add(go);
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
