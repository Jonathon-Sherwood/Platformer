using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    //Used for victory/loss screens to reset game on button click.
    public void LoadMainMenu()
    {
        GameManager.instance.LoadLevel(1);
        GameManager.instance.playerLives = GameManager.instance.MaxPlayerLives;
    }
}

