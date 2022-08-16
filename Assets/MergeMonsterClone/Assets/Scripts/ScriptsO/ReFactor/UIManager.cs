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

        GameManager.Instance.OnWin += LoadWinScreen;
        GameManager.Instance.OnLose += LoadLoseScreen;

    }

    public void LoadWinScreen()
    {
        Instantiate(_winScreenPrefab);
        //Send parameters for text
    }

    public void LoadLoseScreen()
    {
        Instantiate(_loseScreenPrefab);
        //Send parameters for text and images
    }

    public void GoToFightScreen()
    {
        EntityManager.Instance.SetEntityList();
        LevelCreator.Instance.LoadPlayerSO();
        StartCoroutine(ShowCor(_fightCam, _fightCanvas));
    }

    public void GoToMergeScreen()
    {
        EntityManager.Instance.SetEntityList();
        LevelCreator.Instance.LoadPlayerSO();
        StartCoroutine(ShowCor(_mergeCam, _mergeCanvas));
    }

    public void GoToShopScreen()
    {
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
