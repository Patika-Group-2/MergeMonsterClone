using UnityEngine;

public class FightScreenButtonHandler : MonoBehaviour
{
    public void FightButton()
    {
        EntityManagerO.Instance.SetEntityList();
        GameManagerO.Instance.SetRunningTrue();
    }
}
