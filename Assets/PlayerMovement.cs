using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float move = 0;
    public Vector2 direction = Vector2.zero;
    public Rigidbody2D player;
    public float velocity = 6;
    private bool jump = true;
    public LineRenderer moveCheck;
    public float variance;
    public int fastfall;
    private bool onGround = true;
    public GameObject character;
    public float boostPower;
    private Coroutine boostRoutine;
    private bool midBoostLimiter = true;
    public float counterVel;
    public float velCap;
    public bool hasWon = true;
    public GameObject gameOverScreen;

    private void Start()
    {
        hasWon = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon)
        {
            Vector2 player_force = getDirection();
            if (player.velocity.x < -velCap && player_force.x > 1)
            {
                player.AddForce(new Vector2(player_force.x * move, 0));
            }
            else if (player.velocity.x > velCap && player_force.x < 1)
            {
                player.AddForce(new Vector2(player_force.x * move, 0));
            }
            else if (player.velocity.x > -velCap && player.velocity.x < velCap)
            {
                player.AddForce(new Vector2(player_force.x * move, 0));
            }
            if (player_force.x == 0 && onGround)
            {
                if (player.velocity.x < 0)
                {
                    player.AddForce(new Vector2(counterVel, 0));
                }
                else
                {
                    player.AddForce(new Vector2(-counterVel, 0));
                }
            }
            else if (player_force.x == 0 && !onGround)
            {
                if (player.velocity.x < 0)
                {
                    player.AddForce(new Vector2(counterVel / 2, 0));
                }
                else
                {
                    player.AddForce(new Vector2(-counterVel / 2, 0));
                }
            }
            player.AddForce(new Vector2(0, player_force.y * velocity));
            transform.rotation = new Quaternion(0, 0, 0, 0);

            if (character.GetComponent<grapple>().boostAvailable)
            {
                boostRoutine = StartCoroutine(Boost());
            }
            else if (boostRoutine != null)
            {
                StopCoroutine(boostRoutine);
            }
            player.AddForce(new Vector2(player.velocity.normalized.x * -1 * counterVel, 0));
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
            onGround = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
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
            HealthMechs.instance.receiveDamage();
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
