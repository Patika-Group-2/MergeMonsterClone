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

    public EnemyDataSO CurrentLevel => _currentLevel;

    private int _maxLevel;

    //this variables are being used to calculate WinScreen star calculation
    private int _playerCountAtBeginning;
    private int _playerCountAtEnd;

    public int PlayerCountAtBeginning { get => _playerCountAtBeginning; private set => _playerCountAtBeginning = value; }
    public int PlayerCountAtEnd { get => _playerCountAtEnd; private set => _playerCountAtEnd = value; }

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

    //Instantiate enemy units
    public void GenerateEnemies()
    {
        _enemies.Clear();

        for (int i = 0; i < _currentLevel._enemyList.Count; i++)
        {
            int row = _currentLevel._rows[i];
            int column = _currentLevel._columns[i];

            //Get correct tile from EnemyTile list
            Tile tile = Testing3D.BoardGrid.EnemyTiles[row, column];

            if (!tile.IsAvailable)
                continue;
            //Get world position from tile
            Vector3 postion = tile.Get3DTilePosition();
            
            //Instantiate character
            Character enemy = Instantiate(_currentLevel._enemyList[i]);
            ICharacterGenerator go = enemy.GetComponent<ICharacterGenerator>();

            //position character
            go.PositionCharacter(postion, go.CharacterPrefab.transform.rotation);
            //Add character to the dynamic enemy list
            _enemies.Add(enemy);

            tile.IsAvailable = false;
        }
    }

    //Instantiate player units
    public void GeneratePlayers()
    {
        _players.Clear();

        for (int i = 0; i < Data.PlayerEntities.Count; i++)
        {
            int row = Data.Rows[i];
            int column = Data.Columns[i];

            //Get correct tile from EnemyTile list
            Tile tile = Testing3D.BoardGrid.PlayerTiles[row, column];

            if (!tile.IsAvailable)
                continue;

            //Get world position from tile
            Vector3 postion = tile.Get3DTilePosition();

            //Instantiate character 
            Character player = Instantiate(Data.PlayerEntities[i]);
            SetCharacterTileID(player, row, column);

            //Position character
            ICharacterGenerator go = player.GetComponent<ICharacterGenerator>();
            go.PositionCharacter(postion, go.CharacterPrefab.transform.rotation);

            tile.TileObject = player.gameObject;
            //Add character to the dynamic player list
            _players.Add(player);

            tile.IsAvailable = false;
        }
    }

    //Set the given character's row and column id 
    public void SetCharacterTileID(Character ch, int row, int column)
    {
        ch.Row = row;
        ch.Column = column;
    }

    //Make evety tile avaliable, clear board then generate enemy and players from SO
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

    //Clear player SO data
    public void ClearPlayerDataOfSO()
    {
        Data.Columns.Clear();
        Data.Rows.Clear();
        Data.PlayerEntities.Clear();
    }

    //Store players from dynamic player list to the SO
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
        foreach (var player in _players)
        {
            if (player != null)
            {
                Destroy(player.gameObject);
            }
        }

        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    //This func used to calculate stars
    public void SetPlayerCountAtBegin()
    {
        PlayerCountAtBeginning = _players.Count;
    }

    //This func used to calculate stars
    public void SetPlayerCountAtEnd()
    {
        PlayerCountAtEnd = _players.Count;
    }

    //While game is running do not load SO
    private void OnApplicationQuit()
    {
        if(!GameManager.Instance.GameIsRunning)
        LoadPlayerSO();
    }
}
