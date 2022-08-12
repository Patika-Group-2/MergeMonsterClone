using UnityEngine;

public class FightScreenButtonHandler : MonoBehaviour
{
    public void FightButton()
    {
        EntityManager.Instance.SetEntityList();
        GameManager.Instance.SetRunningTrue();
    }

    //this method will be deleted later. Just for test
    public void SetCurrentLevel()
    {
        LevelCreator.Instance.SetLevel();
    }
}
