using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;

    public GameObject gameOverUI;

    private void Start()
    {
        GameIsOver = false;
    }

    private void Update()
    {
        if (GameIsOver)
            return;


        if (PlayerStats.health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
