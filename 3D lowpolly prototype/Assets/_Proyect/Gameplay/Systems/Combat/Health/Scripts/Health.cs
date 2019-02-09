using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public static Action<Health> RequestHealthbar = delegate { };
    public static Action<Health> RemoveHealthBar = delegate { };

    [SerializeField]
    private int maxHealth;
    public int CurrentHealth { get; private set; }
    private bool healthBarActive = false; 


    public Action<float> OnHealthPercentChanged = delegate { };
    public Action OnDeath = delegate { };

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        if (CurrentHealth <= 0)
        {
            OnDeath();
            gameObject.SetActive(false); //--------------------------- this matbe is beter to pospose it to later. or with a visual efect
        }


        CheckHealthForDisplayingHealthbar();

        float currentPrcHealth = (float)CurrentHealth / (float)maxHealth;
        OnHealthPercentChanged(currentPrcHealth);
    }



    private void OnEnable()
    {
        CurrentHealth = maxHealth;
        CheckHealthForDisplayingHealthbar();
    }
    private void OnDisable()
    {
        if (healthBarActive)
        {
            RemoveHealthBar(this);
        }
    }
    

    private void CheckHealthForDisplayingHealthbar()
    {
        if (CurrentHealth < maxHealth && healthBarActive == false)
        {
            RequestHealthbar(this);
            healthBarActive = true;
        }
        else if (CurrentHealth >= maxHealth && healthBarActive)
        {
            RemoveHealthBar(this);
            healthBarActive = false;
        }
    }

    private void OnApplicationQuit()
    {
        this.enabled = false;
    }

}
