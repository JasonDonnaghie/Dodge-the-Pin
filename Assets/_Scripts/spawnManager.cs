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

    [SerializeField] float minDartSpawn = 15f;
    [SerializeField] float maxDartSpawn = 30f;


    [SerializeField] float randomPinSpawn;
    [SerializeField] float randomPowerSpawn;
    [SerializeField] float randomDartSpawn;
    


    private int randomPowerUp;
    public GameObject objPin;

 
    public GameObject[] objPowerUps = new GameObject[3];
    public GameObject objDart;
    public GameObject[] objWarning = new GameObject[2];
    
    void Awake()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void Start()
    {
        randomPinSpawn = Random.Range(minPinSpawn, maxPinSpawn);
        randomPowerUp = Random.Range(0, 3);
        randomPowerSpawn = Random.Range(minPowerSpawn, maxPowerSpawn);
        randomDartSpawn = Random.Range(minDartSpawn, maxDartSpawn);
        objWarning[0].SetActive(false);
        objWarning[1].SetActive(false);

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
                minDartSpawn = 15f;
                maxDartSpawn = 30f;
                break;
            case GameState.Playing:
                canSpawn = true;
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
            spawnDart();
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

    private void spawnDart()
    {
        int randomSide = Random.Range(0, 2);
        randomDartSpawn -= Time.deltaTime;
        if (randomDartSpawn <= 0)
        {
            
            StartCoroutine(spawnWarning(randomSide));
            randomDartSpawn = Random.Range(minDartSpawn, maxDartSpawn);
        }
    }

    private IEnumerator spawnWarning(int side)
    {
        objWarning[side].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        objWarning[side].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (side == 0)
        {
            Instantiate(objDart, new Vector2(-2.9f, -4.7f), Quaternion.identity);
        }
        else
        {
            Instantiate(objDart, new Vector2(2.9f, -4.7f), Quaternion.identity);
        }

        
    }
    
}
