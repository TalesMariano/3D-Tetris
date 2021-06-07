using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("References")]
    public Spawner spawner;
    public GridArea gridArea;
    public Tetrimino mainTetrimino;

    [Header("Settings")]
    public GameState gameState = GameState.Playing;
    public bool blockMoving = true;
    public float timeMoveDown = 1;
    float moveTime = 0;

    #region Events
    public Action<int> OnLineComplete;  // int - Number Lines
    public Action<bool> OnBlockPlaced; // bool - line completed
    public Action OnSpawnBlock;
    public Action OnGameOver;

    private void OnEnable()
    {
        gridArea.OnLineComplete += LineComplete;
        gridArea.OnBlockPlaced += BlockPlaced;
        gridArea.OnBlockOverLimit += GameOver;
    }

    private void OnDisable()
    {
        gridArea.OnLineComplete -= LineComplete;
        gridArea.OnBlockPlaced -= BlockPlaced;
        gridArea.OnBlockOverLimit -= GameOver;
    }

    void LineComplete(int i)
    {
        OnLineComplete?.Invoke(i);
    }

    void BlockPlaced(bool b)
    {
        OnBlockPlaced?.Invoke(b);
    }

    void GameOver()
    {
        OnGameOver?.Invoke();
    }

    #endregion

    private void Start()
    {
        StarGame();
    }

    private void Update()
    {
        if (gameState == GameState.Playing && blockMoving)  // If game is playing and block moving is enabled
        {
            // Move current controlled block down after "timeMoveDown" seconds
            // Moving down tetrimino with controller resets movedown timer
            moveTime += Time.deltaTime;

            if (moveTime>timeMoveDown)
            {
                mainTetrimino?.MoveDown();
                moveTime = 0;
            }

        }
    }

    /// <summary>
    /// Start game, setting up base vars and spawning Block
    /// </summary>
    public void StarGame()
    {
        // Start Game
        SpawnTetrimino();
        gameState = GameState.Playing;
        blockMoving = true;
    }

    #region Movement

    public void MovePieceLeft()
    {
        mainTetrimino.MoveLeft();
    }

    public void MovePieceRight()
    {
        mainTetrimino.MoveRight();
    }

    public void MovePieceDown()
    {
        mainTetrimino.MoveDown();
        moveTime = 0;
    }

    public void RotatePiece()
    {
        mainTetrimino.Rotate();
    }

    #endregion


    /// <summary>
    /// If gameState is palying, Spawn a tetrimino
    /// Apply events and references to tetrimino
    /// </summary>
    public void SpawnTetrimino()
    {
        if (gameState != GameState.Playing)
            return;

        mainTetrimino = spawner.SpawnTetrimino();

        mainTetrimino.OnPlaced += SpawnTetrimino;

        mainTetrimino.gridArea = gridArea;
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }
}
