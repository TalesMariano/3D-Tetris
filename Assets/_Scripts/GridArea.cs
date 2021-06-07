using System;
using System.Collections.Generic;
using UnityEngine;

public class GridArea : MonoBehaviour
{
    public int heith = 20;
    public int width = 10;

    Block[,] blocks;

    List<int> linesToClear = new List<int>();

    public Action<bool> OnBlockPlaced;
    public Action<int> OnLineComplete;
    public Action OnBlockOverLimit;

    private void Start()
    {
        //Ajust height, so new blocks can be spawned outside the play area
        heith += 3;

        blocks = new Block[width, heith];
    }

    /// <summary>
    /// Check if block positions are over defaut play area
    /// Used to check game over 
    /// </summary>
    /// <param name="positions">Block Positions</param>
    bool CheckBlockPosOverGridArea(Vector2Int[] positions)
    {
        int heightLimit = 20;

        foreach (var pos in positions)
        {
            if (pos.y > heightLimit)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Place Tetrinimo block on Grid
    /// </summary>
    /// <param name="tetrimino">Tetrimino block to place</param>
    public void PlaceBlock(Tetrimino tetrimino)
    {
        Vector2Int[] blockPositions = tetrimino.BlockPos();

        // Check Game Over
        if (CheckBlockPosOverGridArea(blockPositions))
        {
            OnBlockOverLimit?.Invoke();
            return;
        }

        for (int i = 0; i < blockPositions.Length; i++)
        {
            blocks[blockPositions[i].x, blockPositions[i].y] = tetrimino.blocks[i];
            tetrimino.blocks[i].transform.parent = transform;
        }

        bool lineCompleted = CheckEachLineFull();

        OnBlockPlaced?.Invoke(lineCompleted);

        // Clear lines, organize and call event
        if (lineCompleted)
        {
            OnLineComplete?.Invoke(linesToClear.Count);

            ClearLines();
        }
    }

    /// <summary>
    /// Return if all blocks in Tetrimino are in a valid position
    /// </summary>
    public bool ValidPos(Tetrimino tetrimino)
    {
        foreach (var blockPos in tetrimino.BlockPos())
        {
            if (!ValidPos(blockPos + tetrimino.nextPosRelative))
                return false;
        }
        return true;
    }


    /// <summary>
    /// Return if all positions given are valid in grid
    /// </summary>
    public bool ArePositionsValid(Vector2Int[] positions)
    {
        foreach (var blockPos in positions)
        {
            if (!ValidPos(blockPos))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Check if tetrimino block have arrived at bottom, or on top of another block
    /// </summary>
    /// <param name="tetrimino"></param>
    /// <returns></returns>
    public bool CheckAriveBottom(Tetrimino tetrimino)
    {
        foreach (var blockPos in tetrimino.BlockPos())
        {
            if (!ValidPos(blockPos + Vector2Int.down))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Check all rows for full lines
    /// </summary>
    /// <returns>Return true if any </returns>
    bool CheckEachLineFull()
    {
        for (int y = 0; y < blocks.GetLength(1); y++)
        {
            // Check if all positions are full
            bool allFull = true;
            for (int x = 0; x < blocks.GetLength(0); x++)
            {
                allFull = allFull && blocks[x, y] != null;
            }


            if (allFull)
                linesToClear.Add(y);//ClearLine(y);

        }

        return linesToClear.Count > 0;
    }

    void ClearLines()
    {
        foreach (var line in linesToClear)
        {
            ClearLine(line);
        }

        MoveAllGridDown();

        linesToClear.Clear();
    }

    /// <summary>
    /// Remove all blocks of "numLine" row
    /// </summary>
    /// <param name="numLine"></param>
    void ClearLine(int numLine)
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            Destroy(blocks[i, numLine].gameObject); // destroy block Object

            blocks[i, numLine] = null;
        }
    }

    void MoveAllGridDown()
    {
        for (int i = linesToClear.Count-1; i >= 0; i--)
        {
            MoveGridDown(linesToClear[i]);
        }
    }

    /// <summary>
    /// Move all rows above numRow one line down
    /// Used to clear completed line
    /// </summary>
    /// <param name="numRow"></param>
    void MoveGridDown(int numRow)
    {
        // Move Lines Down
        for (int y = numRow; y < blocks.GetLength(1) - 1; y++)  // from line number up
        {
            for (int x = 0; x < blocks.GetLength(0); x++)
            {
                blocks[x, y] = blocks[x, y + 1];

                blocks[x, y]?.UpdPos(new Vector3(x, y));   // Update cube pos
            }
        }
    }

    /// <summary>
    /// Return if position is valid in grid
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool ValidPos(Vector2Int pos) // Temp public
    {
        if (pos.x>=0 && pos.x < width && pos.y>=0 && pos.y < heith) // Inside map grid
        {
            if (blocks[pos.x, pos.y] == null)   // is position empth
                return true;
        }
            return false;
    }


    /// <summary>
    /// Check which positions in array are invalid, and return a new array with only invalid position
    /// </summary>
    /// <param name="positions">Position array</param>
    /// <returns>New array with only the invalid blocks</returns>
    public Vector2Int[] GetInvalidBlocks(Vector2Int[] positions)
    {
        List<Vector2Int> invalidPositions = new List<Vector2Int>();

        foreach (var pos in positions)
        {
            if (!ValidPos(pos))
                invalidPositions.Add(pos);
        }

        return invalidPositions.ToArray();
    }


    /// <summary>
    /// Draw a block for eacho tetrimino single piece placed on the grid
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (blocks == null)
            return;

        for (int x = 0; x < blocks.GetLength(0); x++)
        {
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                if(blocks[x,y] != null)
                    Gizmos.DrawWireCube(new Vector3(x,y), Vector3.one * 0.5f);
            }
        }
    }
}
