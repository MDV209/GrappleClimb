using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_spawner : MonoBehaviour
{
    public GameObject projectile;
    public double spawnRate = 2;
    private double timer;

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>().hasWon)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
                timer = 0;
            }
        }
    }
}
