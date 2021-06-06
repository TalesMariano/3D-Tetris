using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCenterBlock : MonoBehaviour, IRotatable
{
    public bool rotetable = true;

    Tetrimino tetrimino;
    GridArea gridArea;

    public void Rotate(Tetrimino _tetrimino, GridArea _gridArea)
    {
        if (!rotetable)
            return;

        tetrimino = _tetrimino;
        gridArea = _gridArea;

        // Rotate blocks
        tetrimino.blockPosRelative = TryRotate(tetrimino.blockPosRelative);

        // Upd position
        for (int i = 0; i < tetrimino.blockPosRelative.Length; i++)
        {
            tetrimino.blocks[i].UpdPos((Vector3Int)tetrimino.blockPosRelative[i]);
        }
    }


    Vector2Int[] TryRotate(Vector2Int[] positionsRelative)
    {
        //List<Vector2Int> invalidPositions = new List<Vector2Int>();
        Vector2Int[] invalidPositions;
        Vector2Int[] posRotatedRelative = new Vector2Int[positionsRelative.Length];
        Vector2Int[] posRotated = new Vector2Int[positionsRelative.Length];

        // rotate all positions
        for (int i = 0; i < posRotatedRelative.Length; i++)   // For each block
        {
            posRotatedRelative[i] = Vector2Int.RoundToInt(Quaternion.Euler(0, 0, -90) * (Vector2)positionsRelative[i]);
            posRotated[i] = posRotatedRelative[i] + tetrimino.pos;
        }

        //tempPos = tetrimino.BlockPos(tempPosRelative);

        invalidPositions = gridArea.GetInvalidBlocks(posRotated);

        // if there are invalid positions
        if (invalidPositions != null && invalidPositions.Length > 0)
        {
            //for now, dont rotate
            return positionsRelative;

            /*
            Vector2Int[] newPosAjusted = tempGridArea.GetNextValidPositionDirection(BlockPos(tempPosRelative), Vector2Int.right, 1);

            if (newPosAjusted == null)
                return tempPosRelative;
            else
                return BlockPosRelative(newPosAjusted);
                */

            //int numMovements = tempGridArea.GetNumMovementsValidPositionDirection(BlockPos(posRotatedRelative), Vector2Int.right);

            //if (numMovements == -1)
            //    return posRotatedRelative;
            //else
            //{
            //    for (int i = 0; i < posRotatedRelative.Length; i++)
            //    {
            //        posRotatedRelative[i] += Vector2Int.right * numMovements;
            //    }
            //    return posRotatedRelative;
            //}

            // Try ajust right
            //GetNextValidPositionDirection

        }

        return posRotatedRelative;
    }

    /*
    public void RotateOld()
    {
        for (int i = 0; i < blockPosRelative.Length; i++)
        {
            Vector2 temp = Quaternion.Euler(0, 0, -90) * (Vector2)blockPosRelative[i];

            blockPosRelative[i] = Vector2Int.RoundToInt(temp);
            blocks[i].transform.localPosition = temp;

        }
    }

    public void RotateNew() {

        //List<Vector2Int> invalidPositions = new List<Vector2Int>();
        Vector2Int[] invalidPositions;
        Vector2Int[] tempPosRelative = blockPosRelative;
        Vector2Int[] tempPos = BlockPos(blockPosRelative);

        // rotate all positions
        for (int i = 0; i < tempPos.Length; i++)   // For each block
        {
            tempPosRelative[i] = Vector2Int.RoundToInt( Quaternion.Euler(0, 0, -90) * (Vector2)blockPosRelative[i]);
        }

        tempPos = BlockPos(blockPosRelative);

        invalidPositions = tempGridArea.GetInvalidBlocks(tempPos);

        // if there are invalid positions
        if(invalidPositions != null && invalidPositions.Length > 0)
        {
            Debug.LogError("Invalid pos " + invalidPositions[0]);

            //for now, dont rotate
            return;
            // Later, it shoud ajust positions
        }

        // apply positions
        blockPosRelative = tempPosRelative;

        // upd block positions
        for (int i = 0; i < blockPosRelative.Length; i++)   
        {
            //blocks[i].UpdPos((Vector3Int)tempPos[i]);
            blocks[i].transform.localPosition = (Vector3Int)tempPosRelative[i];
        }

    }
    */
}
