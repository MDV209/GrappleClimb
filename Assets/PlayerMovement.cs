using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float move = 0;
    public float velocity = 6;
    public float variance;
    public float fastfall;
    public float boostPower;
    public float counterVel;
    public Vector2 direction = Vector2.zero;
    public Rigidbody2D player;
    private bool jump = true;
    public LineRenderer moveCheck;
    public GameObject character;
    private Coroutine boostRoutine;
    private bool midBoostLimiter = true;
    public bool hasWon = true;
    public GameObject gameOverScreen;

    private void Start()
    {
        hasWon = true;
        velocity *= Time.deltaTime;
        move *= Time.deltaTime;
        variance *= Time.deltaTime;
        fastfall *= Time.deltaTime;
        boostPower *= Time.deltaTime;
        counterVel *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon)
        {
            Vector2 player_force = getDirection();
            HorizontalMovement(player_force);
            VerticalMovement(player_force);
            transform.rotation = new Quaternion(0, 0, 0, 0);




            if (character.GetComponent<grapple>().boostAvailable)
            {
                boostRoutine = StartCoroutine(Boost());
            }
            else if (boostRoutine != null)
            {
                StopCoroutine(boostRoutine);
            }
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    Vector3 getDirection()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        //Make a line renderer from player to place player wants to go and then check if that line is in ground *****NOPE*****
        /*
        moveCheck.SetPosition(0, transform.position);
        moveCheck.SetPosition(1, transform.position + direction * move);
        moveCheck.enabled = true;
        */

        if (Input.GetButtonDown("Jump") && jump)
        {
            direction.y = 1;
            jump = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1 / (float)fastfall;
        }
        else
        {
            direction.y = 0;
        }
        //Raycast and store hit location and check
        
        if (Physics2D.Raycast(transform.position, direction.normalized))
        {

            RaycastHit2D _hit = Physics2D.Raycast(transform.position, direction.normalized);
            if (_hit.transform.gameObject.layer == 6)
            {
                if (Vector2.Distance(_hit.point, transform.position) <= variance)
                {
                    direction.x = 0;
                }
            }
        }
        return direction;
    }

    private void HorizontalMovement(Vector2 player_force)
    {
        player.AddForce(new Vector2(player_force.x * move, 0));
        player.AddForce(new Vector2(-player.velocity.x * counterVel, 0));
    }
    private void VerticalMovement(Vector2 player_force)
    {
        player.AddForce(new Vector2(0, player_force.y * velocity));
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flag")
        {
            Win();
        }
        if (collision.gameObject.tag == "projectile")
        {
            HealthMechs.instance.receiveDamage(collision);
        }
    }

    public void Win()
    {
        hasWon = true;
        player.gravityScale = 0;
        character.GetComponent<grapple>().deGrapple();
        player.bodyType = RigidbodyType2D.Static;
        gameOverScreen.SetActive(true);
        
    }

    public void Lose()
    {
        hasWon = true;
        player.gravityScale = 0;
        character.GetComponent<grapple>().deGrapple();
        player.bodyType = RigidbodyType2D.Static;
        gameOverScreen.SetActive(true);
    }
    private IEnumerator Boost()
    {
        
        while(
            Input.GetKey(KeyCode.LeftShift) && 
            boostMechs.instance.enoughBoost(5) && 
            midBoostLimiter)
        {
            midBoostLimiter = false;
            player.AddForce(new Vector2(direction.x * boostPower, 0));
            boostMechs.instance.useBoost(5);
            yield return new WaitForSeconds(0.1f);
            midBoostLimiter = true;
        }
    }
}
