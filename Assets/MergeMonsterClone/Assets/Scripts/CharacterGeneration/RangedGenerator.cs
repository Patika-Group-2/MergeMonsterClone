using UnityEngine;

public class RangedGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(Vector3 position, Quaternion rotation)
    {
        float Characterffset = CharacterPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;

        transform.position = position + Vector3.up * Characterffset;
        transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
<<<<<<< HEAD
        //RangedGenerator ranged = go.AddComponent<RangedGenerator>();
        //ranged.CharacterPrefab = CharacterPrefab;
=======
>>>>>>> 9aac39cc77041f85a515704b8e844507831aa8a9
        return go;
    }
}
