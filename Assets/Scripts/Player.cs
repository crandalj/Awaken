using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sr;
    private Health health;
    private Stats stats;
    private Rigidbody2D rb;
    private Animator anim;

    private string facing;
    private float moveX;
    private float moveY;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        stats = new Stats();
        health = gameObject.GetComponent<Health>();
        facing = "right";
        moveX = 0;
        moveY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveX = -stats.SPEED;
            updateFacing("left");
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveX = stats.SPEED;
            updateFacing("right");
        }
        else
        {
            moveX = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveY = -stats.SPEED;
        } else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveY = stats.SPEED;
        } else
        {
            moveY = 0;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX, moveY);
    }

    void updateFacing(string current)
    {
        if(current != facing)
        {
            facing = current;
            Vector3 newScale = new Vector3(gameObject.transform.localScale.x * -1, 1, 1);
            gameObject.transform.localScale = newScale;
        }
    }
}
