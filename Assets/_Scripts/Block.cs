using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void UpdPos(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }
}
