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

        for (int i = 0; i < row / 2; i++)
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
        foreach (Tile tile in Testing3D.BoardGrid.PlayerTiles)
        {
            if (tile.IsAvailable)
            {
                ICharacterGenerator character = GetComponent<ICharacterGenerator>();
                GameObject characterGO = character.GenerateCharacter();
<<<<<<< HEAD

                character.PositionCharacter(characterGO, GetTilePosition(tile), character.CharacterPrefab.transform.rotation);
                Character ch = characterGO.GetComponent<Character>();

                LevelCreator.Instance.SetCharacterTileID(ch, tile.Row, tile.Column);
                LevelCreator.Instance.Players.Add(ch);

=======
                characterGO.GetComponent<ICharacterGenerator>().PositionCharacter(GetTilePosition(tile), character.CharacterPrefab.transform.rotation);
>>>>>>> 9aac39cc77041f85a515704b8e844507831aa8a9
                tile.IsAvailable = false;
                tile.TileObject = characterGO;
                return;
            }
        }

        Debug.Log("The board is full!");
    }
}