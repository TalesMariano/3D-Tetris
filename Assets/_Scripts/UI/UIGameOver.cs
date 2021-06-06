using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [Header("References")]
    public GameLogic gameLogic;
    public GameObject gameplayUI;
    public GameObject gameoverScreen;

    #region Events
    private void OnEnable()
    {
        gameLogic.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        gameLogic.OnGameOver -= GameOver;
    }

    #endregion


    /// <summary>
    /// When game over happen, show gameover screen and hide normal gameplay UI
    /// </summary>
    void GameOver()
    {
        gameplayUI.SetActive(false);
        gameoverScreen.SetActive(true);
    }
}
