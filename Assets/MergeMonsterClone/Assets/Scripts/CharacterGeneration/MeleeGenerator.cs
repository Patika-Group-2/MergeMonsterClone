using UnityEngine;

public class MeleeGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(Vector3 position, Quaternion rotation)
    {
        // None of human needs this line
        // float Characterffset = _characterPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;

        transform.position = position;
        transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        return go;
    }
}
