using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    /// <summary>
    /// Update the position of the transform location of the block
    /// </summary>
    /// <param name="newPos"></param>
    public void UpdPos(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }
}
