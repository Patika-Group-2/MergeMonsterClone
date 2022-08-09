using UnityEngine;

public class Human : MonoBehaviour, ICharacter
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(GameObject character, Vector3 position, Quaternion rotation)
    {
        float characterOffset = _characterPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;

        character.transform.position = position + Vector3.up * characterOffset;
        character.transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        Human human = go.AddComponent<Human>();
        human.CharacterPrefab = _characterPrefab;
        return go;
    }
}
