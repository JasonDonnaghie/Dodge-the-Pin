using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class pinLogic : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Random.Range(0.5f, 2f);
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
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && gameManager.Instance.State == GameState.Playing )
        {
            gameManager.Instance.increasePoints();
            Destroy(gameObject);
        }else if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
