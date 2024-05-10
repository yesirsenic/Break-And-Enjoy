using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    private GameManager gameManager;
    private SoundManager soundManager;

    public GameObject gameoverObject;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    public void GameOver()
    {
        if (gameManager.isActivate)
        {
            gameManager.isActivate = false;
            gameoverObject.SetActive(true);
            soundManager.GameOver();
        }
            

    }
}
