using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer_Movement : MonoBehaviour
{
    public Rigidbody2D player;
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        float player_pos = player.transform.position.y;
        mainCamera.transform.position = new Vector3((float)-0.5, player_pos + 3, -10);
        
        /*
        Vector3 camera_position = mainCamera.transform.position;
        if (mainCamera.transform.position.y - player.position.y > 8)
        {
            camera_position.y -= (float)0.2;
            mainCamera.transform.position = camera_position;
        }
        else if (mainCamera.transform.position.y - player.position.y > 4)
        {
            camera_position.y -= (float)0.1;
            mainCamera.transform.position = camera_position;
        }
        if (mainCamera.transform.position.y - player.position.y < -8)
        {
            if (mainCamera.transform.position.y < 63.7)
            {
                camera_position.y += (float)0.2;
                mainCamera.transform.position = camera_position;
            }
        }
        else if (mainCamera.transform.position.y - player.position.y < -4)
        {
            if (mainCamera.transform.position.y < 63.7)
            {
                camera_position.y += (float)0.1;
                mainCamera.transform.position = camera_position;
            }
        }
        */
    }
}
