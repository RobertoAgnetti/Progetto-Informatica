using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class stanzaVisuale : MonoBehaviour
{
    public Transform giocatore;
    public float larghezzaStanza = 16f;
    public float altezzaStanza = 9f;

    private Vector2Int stanzaCorrente;
    public Vector2Int stanzaIniziale = Vector2Int.zero;


    private void Start()
    {
        stanzaCorrente = stanzaIniziale;
        CentraCameraSuStanza(stanzaCorrente);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2Int nuovaStanza = new Vector2Int(
                
            Mathf.FloorToInt(giocatore.position.x / larghezzaStanza),
            Mathf.FloorToInt(giocatore.position.y / altezzaStanza)
            );

        if (nuovaStanza != stanzaCorrente)
        {
            stanzaCorrente = nuovaStanza;
            CentraCameraSuStanza(stanzaCorrente);
        }
    }

    private void CentraCameraSuStanza(Vector2Int stanza)
    {
        transform.position = new Vector3(
                stanza.x * larghezzaStanza + larghezzaStanza / 2f,
                stanza.y * altezzaStanza + altezzaStanza / 2f,
                -10f
            );
    }
}
