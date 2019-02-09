using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private float updateTimeSeconds;

    [SerializeField]
    private float verticalOffSet;

    private Health health;
    private Camera relatedCamera;

    public void SetHealth(Health health)
    {
        this.health = health;
        health.OnHealthPercentChanged += HandleHealthChange;
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHealthPercentChanged -= HandleHealthChange;
        }
        health = null;
    }

    private void HandleHealthChange(float percent)
    {
        StartCoroutine(ChangeHealthSmoothly(percent));
    }

    private IEnumerator ChangeHealthSmoothly(float percent)
    {
        float preCahngePercent = foregroundImage.fillAmount;
        float elapsed = 0;
        
        while (elapsed <= updateTimeSeconds) 
        {
            foregroundImage.fillAmount = Mathf.Lerp(preCahngePercent, percent, elapsed / updateTimeSeconds);
            elapsed += Time.deltaTime;

            yield return null;
        }
        foregroundImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        if (health != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(health.transform.position + Vector3.up * verticalOffSet);
        }
        //transform.LookAt(relatedCamera.transform);
        //transform.Rotate(0, 180, 0);
    }
}
