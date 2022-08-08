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

    float _characterOffset;

    GameObject _pickedGameObject;

    BoardGrid3D<Tile> _boardGrid;
    Tile _lastPickedTile;


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
                GameObject spawnedTile = InstantiateGameObject(_boardTilePrefab, _boardGrid.GetWorldPosition(i, j), Quaternion.Euler(90, 0, 0));
                spawnedTile.name = $"Tile3D [{i}, {j}]";
                spawnedTile.transform.SetParent(tileContainer.transform);
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tile tile;

        bool characterHit = Physics.Raycast(ray, out RaycastHit hitCharacterObj, 1000f, _characterLayerMask);
        bool groundHit = Physics.Raycast(ray, out RaycastHit hitGroundObj, 1000f, _groundLayerMask);

        if (characterHit)
        {
            tile = _boardGrid.GetGridObject(hitCharacterObj.point);

            if (Input.GetMouseButtonDown(0))
            {
                _lastPickedTile = tile;

                tile = _boardGrid.GetGridObject(hitCharacterObj.point);
                _pickedGameObject = hitCharacterObj.transform.gameObject;
                _characterOffset = _pickedGameObject.GetComponent<MeshRenderer>().bounds.size.y / 2;
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
                    _boardGrid.GetRowAndColumn(hitGroundObj.point, out int row, out int column);
                    _characterOffset = _characterPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;
                    InstantiateGameObject(_characterPrefab, _boardGrid.GetWorldPosition(row, column) + Vector3.up * _characterOffset, Quaternion.identity);
                    SetTileState(tile, false);
                    _pickedGameObject = null;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_pickedGameObject != null)
                {
                    SetObjectPosition(_pickedGameObject, hitGroundObj.point, _characterOffset);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_pickedGameObject == null)
                    return;

                tile = _boardGrid.GetGridObject(hitGroundObj.point);

                if (tile != null)
                {
                    if (!tile.IsAvailable)
                    {
                        SetObjectPosition(_pickedGameObject, GetTilePosition(_lastPickedTile), _characterOffset);
                        SetTileState(_lastPickedTile, false);
                    }
                    else
                    {
                        SetObjectPosition(_pickedGameObject, GetTilePosition(tile), _characterOffset);
                        SetTileState(tile, false);
                        if (_lastPickedTile != tile)
                            SetTileState(_lastPickedTile, true);
                    }
                }
                else
                {
                    SetObjectPosition(_pickedGameObject, GetTilePosition(_lastPickedTile), _characterOffset);
                    SetTileState(_lastPickedTile, false);
                }

                _pickedGameObject = null;
            }
        }
    }

    void SetObjectPosition(GameObject obj, Vector3 pos, float offset)
    {
        obj.transform.position = pos + Vector3.up * offset;
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

    GameObject InstantiateGameObject(GameObject obj, Vector3 pos, Quaternion angle)
    {
        return Instantiate(obj, pos, angle);
    }
}
