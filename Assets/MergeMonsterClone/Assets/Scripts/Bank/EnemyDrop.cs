using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int _dropAmount;

    private void OnDestroy()
    {
        CoinDropManager.Instance.AddCoin(_dropAmount);
    }
}

