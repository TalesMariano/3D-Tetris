using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    public Tetrimino tetrimino;

    private void Awake()
    {
        tetrimino = GetComponent<Tetrimino>();
    }


    [ContextMenu("Test Place")]
    void TestPlace()
    {
        tetrimino.SetBlock();
    }



    [ContextMenu("TestRotate1")]
    void TestRotate1()
    {
        for (int i = 0; i < tetrimino.blockPosRelative.Length; i++)
        {
            // tetrimino.blockPosRelative[i] = new Vector2Int(-tetrimino.blockPosRelative[i].y, -tetrimino.blockPosRelative[i].x);

            // = (Vector2)tetrimino.blockPosRelative[i];

            Vector2 temp = Quaternion.Euler(0, 0, -90) * (Vector2)tetrimino.blockPosRelative[i];

            tetrimino.blockPosRelative[i] = Vector2Int.RoundToInt( temp);
            tetrimino.blocks[i].transform.localPosition = temp;
        }
    }


    void AjustPos()
    {
        // if too left, move right
            // if too tight, dont rotate

        // if too right, move left

        // if too down, move up
            // do it check height 
    }

    [ContextMenu("Test Move Right")]
    void TestMoveRight()
    {
        tetrimino.gridArea.AjustLeft(tetrimino);
    }

    [ContextMenu("Test Move Left")]
    void TestMoveLEft()
    {
        tetrimino.gridArea.AjustRight(tetrimino);
    }





}
