using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Tooltip("List of windows")]
    public GameObject[] listWindows;

    /// <summary>
    /// Open the main screen
    /// </summary>
    [ContextMenu("Open Main Menu")]
    void OpenMainMenu()
    {
        OpenWindow(0);
    }

    /// <summary>
    /// Open specific windowm and close others
    /// </summary>
    /// <param name="numWindow">windows index number on listWindows</param>
    public void OpenWindow(int numWindow)
    {
        // Prevent invalid window index
        if (numWindow < 0 || numWindow >= listWindows.Length)
        {
            Debug.Log("Invalid window index");
            numWindow = 0;
        }

        // Close all windows
        foreach (var item in listWindows)
        {
            item.SetActive(false);
        }

        // Open select window
        listWindows[numWindow].SetActive(true);
    }
}
