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

        for (int i = 0; i < column / 2; i++)
        {
            for (int j = 0; j < row; j++)
            {
                tileList.Add(Testing3D.BoardGrid.GridTiles[j, i]);
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
<<<<<<< HEAD:MergeMonsterClone/Assets/Scripts/CharacterGeneration/GenerationHandler.cs
                characterGO.GetComponent<ICharacter>().PositionCharacter(GetTilePosition(tile), character.CharacterPrefab.transform.rotation);
=======
                characterGO.GetComponent<ICharacterGenerator>().PositionCharacter(GetTilePosition(tile), character.CharacterPrefab.transform.rotation);
>>>>>>> Arda:Assets/MergeMonsterClone/Assets/Scripts/CharacterGeneration/GenerationHandler.cs
                tile.IsAvailable = false;
                tile.TileObject = characterGO;
                return;
            }
        }

        Debug.Log("The board is full!");
    }
}