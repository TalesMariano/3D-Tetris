using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public GameLogic gameLogic;

    bool horisontalStickDownLast;
    bool verticalStickDownLast;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!horisontalStickDownLast)
                gameLogic.MovePieceRight();

            horisontalStickDownLast = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (!horisontalStickDownLast)
                gameLogic.MovePieceLeft();

            horisontalStickDownLast = true;
        }
        else
            horisontalStickDownLast = false;
        
        if (Input.GetAxis("Vertical") < 0)
        {
            if (!verticalStickDownLast)
                gameLogic.MovePieceDown();

            verticalStickDownLast = true;
        }
        else
            verticalStickDownLast = false;

        if (Input.GetButtonDown("Jump"))
        {
            gameLogic.RotatePiece();
        }
    }
}
