using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloon;
    public double spawnRate = 2;
    private double timer;

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //-9 < x < 8
            //y = 33
            Instantiate(balloon, new Vector3(Random.Range(-9, 8), 33, 0), new Quaternion(0, 0, 0, 0));
            timer = 0;
        }
    }
}
