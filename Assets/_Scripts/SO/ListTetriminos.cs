using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ListTetriminos : ScriptableObject
{
    [Tooltip("A list of all Tetrimino's prefabs")]

    public GameObject[] tetriminos;
}
