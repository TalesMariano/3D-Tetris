using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Access the ManageScene Singleton and change scene
/// </summary>
public class ButtonLoadScene : MonoBehaviour
{
    [SerializeField] int numScene;

    /// <summary>
    /// Load Scene from index
    /// </summary>
    public void LoadScene()
    {
        ManagerScene.instance.LoadScene(numScene);
    }

    /// <summary>
    /// Restart current Scene
    /// </summary>
    public void RestartScene()
    {
        ManagerScene.instance.ReloadScene();
    }
}
