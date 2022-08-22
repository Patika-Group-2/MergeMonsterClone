using System.Collections.Generic;
using UnityEngine;

public class Testing3D : MonoBehaviour
{
    [SerializeField] int _gridWidth = 8;
    [SerializeField] int _gridHeight = 8;
    [SerializeField] float _cellSize = 1.0f;

    [SerializeField] Vector3 _originPosition = Vector3.zero;

    [SerializeField] GameObject _boardTilePrefab;

    [SerializeField] LayerMask _characterLayerMask;
    [SerializeField] LayerMask _groundLayerMask;

    ICharacterGenerator _characterInstance;
    GameObject _pickedCharacter;
    Tile _lastPickedTile;

    static BoardGrid3D<Tile> _boardGrid;

    public static BoardGrid3D<Tile> BoardGrid => _boardGrid;

    public static List<Tile> TileList { get; set; }


    void Awake()
    {
        _boardGrid = new BoardGrid3D<Tile>(_gridWidth, _gridHeight, _cellSize, _originPosition, CreateNode);
        GameObject tileContainer = GameObject.Find("TileContainer");
        if (tileContainer == null)
            tileContainer = new GameObject("TileContainer");

        for (int i = 0; i < _boardGrid.GridWidth / 2; i++)
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
        //prevent to set character's position from fight screen
        if (Camera.main.name != "MergeCamera")
            return;

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

                    if (tile == null || tile.Row > 3)
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
                        //Set character's tile id if character moved this tile
                        Character ch = _pickedCharacter.GetComponent<Character>();
                        LevelCreator.Instance.SetCharacterTileID(ch, tile.Row, tile.Column);

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
                LevelCreator.Instance.Players.Remove(_pickedCharacter.GetComponent<Character>());
                LevelCreator.Instance.Players.Remove(tile.TileObject.GetComponent<Character>());
                LevelCreator.Instance.Players.Add(MergedGO.GetComponent<Character>());
                Destroy(_pickedCharacter);
                Destroy(tile.TileObject);

                //add row column to new created character
                _pickedCharacter = MergedGO;
                _characterInstance = _pickedCharacter.GetComponentInParent<ICharacterGenerator>();
                _characterInstance.PositionCharacter(GetTilePosition(tile), _characterInstance.CharacterPrefab.transform.rotation);
                LevelCreator.Instance.SetCharacterTileID(MergedGO.GetComponent<Character>(), tile.Row, tile.Column);
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
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Humans/Human2Ally") as GameObject);
                    break;
                case ("HumanLevel1"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Humans/Human3Ally") as GameObject);
                    break;
                case ("HumanLevel2"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Humans/Human4Ally") as GameObject);
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
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Dragons/Green_Dragon_4") as GameObject);
                    break;
                case ("HumanLevel1"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Dragons/Orange_Dragon_3") as GameObject);
                    break;
                case ("HumanLevel2"):
                    MergedCharacter = Instantiate(Resources.Load("Prefabs/Ally/Dragons/Red_Dragon_2") as GameObject);
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