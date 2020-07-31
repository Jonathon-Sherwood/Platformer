using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ends the level when the player reaches the destination.
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.playerLives++;
            GameManager.instance.LoadNextScene();
        }
    }
}
