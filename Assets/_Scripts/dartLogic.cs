using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class dartLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isLeft;
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
        if (transform.position.x < 0)
        {
            rb.velocity = new Vector2(3f, 0f);
            isLeft = true;
        }else
        {
            rb.velocity = new Vector2(-3f, 0f);
            isLeft = false;
        }
        
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
        if (isLeft)
        {
            if (transform.position.x > 3.1f)
            {
                Destroy(gameObject);
            }
        }else
        {
            if (transform.position.x < -3.1f)
            {
                Destroy(gameObject);
            }
        }
    }

}
