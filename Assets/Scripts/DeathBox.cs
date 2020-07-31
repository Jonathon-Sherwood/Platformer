using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kills the player on contact, used at the bottom of the screen for falling death.
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
