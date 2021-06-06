using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInputMove : MonoBehaviour
{
    public Tetrimino tetrimino;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            tetrimino.MoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            tetrimino.MoveRight();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            tetrimino.MoveDown();

        if(Input.GetKeyDown(KeyCode.Space))
            tetrimino.Rotate();
    }
}
