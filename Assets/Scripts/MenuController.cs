using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Il nome esatto della scena di gioco inserita in Build Settings
    public string gameSceneName = "SampleScene";

    public string gameImpSceneName = "Impostazioni";

    public string gameMenuSceneName = "SchermataIniziale";

    // Questi metodi verranno collegati via Inspector ai Button OnClick()
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(gameImpSceneName);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(gameMenuSceneName);
        Console.WriteLine("Torna al game menu");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
