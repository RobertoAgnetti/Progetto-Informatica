using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    [Tooltip("L'oggetto da attivare/disattivare")]
    public GameObject targetObject;

    [Tooltip("Tag del giocatore (di solito 'Player')")]
    public string playerTag = "Player";

    void Reset()
    {
        // Assicura che il collider sia trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && targetObject != null)
            targetObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && targetObject != null)
            targetObject.SetActive(false);
    }
}
