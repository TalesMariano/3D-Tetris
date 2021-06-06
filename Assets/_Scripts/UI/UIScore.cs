using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public ScoreManager scoreManager;

    public Text textScore;
    public Text textCombo;

    #region Events
    private void OnEnable()
    {
        scoreManager.OnValuesChange += UpdateText;
    }

    private void OnDisable()
    {
        scoreManager.OnValuesChange -= UpdateText;
    }
    #endregion


    /// <summary>
    /// Update Score and combo text
    /// </summary>
    void UpdateText()
    {
        textScore.text = " Score: " + scoreManager.score;
        textCombo.text = " Combo: " + scoreManager.multiplier + "x";
    }
}
