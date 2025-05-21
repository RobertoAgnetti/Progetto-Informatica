
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class ControlliGiocatore : MonoBehaviour
{
    //movimenti
    public float velocit‡Movimento = 5f;
    public float forzaDiSalto = 12f;
    public float velocit‡Scivolata = 0.5f;
    public float tempoAggrappoMassimo = 3f;
    public float forzaSaltoMuro = 5f;
    public float spintaSaltoMuro = 5f;

    //timer
    private float timerAggrappo = 0f;

    //rigid body e bool
    private Rigidbody2D rb; //riferimento al componenete Rigid Body 2D
    private BoxCollider2D coll; //riferimento al box collider 2d
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public bool ËPerterra;
    public bool ËVicinoAlMuro;
    private bool aggrappatoAlMuro;
    private bool staMuovendo;

    //Dash
    public float forzaDash = 10f;
    public float durataDash = 0.2f;
    public float CooldownDash = 0.5f;
    public Color coloreDash = Color.cyan; // Opzionale: cambia colore durante il dash
    private bool haDashato = false;
    private bool staDashando = false;
    private Color coloreOriginale;

    //vita
    public bool ËMorto = false;


    public LayerMask layerMuro;
    public SpriteRenderer spriteGiocatore;

    private void Start()
    {
        coloreOriginale = spriteGiocatore.color;
        rb = GetComponent<Rigidbody2D>(); //prende il componente rigid body 2d
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public LayerMask layerTerreno; // layer per il pavimento 
    private bool haSaltatoDalMuro = false;

    private void Update()
    {

        ËPerterra = ControlloPerterra();
        ËVicinoAlMuro = ControlloMuro();

        float movimentoInput = Input.GetAxisRaw("Horizontal");
        


        float velocit‡Animazione = Mathf.Abs(movimentoInput);
        anim.SetFloat("Speed", velocit‡Animazione);

        anim.SetBool("perterra", ËPerterra);

        anim.SetBool("appeso", aggrappatoAlMuro);

        anim.SetBool("dashando", staDashando);


        if (Input.GetKeyDown(KeyCode.LeftShift) && !haDashato && !staDashando)
        {
            StartCoroutine(FareDash());
        }

        // Reset del dash quando tocchi terra
        if (ËPerterra)
        {
            haDashato = false;
        }


        //AGGRAPPO AL MURO
        aggrappatoAlMuro = ËVicinoAlMuro && Input.GetMouseButton(0) && !ËPerterra && !haSaltatoDalMuro;

        if (aggrappatoAlMuro)
        {

            timerAggrappo += Time.deltaTime;

            if (timerAggrappo < tempoAggrappoMassimo)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0f;
            }
            else
            {
                rb.gravityScale = 1f;
                rb.velocity = new Vector2(0f, -velocit‡Scivolata);
            }


        }
        else
        {
            //RESET
            timerAggrappo = 0f;
            rb.gravityScale = 3f;

            //MOVIMENTI NORMALI
            

            if (!staDashando) // Solo se non sta dashando
            {
                rb.velocity = new Vector2(movimentoInput * velocit‡Movimento, rb.velocity.y);

            if (movimentoInput != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(movimentoInput), 1f, 1f); //in base a dove vado
                          // con l'input mi calcola la posizione a +1 se vado a destra e a -1 se vado a sinistra
            }
                rb.velocity = new Vector2(movimentoInput * velocit‡Movimento, rb.velocity.y);
                
            }


        }


        //SALTO
        if (ËPerterra && Input.GetKeyDown(KeyCode.Space))
        {
            Salto();
        }

        if (aggrappatoAlMuro && Input.GetKeyDown(KeyCode.Space))
        {
            SaltoDalMuro();
        }

        if (haDashato && !ËPerterra)
        {
            spriteGiocatore.color = coloreDash;
        }
        else
        {
            spriteGiocatore.color = coloreOriginale;
        }



    }

    private bool ControlloPerterra()
    {
        float distanzaRaggio = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanzaRaggio, layerTerreno);
        //spara un raggio che verifica se c'Ë il terreno
        return hit.collider != null;

    }

    private bool ControlloMuro()
    {
        float distanza = 0.27f;
        RaycastHit2D hitDestro = Physics2D.Raycast(transform.position, Vector2.right, distanza, layerMuro);
        RaycastHit2D hitSinistro = Physics2D.Raycast(transform.position, Vector2.left, distanza, layerMuro);

        return hitDestro.collider != null || hitSinistro.collider != null;
    }

    private void Salto()
    {
        rb.velocity = new Vector2(rb.velocity.x, forzaDiSalto);
    }

    private void SaltoDalMuro()
    {
        float direzioneSalto = transform.localScale.x > 0 ? -1 : 1;

        rb.velocity = new Vector2(direzioneSalto * spintaSaltoMuro, forzaSaltoMuro);

        aggrappatoAlMuro = false;
        haSaltatoDalMuro = true;

        Invoke(nameof(ResetSaltoMuro), 0.1f);

        timerAggrappo = 0f;
        rb.gravityScale = 1f;

    }
    private void ResetSaltoMuro()
    {
        haSaltatoDalMuro = false;
    }

    private IEnumerator FareDash()
    {
        haDashato = true;
        staDashando = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Direzione del dash (basata sull'input o sulla direzione del personaggio)
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Se nessuna direzione input, dash nella direzione corrente
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);
        }

        rb.velocity = dashDirection * forzaDash;
        

        yield return new WaitForSeconds(durataDash);

        // Ripristina valori originali
        rb.gravityScale = originalGravity;
        
        staDashando = false;

            yield return new WaitForSeconds(CooldownDash);
        // Dopo il cooldown, il dash puÚ essere riusato solo se si tocca terra
    }

    public void RipristinaDash()
    {
        haDashato = false;
        spriteGiocatore.color = coloreOriginale;
    }

    public float moltiplicatoreCaduta = 1.5f; // Regola quanto velocemente cade (1.5-2.5)
    public float maxVelocit‡Caduta = -20f; // Velocit‡ massima di caduta

    private void FixedUpdate()
    {
        // Controllo caduta pi˘ rapida e limitata
        if (rb.velocity.y < 0 && !ËPerterra)
        {
            // 1. Applica una gravit‡ extra durante la caduta
            rb.velocity += Vector2.up * Physics2D.gravity.y * (moltiplicatoreCaduta - 1) * Time.fixedDeltaTime;

            // 2. Limita la velocit‡ di caduta
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, maxVelocit‡Caduta));
        }
    }

    public void Muori()
    {
        if (ËMorto) return;

        ËMorto = true;

        enabled = false; //disabilita i controlli


        if (TryGetComponent<Animator>(out var anim)) //animazione morte
        {
            anim.SetTrigger("Muori");
        }

        StartCoroutine(RicaricaLivello());

    }

    private IEnumerator RicaricaLivello()
    {
        yield return new WaitForSeconds(2f); 
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    

}
