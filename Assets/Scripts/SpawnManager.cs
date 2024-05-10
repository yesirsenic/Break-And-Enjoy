using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private int fast_Count = 0; // 연달아 등장할 시, 몇번 연달아 등장할 지에 대한 횟수
    private int maxfast_Count = 2; // 최대 연달아 등장할 수 있는 횟수
    private float startDelay = 1.5f; 
    private float repeatInterval = 1f;
    private float chagne_RepeatInterval = 1f; // 시간이 갈수록 생성 간격이 줄어드므로, 이를 저장하기 위한 변수
    private float fastRepeat_Interval = 0.3f; // 연달아 등장할 시의 생성 간격
    private float time;

    private GameManager gameManager;

    public GameObject[] spawnXPos;
    public GameObject[] spawnYPos;
    public GameObject click_Object;
    public bool[,] spawnCheck = new bool[6, 6]; // 어느 지점에 오브젝트가 생성되어있는지 확인하는 변수
    public Text time_Text;



    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        SpawnCheck_Init_();

        time = 0;

        time_Text.text = "Time: " + time;

        StartCoroutine(SpawnLoop());

        StartCoroutine(TimeFlow());
    }

    private void ObjectSpawn()
    {
        if(randomBool() == true && fast_Count ==0)
        {
            fast_Count = Random.Range(1, maxfast_Count);
            repeatInterval = fastRepeat_Interval;
        }

        int randomX = Random.Range(0, spawnXPos.Length);

        int randomY = Random.Range(0, spawnYPos.Length);

        while(spawnCheck[randomX,randomY])
        {
            randomX = Random.Range(0, spawnXPos.Length);

            randomY = Random.Range(0, spawnYPos.Length);
        }

        Vector3 spawnPos = new Vector3(spawnXPos[randomX].transform.position.x, spawnYPos[randomY].transform.position.y , 0);

        GameObject target = Instantiate(click_Object, spawnPos, click_Object.transform.rotation);

        target.GetComponent<Target>().spawnPos = new Vector2(randomX, randomY);

        spawnCheck[randomX, randomY] = true;

        if(fast_Count != 0)
        {
            fast_Count -= 1;
        }

        
        else if(fast_Count == 0)
        {
            repeatInterval = chagne_RepeatInterval;
        }
    }

    bool randomBool() // 랜덤으로 true, false 결과값 도출
    {
        bool randBool = (Random.value > 0.5f);

        return randBool;
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while(gameManager.isActivate)
        {
            ObjectSpawn();

            yield return new WaitForSeconds(repeatInterval);
        }

    }

    private void SpawnCheck_Init_()
    {
        for(int i=0; i<6; i++)
        {
            for(int j=0; j<6; j++)
            {
                spawnCheck[i, j] = false;
            }
        }
    }

    IEnumerator TimeFlow()
    {
        while(gameManager.isActivate)
        {
            yield return new WaitForSeconds(1f);

            time += 1;

            repeatInterval -= 0.01f;

            time_Text.text = "Time: " + time;
        }
    }


    
    

}
