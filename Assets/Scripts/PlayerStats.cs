using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;

    public static int money;
    public int startMoney = 300;

    public static int health;
    public int startHealth = 100;

    private void Start()
    {
        money = startMoney;
        health = startHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned!");
        }
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned!");
        }

        if (health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
