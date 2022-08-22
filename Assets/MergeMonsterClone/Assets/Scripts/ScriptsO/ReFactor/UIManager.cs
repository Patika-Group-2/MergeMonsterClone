using System.Collections;
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

    AutoExposure _autoExposure;

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
        //Subscribe to events
        GameManager.Instance.OnWin += LoadWinScreen;
        GameManager.Instance.OnLose += LoadLoseScreen;
    }

    public void LoadWinScreen()
    {
        //Add level prize to the total gold
        BankManager.Instance.WinAddMoney.AddMoney();

        //Instantiate prefab and get a reference to WinScreenHandler
        GameObject _gameObject = Instantiate(_winScreenPrefab);
        WinScreenHandler wSH = _gameObject.GetComponent<WinScreenHandler>();

        //Set WinScreen texts
        wSH.SetTexts(GameManager.Instance.CurrentLevel, 
            BankManager.Instance.WinAddMoney._winPrize, 
            BankManager.Instance.currentBalance);
    }

    public void LoadLoseScreen()
    {
        //Calculate how much money drops from enemies
        BankManager.Instance.CoinDrop.AddTotalCoin();

        //Instantiate prefab and get a reference to LoseScreenHandler
        GameObject _gameObject = Instantiate(_loseScreenPrefab);
        LoseScreenHandler lSH = _gameObject.GetComponent<LoseScreenHandler>();

        //Set LoseScreen texts
        lSH.SetTexts(GameManager.Instance.CurrentLevel,
                      BankManager.Instance.CoinDrop.MoneyHolder,
                      BankManager.Instance.currentBalance);

        //Reset total droped money
        BankManager.Instance.CoinDrop.ResetMoney();
    }

    public void GoToFightScreen()
    {
        LevelCreator.Instance.LoadPlayerSO();
        StartCoroutine(ShowCor(_fightCam, _fightCanvas));
    }
    // Go to fight from lose-win screen 
    public void GoToFightDirecetly()
    {
        StartCoroutine(ShowCor(_fightCam, _fightCanvas));
    }
    // Go to shop from lose-win screen 
    public void GoToShopDirecetly()
    {
        StartCoroutine(ShowCor(_shopCam, _shopCanvas));
    }

    public void GoToMergeScreen()
    {
        LevelCreator.Instance.LoadPlayerSO();
        StartCoroutine(ShowCor(_mergeCam, _mergeCanvas));
    }

    public void GoToShopScreen()
    {
        LevelCreator.Instance.LoadPlayerSO();
        StartCoroutine(ShowCor(_shopCam, _shopCanvas));
    }

    //make transition effect and change camera
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
        LevelCreator.Instance.SetLevel();
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
