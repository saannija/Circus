using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{

    public FadeScript fadeScript;
    public SaveLoadScript saveLoadScript;

    public void CloseGame()
    {
        StartCoroutine(Delay("quit", -1, ""));
    }

    public IEnumerator Delay(string command, int characterIndex, string name)
    {
        if (string.Equals(command, "quit", StringComparison.OrdinalIgnoreCase))
        {
            yield return fadeScript.FadeIn(0.05f);
            PlayerPrefs.DeleteAll();
            if(UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }
        else if (string.Equals(command, "play", StringComparison.OrdinalIgnoreCase))
        {
            yield return fadeScript.FadeIn(0.05f);
            saveLoadScript.SaveGame(characterIndex, name);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
