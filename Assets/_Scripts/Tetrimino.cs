using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    public Vector2Int pos;
    public Vector2Int[] blockPosRelative;    // Tetrimino blocks relative to 

    public GridArea gridArea;

    IRotatable rotatable;


    private void Awake()
    {
        rotatable = GetComponent<IRotatable>();
    }

    public void Rotate()
    {
        rotatable.Rotate(this, gridArea);
    }


    public Vector2Int[] BlockPos()
    {
        Vector2Int[] result = new Vector2Int[blockPosRelative.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = pos + blockPosRelative[i];
        }

        return result;
    }

    Vector2Int[] BlockPos(Vector2Int[] blocks)
    {
        Vector2Int[] result = new Vector2Int[blocks.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = pos + blocks[i];
        }

        return result;
    }

    Vector2Int[] BlockPosRelative(Vector2Int[] blocks)
    {
        Vector2Int[] result = new Vector2Int[blocks.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] =  blocks[i] - pos;
        }

        return result;
    }

    [SerializeField] public Block[] blocks;

    // Events

    public Action OnPlaced;
    public Action OnMoveDown;

    [Header("Temp")]
    
    public Vector2Int nextPosRelative;

    public void MoveDown()
    {
        // test try set
        if (gridArea.CheckAriveBottom(this))
        {
            SetBlock();
            return;
        }



        nextPosRelative = Vector2Int.down;

        if (!gridArea.ValidPos(this))
            return;

        transform.localPosition += Vector3.down;
        pos += Vector2Int.down;
    }

    public void MoveLeft()
    {
        // If tetrimino position to the left is invalid, end method
        if(!gridArea.ArePositionsValid(MovePosDir(BlockPos(), Vector2Int.left)))
            return;

        transform.localPosition += Vector3.left;
        pos += Vector2Int.left;
    }

    public void MoveRight()
    {
        // If tetrimino position to the left is invalid, end method
        if (!gridArea.ArePositionsValid(MovePosDir(BlockPos(), Vector2Int.right)))
            return;

        transform.localPosition += Vector3.right;
        pos += Vector2Int.right;
    }



    //public void Rotate()
    //{
    //    // Rotate blocks
    //    blockPosRelative = TryRotate(blockPosRelative);

    //    // Upd position
    //    for (int i = 0; i < blockPosRelative.Length; i++)
    //    {
    //        blocks[i].UpdPos((Vector3Int)blockPosRelative[i]);
    //    }
    //}


    //Vector2Int[] TryRotate(Vector2Int[] positionsRelative)
    //{
    //    //List<Vector2Int> invalidPositions = new List<Vector2Int>();
    //    Vector2Int[] invalidPositions;
    //    Vector2Int[] tempPosRelative = new Vector2Int[positionsRelative.Length];


    //    Vector2Int[] tempPos;

    //    // rotate all positions
    //    for (int i = 0; i < tempPosRelative.Length; i++)   // For each block
    //    {
    //        tempPosRelative[i] = Vector2Int.RoundToInt(Quaternion.Euler(0, 0, -90) * (Vector2)positionsRelative[i]);
    //    }

    //    tempPos = BlockPos(tempPosRelative);

    //    invalidPositions = gridArea.GetInvalidBlocks(tempPos);

    //    // if there are invalid positions
    //    if (invalidPositions != null && invalidPositions.Length > 0)
    //    {
    //        //for now, dont rotate
    //        //return positionsRelative;

    //        /*
    //        Vector2Int[] newPosAjusted = tempGridArea.GetNextValidPositionDirection(BlockPos(tempPosRelative), Vector2Int.right, 1);

    //        if (newPosAjusted == null)
    //            return tempPosRelative;
    //        else
    //            return BlockPosRelative(newPosAjusted);
    //            */

    //        int numMovements = gridArea.GetNumMovementsValidPositionDirection(BlockPos(tempPosRelative), Vector2Int.right);

    //        if (numMovements == -1)
    //            return tempPosRelative;
    //        else
    //        {
    //            for (int i = 0; i < tempPosRelative.Length; i++)
    //            {
    //                tempPosRelative[i] += Vector2Int.right * numMovements;
    //            }
    //            return tempPosRelative;
    //        }

    //        // Try ajust right
    //        //GetNextValidPositionDirection


    //    }

    //    return tempPosRelative;
    //}



    public void SetBlock()
    {
        Debug.Log("Block Placed");

        // test Set Block
        gridArea.PlaceBlock(this);
        OnPlaced.Invoke();


        Destroy(this.gameObject);
    }

    [ContextMenu("TestPlace")]
    void TestPlace()
    {
        gridArea.PlaceBlock(this);
    }



    Vector2Int[] MovePosDir(Vector2Int[] blockPositions, Vector2Int direction)
    {
        for (int i = 0; i < blockPositions.Length; i++)
        {
            blockPositions[i] += direction;
        }

        return blockPositions;
    }

    /// <summary>
    /// Draw Center
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + Vector3.back*0.5f, 0.2f);
    }
}
