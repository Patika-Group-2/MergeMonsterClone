using System.Collections.Generic;
using UnityEngine;

public class Testing3D : MonoBehaviour
{
    [SerializeField] int _gridWidth = 8;
    [SerializeField] int _gridHeight = 8;
    [SerializeField] float _cellSize = 1.0f;

    [SerializeField] Vector3 _originPosition = Vector3.zero;

    [SerializeField] GameObject _boardTilePrefab;
    [SerializeField] GameObject _characterPrefab;

    [SerializeField] LayerMask _characterLayerMask;
    [SerializeField] LayerMask _groundLayerMask;

    ICharacterGenerator _characterInstance;
    GameObject _pickedCharacter;
    Tile _lastPickedTile;

    static BoardGrid3D<Tile> _boardGrid;

    public static BoardGrid3D<Tile> BoardGrid => _boardGrid;

    public static List<Tile> AllyTileList { get; set; }
    public static List<Tile> EnemyTileList { get; set; }


    void Awake()
    {
        _boardGrid = new BoardGrid3D<Tile>(_gridWidth, _gridHeight, _cellSize, _originPosition, CreateNode);
        GameObject tileContainer = GameObject.Find("TileContainer");
        if (tileContainer == null)
            tileContainer = new GameObject("TileContainer");

        for (int i = 0; i < _boardGrid.GridWidth; i++)
        {
            for (int j = 0; j < _boardGrid.GridHeight; j++)
            {
                GameObject spawnedTile = Instantiate(_boardTilePrefab, _boardGrid.GetWorldPosition(i, j), Quaternion.Euler(90, 0, 0));
                spawnedTile.name = $"Tile3D [{i}, {j}]";
                spawnedTile.transform.SetParent(tileContainer.transform);
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tile tile;

        bool characterHit = Physics.Raycast(ray, out RaycastHit hitCharacterbj, 1000f, _characterLayerMask);
        bool groundHit = Physics.Raycast(ray, out RaycastHit hitGroundObj, 1000f, _groundLayerMask);

        if (characterHit)
        {
            tile = _boardGrid.GetGridObject(hitCharacterbj.transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                if (tile != null)
                {
                    _lastPickedTile = tile;
                    tile.TileObject = null;
                    _pickedCharacter = hitCharacterbj.transform.gameObject;
                    _characterInstance = _pickedCharacter.GetComponent<ICharacterGenerator>();

                    SetTileState(tile, true);
                    return;
                }
            }
        }

        if (groundHit)
        {
            {
                tile = _boardGrid.GetGridObject(hitGroundObj.point);

                if (Input.GetMouseButton(0))
                {
                    if (_pickedCharacter != null)
                    {
                        _characterInstance.PositionCharacter(hitGroundObj.point, _characterInstance.CharacterPrefab.transform.rotation);
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (_pickedCharacter == null)
                        return;

                    tile = _boardGrid.GetGridObject(hitGroundObj.point);

                    if (tile == null)
                    {
                        _characterInstance.PositionCharacter(GetTilePosition(_lastPickedTile), _characterInstance.CharacterPrefab.transform.rotation);
                        SetTileState(_lastPickedTile, false);
                        _lastPickedTile.TileObject = _pickedCharacter;
                        _pickedCharacter = null;
                        return;
                    }

                    if (!tile.IsAvailable)
                    {
                        if (_pickedCharacter.tag == tile.TileObject.tag)
                        {
                            MergeCheck(tile);
                        }
                        else
                        {
                            _characterInstance.PositionCharacter(GetTilePosition(_lastPickedTile), _characterInstance.CharacterPrefab.transform.rotation);
                            SetTileState(_lastPickedTile, false);
                            _lastPickedTile.TileObject = _pickedCharacter;
                        }
                    }
                    else
                    {
                        _characterInstance.PositionCharacter(GetTilePosition(tile), _characterInstance.CharacterPrefab.transform.rotation);
                        SetTileState(tile, false);
                        tile.TileObject = _pickedCharacter;
                        if (_lastPickedTile != tile)
                            SetTileState(_lastPickedTile, true);
                    }

                    _pickedCharacter = null;
                }
            }
        }
    }

    void MergeCheck(Tile tile)
    {
        if (tile != _lastPickedTile)
        {
            GameObject MergedGO = GetMergedCharacter(_pickedCharacter.tag, tile);
            if (MergedGO == null)
            {
                _characterInstance.PositionCharacter(GetTilePosition(_lastPickedTile), _characterInstance.CharacterPrefab.transform.rotation);
                SetTileState(_lastPickedTile, false);
                _lastPickedTile.TileObject = _pickedCharacter;
            }
            else
            {

                Destroy(_pickedCharacter);
                Destroy(tile.TileObject);

                _pickedCharacter = MergedGO;
                _characterInstance = _pickedCharacter.GetComponentInParent<ICharacterGenerator>();
                _characterInstance.PositionCharacter(GetTilePosition(tile), _characterInstance.CharacterPrefab.transform.rotation);
                tile.TileObject = MergedGO;
                SetTileState(_lastPickedTile, true);
            }
        }
    }

    GameObject GetMergedCharacter(string mergeTag, Tile tile)
    {
        GameObject MergedCharacter;
        if (_characterInstance is MeleeGenerator && tile.TileObject.GetComponent<ICharacterGenerator>() is MeleeGenerator)
        {
            switch (mergeTag)
            {
                case ("HumanLevel0"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/PrebasO/Player 1") as GameObject);
                    break;
                case ("HumanLevel1"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/PrebasO/Player 2") as GameObject);
                    break;
                case ("HumanLevel2"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/PrebasO/Player 3") as GameObject);
                    break;
                default:
                    MergedCharacter = null;
                    break;
            }
        }
        else if (_characterInstance is RangedGenerator && tile.TileObject.GetComponent<ICharacterGenerator>() is RangedGenerator)
        {
            switch (mergeTag)
            {
                case ("HumanLevel0"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Dragons/Red_Dragon_2") as GameObject);
                    break;
                case ("HumanLevel1"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Dragons/Orange_Dragon_3") as GameObject);
                    break;
                default:
                    MergedCharacter = null;
                    break;
            }
        }
        else
            MergedCharacter = null;

        return MergedCharacter;
    }

    void SetTileState(Tile tile, bool state)
    {
        tile.IsAvailable = state;
    }

    Vector3 GetTilePosition(Tile tile)
    {
        return tile.Get3DTilePosition();
    }

    Tile CreateNode(BoardGrid3D<Tile> grid, int row, int column)
    {
        return new Tile(grid, row, column);
    }
}