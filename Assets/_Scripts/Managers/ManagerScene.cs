using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene instance;

    [Header("References")]
    [Tooltip("Transition Animation Animator")]
    public Animator anim;

    private void Awake()
    {
        // Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Load Scene asynchronously
    /// </summary>
    /// <param name="numScene">Index of scene</param>
    public void LoadScene(int numScene)
    {
        StartAnimateTransition();
        StartCoroutine(LoadYourAsyncScene(numScene));
    }

    void StartAnimateTransition()
    {
        anim.SetBool("LoadingScene", true);
    }

    /// <summary>
    /// Reload current Scene
    /// </summary>
    public void ReloadScene()
    {
        int numScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(numScene);
    }


    IEnumerator LoadYourAsyncScene(int numScene)
    {
        yield return new WaitForSeconds(0.3f); // wait start of transition animation

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numScene);   // Load scene Async


        bool animDone = false;  // if the start part of animation  transition is over

        // Wait until the asynchronous scene fully loads and animation is finished
        while (!asyncLoad.isDone || !animDone)
        {
            animDone = anim.GetCurrentAnimatorStateInfo(0).IsName("Enter") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            yield return null;
        }

        anim.SetBool("LoadingScene", false);
    }
}
