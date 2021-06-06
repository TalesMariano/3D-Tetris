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
        }

        return posRotatedRelative;
    }

}
