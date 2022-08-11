using UnityEngine;

public class Dragon : MonoBehaviour, ICharacter
{
    [SerializeField] GameObject _characterPrefab;

    public GameObject CharacterPrefab { get => _characterPrefab; set => _characterPrefab = value; }


    public void PositionCharacter(Vector3 position, Quaternion rotation)
    {
        float characterOffset = CharacterPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;

        transform.position = position + Vector3.up * characterOffset;
        transform.rotation = rotation;
    }

    public GameObject GenerateCharacter()
    {
        GameObject go = Instantiate(CharacterPrefab);
        Dragon dragon = go.AddComponent<Dragon>();
        dragon.CharacterPrefab = CharacterPrefab;
        return go;
    }
}
