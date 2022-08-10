using System.Collections.Generic;
using UnityEngine;

public class GenerationHandler : MonoBehaviour
{
    void Start()
    {
        Testing3D.TileList = GetTileList();
    }

    List<Tile> GetTileList()
    {
        List<Tile> tileList = new List<Tile>();

        int row = Testing3D.BoardGrid.GridTiles.GetLength(0);
        int column = Testing3D.BoardGrid.GridTiles.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                tileList.Add(Testing3D.BoardGrid.GridTiles[i, j]);
            }
        }

        return tileList;
    }

    public Vector3 GetTilePosition(Tile tile)
    {
        return tile.Get3DTilePosition();
    }

    public void GenerateCharacter()
    {
        foreach (Tile tile in Testing3D.TileList)
        {
            if (tile.IsAvailable)
            {
                ICharacterGenerator character = GetComponent<ICharacterGenerator>();
                GameObject characterGO = character.GenerateCharacter();
                character.PositionCharacter(characterGO, GetTilePosition(tile), character.CharacterPrefab.transform.rotation);
                tile.IsAvailable = false;
                return;
            }
        }

        Debug.Log("The board is full!");
    }
}