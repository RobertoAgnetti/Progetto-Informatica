using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenuManager : MonoBehaviour
{
    public GameObject pausaMenu;
    private bool isInPausa = false;

    private void Start()
    {
        pausaMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isInPausa) Resume();
            else Pausa();
        }


    }

    public void Pausa()
    {
        pausaMenu.SetActive(true);
        Time.timeScale = 0f;
        isInPausa = true ;
    }

    public void Resume()
    {
        pausaMenu.SetActive(false);
        Time.timeScale = 1f;
        isInPausa = false ;

    }

    public void VaiAlMenu()
    {
        SceneManager.LoadScene("SchermataIniziale");
        
    }
}
