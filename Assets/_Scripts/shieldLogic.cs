using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class shieldLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    void Awake()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestory()
    {
        gameManager.OnGameStateChanged -= OnGameStateChanged;
    }  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Random.Range(1f, 1.5f);
    }

    void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                break;
        }
    }
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
