using UnityEngine;

//Fight button manager
public class FightScreenButtonHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _ambientSound;

    public void FightButton()
    {
        //EntityManager.Instance.SetEntityList();
        //start the fight
        GameManager.Instance.SetRunningTrue();
        //Store all active player entities into the SO
        LevelCreator.Instance.LoadPlayerSO();
        //Set player count at the beginnig to calculate star count
        LevelCreator.Instance.SetPlayerCountAtBegin();
        //Play ambient sound
        SoundManager.Instance.PlaySound(_ambientSound);
        UIManager.Instance.ResetCanvas();
    }
}
