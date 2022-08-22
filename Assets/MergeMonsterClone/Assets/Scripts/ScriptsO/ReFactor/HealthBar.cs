using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _foregroundImage;
    //to prevent a sudden decrease in life
    [SerializeField] private float _updateSpeed;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    //make slowly decreasing health bar effect
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
