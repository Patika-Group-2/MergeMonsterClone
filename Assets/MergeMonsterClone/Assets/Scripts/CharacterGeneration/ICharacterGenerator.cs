using UnityEngine;

public interface ICharacterGenerator
{
    public GameObject CharacterPrefab { get; set; }

    public void PositionCharacter(GameObject character, Vector3 position, Quaternion rotation);
    
    public GameObject GenerateCharacter();
}