using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _foregroundImage;
    [SerializeField] private float _updateSpeed;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    public IEnumerator ChangeHealthBar(float percentage)
    {
        float percentagePreChange = _foregroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < _updateSpeed)
        {
            elapsed += Time.deltaTime;
            _foregroundImage.fillAmount = Mathf.Lerp(
                percentagePreChange, percentage, elapsed);
            yield return null;
        }

        _foregroundImage.fillAmount = percentage;
    }
}
