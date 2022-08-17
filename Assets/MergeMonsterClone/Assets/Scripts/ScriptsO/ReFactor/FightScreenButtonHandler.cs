using UnityEngine;
using UnityEngine.UI;


public class FightScreenButtonHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _ambientSound;

    public void FightButton()
    {
        EntityManager.Instance.SetEntityList();
        GameManager.Instance.SetRunningTrue();
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetPlayerCountAtBegin();
        SoundManager.Instance.PlaySound(_ambientSound);
        UIManager.Instance.ResetCanvas();
    }
}
