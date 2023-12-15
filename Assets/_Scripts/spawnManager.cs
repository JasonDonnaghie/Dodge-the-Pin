using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class spawnManager : MonoBehaviour
{

    private bool canSpawn;


    [SerializeField] float minPinSpawn = 1f;
    [SerializeField] float maxPinSpawn = 3f;

    [SerializeField] float minPowerSpawn = 5f;
    [SerializeField] float maxPowerSpawn = 10f;


    [SerializeField] float randomPinSpawn;
    [SerializeField] float randomPowerSpawn;
    


    private int randomPowerUp;
    public GameObject objPin;

 
    public GameObject[] objPowerUps = new GameObject[3];
    
    void Awake()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void Start()
    {
        randomPinSpawn = Random.Range(minPinSpawn, maxPinSpawn);
        randomPowerUp = Random.Range(0, 3);
        randomPowerSpawn = Random.Range(minPowerSpawn, maxPowerSpawn);

    }

    void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                canSpawn = false;
                maxPowerSpawn = 10f;
                minPowerSpawn = 5f;
                maxPinSpawn = 3f;
                minPinSpawn = 1f;
                break;
            case GameState.Playing:
                canSpawn = false;
                break;
            case GameState.Paused:
                canSpawn = false;
                break;
            case GameState.GameOver:
                canSpawn = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            spawnPin();
            spawnPower();  
        }
    }

    private void spawnPin()
    {
        randomPinSpawn -= Time.deltaTime;
        if (randomPinSpawn <= 0)
        {
            float randomX = Random.Range(-2.85f, 2.85f);
            Instantiate(objPin, new Vector2(randomX,6), Quaternion.identity);
            if (minPinSpawn > 0.5f)
            {
                minPinSpawn -= 0.05f;
            }else if (maxPinSpawn > 1f)
            {
                maxPinSpawn -= 0.05f;
            }

            randomPinSpawn = Random.Range(minPinSpawn, maxPinSpawn);
        }
    }

    private void spawnPower()
    {
        randomPowerSpawn -= Time.deltaTime;
        if (randomPowerSpawn <= 0)
        {
            float randomX = Random.Range(-2.85f, 2.85f);
            Instantiate(objPowerUps[randomPowerUp], new Vector2(randomX, 6), Quaternion.identity);

            randomPowerSpawn = Random.Range(minPowerSpawn, maxPowerSpawn);
            randomPowerUp = Random.Range(0, 3);
        }
    }
    
}
