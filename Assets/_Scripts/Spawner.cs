using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ListTetriminos listTetriminos;

    Vector2Int spawnPos = new Vector2Int(4, 21);    // half x size, y size +1

    [ContextMenu("Spawn Tetrimino")]
    public Tetrimino SpawnTetrimino()
    {
        int random = Random.Range(0, listTetriminos.tetriminos.Length); //  get a random tetrimino

        GameObject go = Instantiate(listTetriminos.tetriminos[random], (Vector2)spawnPos, Quaternion.identity) as GameObject;

        go.GetComponent<Tetrimino>().pos = spawnPos;

        return go.GetComponent<Tetrimino>();
    }
}
