using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour
{
    public int balloonSpeed = 6;
    public Rigidbody2D balloon_physics;

    void Start()
    {
        balloon_physics.AddForce(new Vector2(0, balloonSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 72)
        {
            Destroy(gameObject);
        }
    }
}
