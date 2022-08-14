using UnityEngine;

public class RangedGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(GameObject character, Vector3 position, Quaternion rotation)
    {
        float Characterffset = CharacterPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;

        character.transform.position = position + Vector3.up * Characterffset;
        character.transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        //RangedGenerator ranged = go.AddComponent<RangedGenerator>();
        //ranged.CharacterPrefab = CharacterPrefab;
        return go;
    }
}
