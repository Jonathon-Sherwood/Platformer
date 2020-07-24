using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{

    public void LoadMainMenu()
    {
        GameManager.instance.LoadLevel(1);
    }
}

