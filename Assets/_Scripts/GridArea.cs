using System;
using System.Collections.Generic;
using UnityEngine;

public class GridArea : MonoBehaviour
{
    public int heith = 20;
    public int width = 10;

    public Block[,] blocks;    // temp public

    List<int> linesToClear = new List<int>();


    public Action<bool> OnBlockPlaced;
    public Action<int> OnLineComplete;
    public Action OnBlockOverLimit;



    private void Start()
    {
        //Ajust height
        heith += 3;

        blocks = new Block[width, heith];

    }


    bool CheckGameOver(Vector2Int[] positions)
    {
        int heightLimit = 20;

        foreach (var pos in positions)
        {
            if (pos.y > heightLimit)
                return true;
        }

        return false;
    }


    public void PlaceBlock(Tetrimino tetrimino)
    {
        /*
        //Old
        for (int i = 0; i < tetrimino.blocks.Length; i++)
        {
            tetrimino.blocks[i].transform.parent = transform;

            Vector2Int tempPos = new Vector2Int((int)tetrimino.blocks[i].transform.position.x, (int)tetrimino.blocks[i].transform.position.y);

            blocks[tempPos.x, tempPos.y] = tetrimino.blocks[i];
        }
        */

        Vector2Int[] blockPositions = tetrimino.BlockPos();


        // Check Game Over
        if (CheckGameOver(blockPositions))
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

    public bool ValidPos(Tetrimino tetrimino)
    {
        foreach (var blockPos in tetrimino.BlockPos())
        {
            if (!ValidPos(blockPos + tetrimino.nextPosRelative))
                return false;
        }


        return true;
    }



    public bool IsPositionsValid(Vector2Int[] positions)
    {
        foreach (var blockPos in positions)
        {
            if (!ValidPos(blockPos))
                return false;
        }

        return true;
    }


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

    public Vector2Int AjustPos(Vector2Int[] blockPositions)
    {
        Vector2Int ajustDirection = Vector2Int.zero;


        for (int i = 0; i < blockPositions.Length; i++)
        {
            //Check Left
            



        }


        //Check Left
        do
        {





        } while (true);
    }

    public Vector2Int AjustPosSingle(Vector2Int pos)
    {
        Vector2Int ajustDirection = Vector2Int.zero;

        // move left
        for (int i = 0; i < 2; i++)
        {
            
        }




        return Vector2Int.zero;
    }

    public void AjustLeft( Tetrimino tetrimino)
    {
        Vector2Int[] blockPositions = tetrimino.BlockPos();

        int numMove = 0;

        // Calculate number of moves ajusts
        for (int i = 0; i < blockPositions.Length; i++)
        {
            if (blockPositions[i].x < 0)
                numMove++;

        }

        Debug.Log("Num Moves " + numMove);

        // Do Moves
        for (int i = 0; i < numMove; i++)
        {
            tetrimino.MoveRight();
        }
    }

    public void AjustRight(Tetrimino tetrimino)
    {
        Vector2Int[] blockPositions = tetrimino.BlockPos();

        int numMove = 0;

        // Calculate number of moves ajusts
        for (int i = 0; i < blockPositions.Length; i++)
        {
            if (blockPositions[i].x >= width)
                numMove++;
        }

        Debug.Log("Num Moves " + numMove);

        // Do Moves
        for (int i = 0; i < numMove; i++)
        {
            tetrimino.MoveLeft();
        }
    }


    Vector2Int AjustRotation(Tetrimino tetrimino)
    {
        // base block position
        Vector2Int[] blockPositions = tetrimino.BlockPos();

        int horizontalMove = 0;

        // Calculate ajust from left
        for (int i = 0; i < blockPositions.Length; i++)
        {
            if (blockPositions[i].x < 0)
                horizontalMove++;
        }

        // Calculate ajust from left
        for (int i = 0; i < blockPositions.Length; i++)
        {
            if (blockPositions[i].x >= width)
            {
                //if need to move both right and left, cannot rotate
                if (horizontalMove > 0)
                {
                    // return -1, meaning rotation is invalid
                    return -Vector2Int.one;
                }


                horizontalMove--;
            }   
        }




        return Vector2Int.right * horizontalMove;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="positions"></param>
    /// <param name="direction"></param>
    /// <param name="numTries">num of movements it will try before give up</param>
    /// <returns>The new position if valid, null if there is no valid position</returns>
    public Vector2Int[] GetNextValidPositionDirection(Vector2Int[] positions, Vector2Int direction, int numTries = 2)
    {
        for (int i = 0; i < numTries; i++)
        {
            bool valid = true;

            foreach (var pos in positions)
            {
                valid = valid && ValidPos(pos + direction * i);
            }

            if (valid)  // return new position if valid
            {
                for (int j = 0; j < positions.Length; j++)
                {
                    positions[j] += direction * j;
                }
                return positions;
            }
                
        }

        return null;
    }


    public int GetNumMovementsValidPositionDirection(Vector2Int[] positions, Vector2Int direction, int numTries = 2)
    {
        for (int i = 0; i < numTries; i++)
        {
            bool valid = true;

            foreach (var pos in positions)
            {
                valid = valid && ValidPos(pos + direction * i);
            }

            if (valid)  // return new position if valid
            {
                for (int j = 0; j < positions.Length; j++)
                {
                    positions[j] += direction * j;
                }
                return numTries;
            }

        }

        return -1;
    }

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
