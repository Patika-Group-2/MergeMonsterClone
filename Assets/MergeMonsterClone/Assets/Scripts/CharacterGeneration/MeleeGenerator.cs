using UnityEngine;

public class MeleeGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(Vector3 position, Quaternion rotation)
    {
        float Characterffset = _characterPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;

        transform.position = position + Vector3.up * Characterffset;
        transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        return go;
    }
}
