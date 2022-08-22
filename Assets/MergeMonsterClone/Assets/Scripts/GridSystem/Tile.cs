using UnityEngine;

public class Tile
{
    //Tile is belong this grid
    BoardGrid3D<Tile> _grid3D;

    public int Row { get; set; }
    public int Column { get; set; }

    public bool IsAvailable { get; set; }
    public GameObject TileObject { get; set; }


    public Tile(BoardGrid3D<Tile> grid3D, int row, int column)
    {
        _grid3D = grid3D;
        Row = row;
        Column = column;
        IsAvailable = true;
    }
    //Get world position from specific tile
    public Vector3 Get3DTilePosition()
    {
        return _grid3D.GetWorldPosition(Row, Column);
    }
}
