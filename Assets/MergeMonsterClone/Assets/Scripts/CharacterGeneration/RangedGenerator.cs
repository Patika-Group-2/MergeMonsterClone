using UnityEngine;

public class RangedGenerator : MonoBehaviour, ICharacterGenerator
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(Vector3 position, Quaternion rotation)
    {
        //Green dragon not suitable for this method
        float characterOffset;
        // Only Green and orange dragon must have this line
        if (CharacterPrefab.name == "Orange_Dragon_3(Clone)" || CharacterPrefab.name == "Green_Dragon_4(Clone)")
            characterOffset = CharacterPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
        else
            characterOffset = 0;

        transform.position = position + Vector3.up * characterOffset;
        transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        return go;
    }
}
