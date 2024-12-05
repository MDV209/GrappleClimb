using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public GameObject startScreen;
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BeginGame()
    {
        startScreen.SetActive(false);
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>().hasWon = false;
    }
}
