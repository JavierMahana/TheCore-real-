using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Queue<HealthBar> unusedHealthBars = new Queue<HealthBar>(64);
    private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

    //para cambiarloa world space ahay que reparentar el prefaba a la health.
    // tanmbien hay que cambiar la healthbar a world space.

    private void Awake()
    {
        AllocateUnusedQueue();
        Health.RequestHealthbar += AddHealthBar;
        Health.RemoveHealthBar += RemoveHealthBar;
    }
    private void AllocateUnusedQueue()
    {
        HealthBar[] unusedBars = GetComponentsInChildren<HealthBar>(true);

        Debug.Assert(unusedBars.Length > 0);
        foreach (HealthBar bar in unusedBars)
        {
            unusedHealthBars.Enqueue(bar);
        }
    }
    private void AddHealthBar(Health health)
    {
        if (healthBars.ContainsKey(health) == false)
        {
            HealthBar healthBar = unusedHealthBars.Dequeue();
            healthBars.Add(health, healthBar);
            healthBar.gameObject.SetActive(true);
            healthBar.transform.localScale = Vector3.one; //habia un bug donde la escala se ponía 0

            healthBar.SetHealth(health);
            
        }
    }
    private void RemoveHealthBar(Health health)
    {
        if (healthBars.ContainsKey(health))
        {
            healthBars[health].gameObject.SetActive(false);
            unusedHealthBars.Enqueue(healthBars[health]);
            healthBars.Remove(health);
            
            
        }
    }
}
