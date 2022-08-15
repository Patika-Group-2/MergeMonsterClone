using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UISettings : MonoBehaviour
{
    [SerializeField] Camera _shopCam, _fightCam, _mergeCam;
    [SerializeField] PostProcessVolume _volume;

    Camera _currentCam;
    AutoExposure _autoExposure;


    void Start()
    {
        _volume.profile.TryGetSettings(out _autoExposure);
    }

    public void GoToFightScene()
    {
        StartCoroutine(ShowCor(_fightCam));
    }

    public void GoToMergeScene()
    {
        StartCoroutine(ShowCor(_mergeCam));
    }

    public void GoToShopScene()
    {
        StartCoroutine(ShowCor(_shopCam));
    }

    IEnumerator ShowCor(Camera cam)
    {
        _autoExposure.keyValue.value = 0;
        yield return new WaitForSeconds(1.5f);
        Reset();
        cam.depth = 1;
        cam.tag = "MainCamera";
        _autoExposure.keyValue.value = 1;
    }

    void Reset()
    {
        _shopCam.tag = "Untagged";
        _fightCam.tag = "Untagged";
        _mergeCam.tag = "Untagged";

        _shopCam.depth = 0;
        _fightCam.depth = 0;
        _mergeCam.depth = 0;
    }
}
