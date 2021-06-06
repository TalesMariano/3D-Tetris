using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatable
{
    void Rotate(Tetrimino tetrimino, GridArea grid);
}
