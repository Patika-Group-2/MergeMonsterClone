using System.Collections.Generic;
using UnityEngine;

public class Testing2D : MonoBehaviour
{
    [SerializeField] int _gridWidth = 10;
    [SerializeField] int _gridHeight = 10;
    [SerializeField] float _cellSize = 1.0f;

    [SerializeField] GameObject _boardTilePrefab;
    [SerializeField] GameObject _characterObject;
    [SerializeField] Vector3 _originPosition = Vector3.zero;
    [SerializeField] LayerMask _gameObjectLayerMask;

    BoardGrid2D<Tile> _boardGrid;

    GameObject _handledGameObject;
    Tile _lastPickedTile;


    void Awake()
    {
        _boardGrid = new BoardGrid2D<Tile>(_gridWidth, _gridHeight, _cellSize, _originPosition, CreateTile);
        GameObject tileContainer = GameObject.Find("TileContainer");
        if (tileContainer == null)
            tileContainer = new GameObject("TileContainer");

        for (int i = 0; i < _boardGrid.GridWidth; i++)
        {
            for (int j = 0; j < _boardGrid.GridHeight; j++)
            {
                var spawnedTile = Instantiate(_boardTilePrefab, _boardGrid.GetWorldPosition(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile2D {i} {j}";
                spawnedTile.transform.SetParent(tileContainer.transform);
            }
        }
    }

    void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = _boardGrid.GetGridObject(mouseWorldPosition);

            if (tile != null)
            {
                _lastPickedTile = tile;

                if (tile.IsAvailable == true)
                {
                    var obj = Instantiate(_characterObject, (Vector2)tile.Get2DTilePosition(), Quaternion.identity);
                    tile.IsAvailable = false;
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _gameObjectLayerMask))
                    {
                        _handledGameObject = hit.transform.gameObject;
                        tile.IsAvailable = true;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_handledGameObject != null)
                _handledGameObject.transform.position = mouseWorldPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Tile tile = _boardGrid.GetGridObject(mouseWorldPosition);

            if (_handledGameObject == null)
                return;

            if (tile != null)
            {
                if (!tile.IsAvailable)
                {
                    _handledGameObject.transform.position = GetTilePosition(_lastPickedTile);
                    SetTileState(_lastPickedTile, false);
                }
                else
                {
                    _handledGameObject.transform.position = GetTilePosition(tile);
                    SetTileState(tile, false);
                    if (_lastPickedTile != tile)
                        SetTileState(_lastPickedTile, true);
                }
            }
            else
            {
                _handledGameObject.transform.position = GetTilePosition(_lastPickedTile);
                SetTileState(_lastPickedTile, false);
            }

            _handledGameObject = null;
        }
    }

    void SetTileState(Tile tile, bool state)
    {
        tile.IsAvailable = state;
    }

    Vector3 GetTilePosition(Tile tile)
    {
        return tile.Get2DTilePosition();
    }

    Tile CreateTile(BoardGrid2D<Tile> grid, int row, int column)
    {
        return new Tile(grid, row, column);
    }

    GameObject CreateGameObject(BoardGrid2D<GameObject> gameObject, int row, int column)
    {
        Vector3 objectPosition = new Vector3(row, column) + Vector3.one * _cellSize * .5f;
        return Instantiate(_boardTilePrefab, objectPosition, Quaternion.identity);
    }
}
