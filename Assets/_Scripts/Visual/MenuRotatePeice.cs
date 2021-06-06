using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotatePeice : MonoBehaviour
{
    public float rotateSpeed = 1;

    // Rotate object contanstily
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
