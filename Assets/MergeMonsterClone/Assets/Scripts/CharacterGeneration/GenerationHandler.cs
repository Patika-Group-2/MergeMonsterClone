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

    // Get world position from specific tile 
    public Vector3 GetTilePosition(Tile tile)
    {
        return tile.Get3DTilePosition();
    }

    //Attached to shop buy buttons
    public void GenerateCharacter()
    {
        foreach (Tile tile in Testing3D.BoardGrid.PlayerTiles)
        {
            if (tile.IsAvailable)
            {
                ICharacterGenerator character = GetComponent<ICharacterGenerator>();
                //Instantiate character
                GameObject characterGO = character.GenerateCharacter();
                // Set character position
                characterGO.GetComponent<ICharacterGenerator>().PositionCharacter(GetTilePosition(tile), character.CharacterPrefab.transform.rotation);

                Character ch = characterGO.GetComponent<Character>();
                // Set character's tile id's and add it to the dynamic player list
                LevelCreator.Instance.SetCharacterTileID(ch, tile.Row, tile.Column);
                LevelCreator.Instance.Players.Add(ch);
                //make tile disable
                tile.IsAvailable = false;
                tile.TileObject = characterGO;
                return;
            }
        }

        Debug.Log("The board is full!");
    }
}