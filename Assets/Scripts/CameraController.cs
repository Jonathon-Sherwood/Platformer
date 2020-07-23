using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Sets the camera's transform to match the player while keeping the z above the player to see them.
        if(GameManager.instance.player != null)
        transform.position = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, -1);
    }
}
