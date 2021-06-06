using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    [Header("References")]
    public GameLogic gameLogic;
    public AudioSource audioSource;
    [Space]
    public AudioClip aBlockPlaced;
    public AudioClip aLineCompleted;

    #region Events
    private void OnEnable()
    {
        gameLogic.OnBlockPlaced += PlaySFX;
    }

    private void OnDisable()
    {
        gameLogic.OnBlockPlaced -= PlaySFX;
    }
    #endregion


    /// <summary>
    /// Play block placed sfx
    /// If Lineis complete, play line completed sfx
    /// </summary>
    /// <param name="lineCompleted"></param>
    void PlaySFX(bool lineCompleted)
    {
        if(lineCompleted)
            audioSource.PlayOneShot(aLineCompleted);
        else
            audioSource.PlayOneShot(aBlockPlaced);
    }
}
