using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    public GameLogic gameLogic;

    [Header("Settings")]

    [Tooltip("Current Game score")]
    public int score = 0;

    [Tooltip("Current Game score multiplier")]
    public int multiplier = 1; 

    bool combo = false; // if current player is in a line completion combo

    public int linePoints = 10; // how much points it is worth to complete a line

    #region Events

    public Action OnValuesChange;   //everytime the score change

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

    /// <summary>
    /// Update score when a line is completed
    /// </summary>
    /// <param name="numLines"></param>
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

    /// <summary>
    /// Everytime a block is placed, if it was not used to complete a line, stop combo
    /// </summary>
    /// <param name="lineCompleted"></param>
    void OnBlockPlaced(bool lineCompleted)
    {
        if (!lineCompleted)
        {
            combo = false;
            multiplier = 1;
            OnValuesChange?.Invoke();
        }
    }
}
