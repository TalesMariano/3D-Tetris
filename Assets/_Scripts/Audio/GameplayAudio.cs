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

    //events
    private void OnEnable()
    {
        gameLogic.OnBlockPlaced += PlaySFX;
    }

    private void OnDisable()
    {
        gameLogic.OnBlockPlaced -= PlaySFX;
    }


    void PlaySFX(bool lineCompleted)
    {
        if(lineCompleted)
            audioSource.PlayOneShot(aLineCompleted);
        else
            audioSource.PlayOneShot(aBlockPlaced);
    }

}
