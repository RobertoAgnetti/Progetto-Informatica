using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ControlliGiocatore player = collision.GetComponent<ControlliGiocatore>();
        if (player != null)
        {
            player.Muori();
        }
    }
}
