using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public GameLogic gameLogic;

    [Header("Settings")]

    public int score = 0;

    public int multiplier = 1;   //  multiplier

    bool combo = false;

    public int linePoints = 10;

    #region Events

    public Action OnValuesChange;

    private void OnEnable()
    {
        gameLogic.OnBlockPlaced += OnBlockPlaced;
        gameLogic.OnLineComplete += CompleteLine;
    }
    private void OnDisable()
    {
        gameLogic.OnBlockPlaced -= OnBlockPlaced;
        gameLogic.OnLineComplete -= CompleteLine;
    }
    #endregion

    void CompleteLine(int numLines)
    {
        if (combo)
            multiplier += numLines;
        else if (numLines > 1)
            multiplier = numLines;


        combo = true;

        score += linePoints * multiplier;

        OnValuesChange?.Invoke();
    }


    void OnBlockPlaced(bool lineCompleted)
    {
        if (!lineCompleted)
        {
            combo = false;
            multiplier = 1;
        }
    }
}
