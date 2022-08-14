using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance = null;

    [SerializeField] List<EnemyDataSO> _levels;
    private EnemyDataSO _currentLevel;

    public PlayerDataSO Data;

    [SerializeField] private List<Character> _enemies;
    [SerializeField] private List<Character> _players;

    public List<Character> Players => _players;
    public List<Character> Enemies => _enemies;

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
        _players = new List<Character>();
        _enemies = new List<Character>();
        GenerateEnemies();
        GeneratePlayers();
    }

    private void Update()
    {
        //This code will be added to OnApplicationQuit()
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadPlayerSO();
        }
    }

    public void GenerateEnemies()
    {
        _enemies.Clear();

        for (int i = 0; i < _currentLevel._enemyList.Count; i++)
        {
            int row = _currentLevel._rows[i];
            int column = _currentLevel._columns[i];

            Tile tile = Testing3D.BoardGrid.EnemyTiles[row, column];

            if (!tile.IsAvailable)
                continue;

            Vector3 postion = tile.Get3DTilePosition();

            Character enemy = Instantiate(_currentLevel._enemyList[i]);
            ICharacterGenerator go = enemy.GetComponent<ICharacterGenerator>();

            go.PositionCharacter(enemy.gameObject, postion, go.CharacterPrefab.transform.rotation);
            _enemies.Add(enemy);

            tile.IsAvailable = false;
        }
    }

    public void GeneratePlayers()
    {
        _players.Clear();
        
        for (int i = 0; i < Data.PlayerEntities.Count; i++)
        {
            int row = Data.Rows[i];
            int column = Data.Columns[i];

            Tile tile = Testing3D.BoardGrid.PlayerTiles[row, column];

            if (!tile.IsAvailable)
                continue;

            Vector3 postion = tile.Get3DTilePosition();

            Character player = Instantiate(Data.PlayerEntities[i]);
            SetCharacterTileID(player, row, column);

            ICharacterGenerator go = player.GetComponent<ICharacterGenerator>();
            go.PositionCharacter(player.gameObject, postion, go.CharacterPrefab.transform.rotation);

            _players.Add(player);

            tile.IsAvailable = false;
        }
    }

    public void SetCharacterTileID(Character ch, int row, int column)
    {
        ch.Row = row;
        ch.Column = column;
    }

    //This func will be invoked with Win or Lose Screen Buttons
    public void SetLevel()
    {
        _currentLevel = _levels[GameManager.Instance.CurrentLevel - 1];
        MakeEnemyTilesAvailable();
        MakePlayerTilesAvailable();
        ClearBoard();
        GenerateEnemies();
        GeneratePlayers();
    }

    public void MakeEnemyTilesAvailable()
    {
        foreach (var tile in Testing3D.BoardGrid.EnemyTiles)
        {
            tile.IsAvailable = true;
        }
    }

    public void MakePlayerTilesAvailable()
    {
        foreach (var tile in Testing3D.BoardGrid.PlayerTiles)
        {
            tile.IsAvailable = true;
        }
    }

    public void ClearPlayerDataOfSO()
    {
        Data.Columns.Clear();
        Data.Rows.Clear();
        Data.PlayerEntities.Clear();
    }

    public void LoadPlayerSO()
    {
        ClearPlayerDataOfSO();

        foreach (var player in _players)
        {
            GameObject prefab = Resources.Load(player.PrefabPath) as GameObject;

            Data.Rows.Add(player.Row);
            Data.Columns.Add(player.Column);
            Data.PlayerEntities.Add(prefab.GetComponent<Character>());
        }
    }
    
    public void ClearBoard()
    {
        foreach(var player in _players)
        {
            if(player != null)
            {
                Destroy(player.gameObject);
            }
        }

        foreach(var enemy in _enemies)
        {
            if(enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
