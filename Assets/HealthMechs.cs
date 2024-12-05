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

    public void receiveDamage()
    {
        switch (curHealth) {
            case (1):
                GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>().Lose();
                break;

            case (2):
                HealthBar.sprite = one_hearts;
                HealthBar.rectTransform.anchoredPosition = new Vector2(97, HealthBar.rectTransform.anchoredPosition.y);
                HealthBar.rectTransform.sizeDelta = new Vector2(18, HealthBar.rectTransform.sizeDelta.y);
                curHealth--;
                break;

            case (3):
                HealthBar.sprite = two_hearts;
                HealthBar.rectTransform.anchoredPosition = new Vector2(57, HealthBar.rectTransform.anchoredPosition.y);
                HealthBar.rectTransform.sizeDelta = new Vector2(40, HealthBar.rectTransform.sizeDelta.y);
                curHealth--;
                break;
        }
    }
}
