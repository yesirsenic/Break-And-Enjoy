using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector2 spawnPos;
    public ParticleSystem explosion_Particle;


    private float gameover_Count = 1f;
    private Vector3 FX_SpawnPos;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private GameOverManager gameOverManager;
    private SoundManager soundManager;
    private GameObject bang;
    

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameOverManager = GameObject.Find("GameOverManager").GetComponent<GameOverManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        bang = GameObject.Find("Bang");
        FX_SpawnPos = new Vector3(transform.position.x, transform.position.y + 0.3f , -1f);

        

        StartCoroutine(GameOver());
    }


    private void OnMouseDown()
    {
        if(gameManager.isActivate)
        {
            spawnManager.spawnCheck[(int)spawnPos.x, (int)spawnPos.y] = false;
            soundManager.Explosion(bang.GetComponent<AudioSource>());
            Instantiate(explosion_Particle, FX_SpawnPos, transform.rotation);
            Destroy(gameObject);


        }
    }

    IEnumerator GameOver()
    {
        if (gameManager.isActivate)
        {
            yield return new WaitForSeconds(gameover_Count);

            gameOverManager.GameOver();
        }
    }

    
}
