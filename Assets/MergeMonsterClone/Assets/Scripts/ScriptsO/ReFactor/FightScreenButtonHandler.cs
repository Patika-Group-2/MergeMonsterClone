using UnityEngine;

public class FightScreenButtonHandler : MonoBehaviour
{
    public void FightButton()
    {
        EntityManager.Instance.SetEntityList();
        GameManager.Instance.SetRunningTrue();
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetPlayerCountAtBegin();
    }
}
