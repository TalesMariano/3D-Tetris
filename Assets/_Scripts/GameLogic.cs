using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("References")]
    public Spawner spawner;
    public GridArea gridArea;
    public TempInputMove tempInput;
    public Tetrimino mainTetrimino;

    [Header("Settings")]
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
        // test start
        SpawnTetrimino();
    }

    private void Update()
    {
        // Test Move Down
        if (blockMoving)
        {
            moveTime += Time.deltaTime;

            if (moveTime>timeMoveDown)
            {
                mainTetrimino?.MoveDown();
                moveTime = 0;
            }

        }
    }


    public void SpawnTetrimino()
    {
        mainTetrimino = spawner.SpawnTetrimino();
        tempInput.tetrimino = mainTetrimino;

        mainTetrimino.OnPlaced += SpawnTetrimino;

        // Test
        mainTetrimino.gridArea = gridArea;
    }

    [ContextMenu("test Pos")]
    void TestDownPos()
    {
        Vector2Int[] array = mainTetrimino.BlockPos();
        for (int i = 0; i < array.Length; i++)
        {
            Vector2Int blockPos = array[i];
            Vector2Int tPos = blockPos + Vector2Int.down;

            if(gridArea.blocks[tPos.x, tPos.y] == null)
                Debug.Log(gridArea.blocks[tPos.x, tPos.y]);
            else
                Debug.Log(gridArea.blocks[tPos.x, tPos.y], gridArea.blocks[tPos.x, tPos.y].gameObject);




            if (!gridArea.ValidPos(tPos))
            {
                

                /*
                //test
                if (blockPos.y - 1 > 0) // Inside map grid
                {
                    Debug.Log(blocks[blockPos.x, blockPos.y - 1], blocks[blockPos.x, blockPos.y - 1].gameObject);
                }*/

            }

        }
    }

}
