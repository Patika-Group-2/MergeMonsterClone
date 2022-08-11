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
    public static List<Tile> TileList { get; set; }


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
            tile = _boardGrid.GetGridObject(hitCharacterbj.point);

            if (Input.GetMouseButtonDown(0))
            {
                _lastPickedTile = tile;

                _pickedCharacter = hitCharacterbj.transform.gameObject;
                _characterInstance = _pickedCharacter.GetComponent<ICharacterGenerator>();
                SetTileState(tile, true);
                return;
            }
        }

        if (groundHit)
        {
            tile = _boardGrid.GetGridObject(hitGroundObj.point);

            if (Input.GetMouseButtonDown(0))
            {
                if (tile == null) return;

                _lastPickedTile = tile;

                if (tile.IsAvailable)
                {
                    _pickedCharacter = null;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_pickedCharacter != null)
                {
                    _characterInstance.PositionCharacter(_pickedCharacter, hitGroundObj.point, Quaternion.identity);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_pickedCharacter == null)
                    return;

                tile = _boardGrid.GetGridObject(hitGroundObj.point);

                if (tile != null)
                {
                    if (!tile.IsAvailable)
                    {
                        _characterInstance.PositionCharacter(_pickedCharacter, GetTilePosition(_lastPickedTile), Quaternion.identity);
                        SetTileState(_lastPickedTile, false);
                    }
                    else
                    {
                        _characterInstance.PositionCharacter(_pickedCharacter, GetTilePosition(tile), Quaternion.identity);
                        SetTileState(tile, false);
                        if (_lastPickedTile != tile)
                            SetTileState(_lastPickedTile, true);
                    }
                }
                else
                {
                    _characterInstance.PositionCharacter(_pickedCharacter, GetTilePosition(_lastPickedTile), Quaternion.identity);
                    SetTileState(_lastPickedTile, false);
                }

                _pickedCharacter = null;
            }
        }
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
