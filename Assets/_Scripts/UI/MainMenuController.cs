using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] listWindows;


    [ContextMenu("Open Main Menu")]
    void OpenMainMenu()
    {
        OpenWindow(0);
    }

    public void OpenWindow(int numWindow)
    {
        numWindow = Mathf.Clamp(numWindow, 0, listWindows.Length-1);

        foreach (var item in listWindows)
        {
            item.SetActive(false);
        }

        listWindows[numWindow].SetActive(true);
    }
}
