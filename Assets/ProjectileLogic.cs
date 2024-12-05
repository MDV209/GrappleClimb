using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D projectile;
    public float despawnRadius = 5;
    public float projectile_vel = 6;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        Vector3 distance_diff = player.transform.position - transform.position;
        projectile.AddTorque((float)100);
        projectile.AddForce(distance_diff.normalized * projectile_vel);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerMovement>().hasWon)
        {
            Vector3 distance_diff = player.transform.position - transform.position;
            float x_diff = Mathf.Abs(distance_diff.x);
            float y_diff = Mathf.Abs(distance_diff.y);
            if (Mathf.Sqrt(x_diff * x_diff + y_diff * y_diff) > despawnRadius)
            {
                delete_projectile();
            }
        }
        else
        {
            projectile.velocity = new Vector2(0, 0);
        }
    }

    void delete_projectile()
    {
        Destroy(gameObject);
    }
}
