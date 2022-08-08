using UnityEngine;

public class Tile
{
    BoardGrid2D<Tile> _grid2D;
    BoardGrid3D<Tile> _grid3D;

    public int Row { get; set; }
    public int Column { get; set; }

    public bool IsAvailable { get; set; }


    public Tile(BoardGrid2D<Tile> grid2D, int row, int column)
    {
        _grid2D = grid2D;
        Row = row;
        Column = column;
        IsAvailable = true;
    }

    public Tile(BoardGrid3D<Tile> grid3D, int row, int column)
    {
        _grid3D = grid3D;
        Row = row;
        Column = column;
        IsAvailable = true;
    }

    public Vector3 Get3DTilePosition()
    {
        return _grid3D.GetWorldPosition(Row, Column);
    }

    public Vector2 Get2DTilePosition()
    {
        return _grid2D.GetWorldPosition(Row, Column);
    }
}
