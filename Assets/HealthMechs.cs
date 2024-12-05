using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMechs : MonoBehaviour
{
    public Image HealthBar;

    public Sprite two_hearts;
    public Sprite one_hearts;

    private int curHealth;
    private int maxHealth = 3;


    public GameObject player;
    public Sprite damagedPlayer;
    public Sprite regularPlayer;
    private Coroutine ShowDamageRoutine;

    private double timer = 0;


    public static HealthMechs instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        curHealth = maxHealth;
    }

    public void receiveDamage(Collider2D collision)
    {
        switch (curHealth) {
            case (1):
                player.GetComponent<SpriteRenderer>().sprite = damagedPlayer;
                GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>().Lose();
                break;

            case (2):
                ReceiveDamageHelper(one_hearts, 18, collision);
                break;

            case (3):
                ReceiveDamageHelper(two_hearts, 40, collision);
                break;
        }
    }

    private void ReceiveDamageHelper(Sprite newSprite, int resize, Collider2D collision)
    {
        HealthBar.sprite = newSprite;
        HealthBar.rectTransform.sizeDelta = new Vector2(resize, HealthBar.rectTransform.sizeDelta.y);
        curHealth--;
        ShowDamageRoutine = StartCoroutine(ShowDamage());
        Destroy(collision.gameObject);
    }

    private IEnumerator ShowDamage()
    {
        player.GetComponent<SpriteRenderer>().sprite = damagedPlayer;
        while(timer < 0.5)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        player.GetComponent<SpriteRenderer>().sprite = regularPlayer;
        timer = 0;
        StopCoroutine(ShowDamageRoutine);

    }
}
