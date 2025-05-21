using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{

    public Toggle fullscreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Inizializza il toggle allo stato corrente dello schermo
        fullscreenToggle.isOn = Screen.fullScreen;

        // Aggiungi l'evento per cambiare la modalità
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
}
