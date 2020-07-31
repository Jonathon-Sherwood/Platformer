using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject flag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sets a new spawn point in the game manager when the player reaches the checkpoint.
        if (collision.CompareTag("Player"))
        {
            flag.SetActive(true);
            AudioManager.instance.Play("Checkpoint");
            GameManager.instance.spawnPoint = this.gameObject;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
