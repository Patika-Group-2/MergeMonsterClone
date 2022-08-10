using UnityEngine;
using System;

public class BoardGrid2D<TGridObject>
{
    int _gridWidth;
    int _gridHeight;
    float _cellSize;
    Vector3 _originPosition;

    TGridObject[,] _gridTiles;

    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;
    public float CellSize => _cellSize;
    public Vector3 OriginPosition => _originPosition;


    public BoardGrid2D(int width, int height, float cellSize, Vector3 originPosition, Func<BoardGrid2D<TGridObject>, int, int, TGridObject> createGridObject)
    {
        if (width < 0 || height < 0)
            return;

        _gridWidth = width;
        _gridHeight = height;
        _cellSize = cellSize;
        _originPosition = originPosition;

        _gridTiles = new TGridObject[width, height];

        for (int i = 0; i < _gridTiles.GetLength(0); i++)
        {
            for (int j = 0; j < _gridTiles.GetLength(1); j++)
            {
                _gridTiles[i, j] = createGridObject(this, i, j);

                Debug.DrawLine(GetWorldPosition(i, j) - Vector3.one * _cellSize / 2, GetWorldPosition(i, j + 1) - Vector3.one * _cellSize / 2, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j) - Vector3.one * _cellSize / 2, GetWorldPosition(i + 1, j) - Vector3.one * _cellSize / 2, Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(width, 0) - Vector3.one * _cellSize / 2, GetWorldPosition(width, height) - Vector3.one * _cellSize / 2, Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, height) - Vector3.one * _cellSize / 2, GetWorldPosition(width, height) - Vector3.one * _cellSize / 2, Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int row, int column)
    {
        return new Vector3(row, column) * _cellSize + _originPosition + Vector3.one * _cellSize / 2;
    }

    public void GetRowAndColumn(Vector3 worldPosition, out int row, out int column)
    {
        row = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        column = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }

    public void SetGridObject(int row, int column, TGridObject obj)
    {
        if (row >= 0 && column >= 0 && row < _gridWidth && column < _gridHeight)
        {
            _gridTiles[row, column] = obj;
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject obj)
    {
        int row, column;
        GetRowAndColumn(worldPosition, out row, out column);
        SetGridObject(row, column, obj);
    }

    public TGridObject GetGridObject(int row, int column)
    {
        if (row >= 0 && column >= 0 && row < _gridWidth && column < _gridHeight)
        {
            return _gridTiles[row, column];
        }
        else
            return default(TGridObject);
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int row, column;
        GetRowAndColumn(worldPosition, out row, out column);
        return GetGridObject(row, column);
    }
}
