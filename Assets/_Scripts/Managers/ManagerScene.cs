using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene instance;

    [Header("References")]
    public Animator anim;

    #region Events

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion


    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(int numScene)
    {
        AnimateTransition();
        //SceneManager.LoadScene(numScene);
        StartCoroutine(LoadYourAsyncScene(numScene));
    }

    void AnimateTransition()
    {
        //anim.ResetTrigger("Exit");
        //anim.SetTrigger("Enter");

        anim.SetBool("LoadingScene", true);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //anim.SetTrigger("Exit");
        //anim.SetBool("LoadingScene", false);
    }

    public void ReloadScene()
    {
        int numScene = SceneManager.GetActiveScene().buildIndex;

        LoadScene(numScene);
    }


    IEnumerator LoadYourAsyncScene(int numScene)
    {
        yield return new WaitForSeconds(0.3f); // wait start of transition animation


        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numScene);


        bool animDone = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone || !animDone)
        {
            animDone = anim.GetCurrentAnimatorStateInfo(0).IsName("Enter") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            yield return null;
        }

        anim.SetBool("LoadingScene", false);
    }
}
