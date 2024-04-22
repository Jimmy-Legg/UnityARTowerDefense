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
        healthBar.SetMaxHealth(health);
    }
}
