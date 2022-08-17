using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _winScreenPrefab;
    [SerializeField] private GameObject _loseScreenPrefab;

    [SerializeField] Camera _shopCam, _fightCam, _mergeCam;
    [SerializeField] Canvas _shopCanvas, _fightCanvas, _mergeCanvas;
    [SerializeField] PostProcessVolume _volume;
    [SerializeField] BankManager _bankManager;

    AutoExposure _autoExposure;
    [SerializeField] WinLoseAddMoney _wLAM;

    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _volume.profile.TryGetSettings(out _autoExposure);
        

        GameManager.Instance.OnWin += LoadWinScreen;
        GameManager.Instance.OnLose += LoadLoseScreen;

    }

    public void LoadWinScreen()
    {
        GameObject _gameObject = Instantiate(_winScreenPrefab);
        WinScreenHandler _wSH = _gameObject.GetComponent<WinScreenHandler>();
        _wSH.SetTexts(GameManager.Instance.CurrentLevel,_wLAM._winWin, _bankManager.currentBalance);
        //Send parameters for text
    }

    public void LoadLoseScreen()
    {
        Instantiate(_loseScreenPrefab);
        //Send parameters for text and images
    }

    public void GoToFightScreen()
    {
        
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetLevel();
        StartCoroutine(ShowCor(_fightCam, _fightCanvas));
    }

    public void GoToFightDirecetly()
    {
        StartCoroutine(ShowCor(_fightCam, _fightCanvas));
        LevelCreator.Instance.SetLevel();
    }

    public void GoToShopDirecetly()
    {
        LevelCreator.Instance.SetLevel();
        StartCoroutine(ShowCor(_shopCam, _shopCanvas));
    }

    public void GoToMergeScreen()
    {
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetLevel();
        StartCoroutine(ShowCor(_mergeCam, _mergeCanvas));
    }

    public void GoToShopScreen()
    {
        LevelCreator.Instance.LoadPlayerSO();
        LevelCreator.Instance.SetLevel();
        StartCoroutine(ShowCor(_shopCam, _shopCanvas));
    }

    IEnumerator ShowCor(Camera cam, Canvas canvas)
    {
        _autoExposure.keyValue.value = 0;
        yield return new WaitForSeconds(1f);
        ResetCam();
        ResetCanvas();
        cam.depth = 1;
        cam.tag = "MainCamera";
        _autoExposure.keyValue.value = 1;
        canvas.enabled = true;
    }

    void ResetCam()
    {
        _shopCam.tag = "Untagged";
        _fightCam.tag = "Untagged";
        _mergeCam.tag = "Untagged";

        _shopCam.depth = 0;
        _fightCam.depth = 0;
        _mergeCam.depth = 0;
    }

    public void ResetCanvas()
    {
        _shopCanvas.enabled = false;
        _fightCanvas.enabled = false;
        _mergeCanvas.enabled = false;
    }

}
