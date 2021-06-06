using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadScene : MonoBehaviour
{
    [SerializeField] int numScene;

    public void LoadScene()
    {
        ManagerScene.instance.LoadScene(numScene);
    }

    public void RestartScene()
    {
        ManagerScene.instance.ReloadScene();
    }
}
