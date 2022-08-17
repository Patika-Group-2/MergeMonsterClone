using UnityEngine;

public interface ICharacter
{
    public GameObject CharacterPrefab { get; set; }

    public void PositionCharacter(Vector3 position, Quaternion rotation);
    public GameObject GenerateCharacter();
}
