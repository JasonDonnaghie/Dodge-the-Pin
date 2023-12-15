using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class playerLogic : MonoBehaviour
{   
    [SerializeField] KeyCode moveLeft = KeyCode.A;
    [SerializeField] KeyCode moveRight = KeyCode.D;
    [SerializeField] KeyCode playerJump = KeyCode.Space;
    [SerializeField] KeyCode playerPause = KeyCode.Escape;
    [SerializeField] KeyCode playerRestart = KeyCode.R;

    [SerializeField] float speed = 1f;
    [SerializeField] float dazedTime;
    private float dazedTimeCounter;

    private Rigidbody2D rb;
    private bool isProtected = false;
    private bool isGrounded;

    void Awake()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                transform.position = new Vector2(0, -3);
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
        if (gameManager.Instance.State == GameState.Start)
        {
            if (Input.GetKeyDown(playerJump))
            {
                gameManager.Instance.updateGameState(GameState.Playing);
            }
        }
        else if (gameManager.Instance.State == GameState.Playing)
        {
            movePlayer();
            if (Input.GetKeyDown(playerPause))
            {
                gameManager.Instance.updateGameState(GameState.Paused);
            }
        }
        else if (gameManager.Instance.State == GameState.Paused)
        {
            if (Input.GetKeyDown(playerPause))
            {
                gameManager.Instance.updateGameState(GameState.Playing);
            }
        }
        else if (gameManager.Instance.State == GameState.GameOver)
        {
            if (Input.GetKeyDown(playerRestart))
            {
                gameManager.Instance.updateGameState(GameState.Start);
            }
        }
        
    }

    private void movePlayer()
    {
        if (dazedTimeCounter <= 0)
        {
            jumpPlayer();
            if (Input.GetKey(moveLeft))
            {
                rb.velocity += new Vector2(-speed, 0);
            }else if (Input.GetKey(moveRight))
            {
                rb.velocity += new Vector2(speed, 0);
            }
            else
            {
                rb.velocity += new Vector2(0, 0);
            }
            lockSpeed();
            dazedTimeCounter = 0;
        }
        else
        {
            dazedTimeCounter -= Time.deltaTime;
        }
    }
    private void jumpPlayer()
    {
        if (transform.position.y <= -4.5f)
        {
            isGrounded = true;
        }
        if (Input.GetKey(playerJump) && isGrounded)
        {
            rb.velocity += new Vector2(0, 1f);
            Invoke("disableJump", 0.2f);
        }
    }

    private void disableJump()
    {
        isGrounded = false;
    }

    private void lockSpeed()
    {
        if (rb.velocity.x > 12f)
        {
            rb.velocity = new Vector2(12f, rb.velocity.y);
        }else if (rb.velocity.x < -12f)
        {
            rb.velocity = new Vector2(-12f, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pin")
        {
            Destroy(collision.gameObject);
            if (isProtected)
            {
                isProtected = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                gameManager.Instance.updateGameState(GameState.GameOver);
            }
        }
        else if (collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
            rb.velocity = new Vector2(0, 0);
            dazedTimeCounter = dazedTime;
        }
        else if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            gameManager.Instance.increasePoints();
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Destroy(collision.gameObject);
            isProtected = true;
            GetComponent<SpriteRenderer>().color = Color.green;
            
        }else if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
}
