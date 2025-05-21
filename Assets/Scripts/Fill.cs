using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill : MonoBehaviour
{

    public float tempoDisattivazione = 4f;
    

    
    private SpriteRenderer spriteRenderer;
    private Collider2D colliderOggetto;
    private Sprite spriteOriginale;
    private bool ËAttivo = true;

    //ANIMAZIONI
    public AnimationClip animazioneAttivo; 
    public AnimationClip animazioneUtilizzo;
    public AnimationClip animazioneDisattivo;
    public AnimationClip animazioneRiattivazione;

    private Animator anim;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderOggetto = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        anim.Play(animazioneAttivo.name);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ËAttivo) return;

        ControlliGiocatore giocatore = collision.GetComponent<ControlliGiocatore>();
        if (giocatore != null)
        {
            giocatore.RipristinaDash();
            StartCoroutine(CicloAnimazioni());
        }
    }

    private IEnumerator CicloAnimazioni()
    {
        

        //animazione utilizzo
        ËAttivo = false;
        anim.Play(animazioneUtilizzo.name);
        colliderOggetto.enabled = false;
        yield return new WaitForSeconds(animazioneUtilizzo.length);

        //animazione in loop di disattivazione
        anim.Play(animazioneDisattivo.name);

        //timer per la riattivazione
        yield return new WaitForSeconds(tempoDisattivazione - animazioneUtilizzo.length - animazioneRiattivazione.length);

        //riattivazione
        anim.Play(animazioneRiattivazione.name);
        yield return new WaitForSeconds(animazioneRiattivazione.length);

        // Ripristina tutto
        anim.Play(animazioneAttivo.name);
        ËAttivo = true;
        colliderOggetto.enabled = true;
    }

  
}
