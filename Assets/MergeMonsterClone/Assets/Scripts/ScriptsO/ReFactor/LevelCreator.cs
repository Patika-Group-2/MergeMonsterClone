using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance = null;

    [SerializeField] List<EnemyDataSO> _levels;
    private EnemyDataSO _currentLevel;
    private List<Character> _enemies;

    private int _maxLevel;
    public int MaxLevel { get => _maxLevel; private set => _maxLevel = value; }

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
        MaxLevel = _levels.Count;
        _currentLevel = _levels[(GameManager.Instance.CurrentLevel - 1)];
        _enemies = _currentLevel._enemyList;
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            int row = _currentLevel._rows[i];
            int column = _currentLevel._columns[i];

            Character enemy = _enemies[i];
            ICharacterGenerator go = enemy.GetComponent<ICharacterGenerator>();

            Tile tile = Testing3D.BoardGrid.EnemyTiles[row, column];
            Vector3 postion = tile.Get3DTilePosition();

            if (!tile.IsAvailable)
                continue;

            Instantiate(enemy);
            go.PositionCharacter(enemy.gameObject, postion, go.CharacterPrefab.transform.rotation);

            tile.IsAvailable = false;
        }
    }

    public void SetLevel()
    {
        _currentLevel = _levels[GameManager.Instance.CurrentLevel - 1];
        _enemies = _currentLevel._enemyList;
        MakeEnemyTilesAvailable();
        GenerateEnemies();
    }

    public void MakeEnemyTilesAvailable()
    {
        foreach (var tile in Testing3D.BoardGrid.EnemyTiles)
        {
            tile.IsAvailable = true;
        }
    }
}
