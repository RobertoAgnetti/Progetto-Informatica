using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{

    public Toggle fullscreenToggle;

    // copiata da un sito, non so se funziona
    void Start()
    {
        
        fullscreenToggle.isOn = Screen.fullScreen;

        
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
}
