using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats stats;
    private float proximity = 0.3f;
    private float xdir;
    private float ydir;
    private float xSpeed ;
    private float ySpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        stats = new Stats(1, 2, 3);
        xSpeed = stats.SPEED;
        ySpeed = stats.SPEED;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isPlaying)
        {
            Move();
        }
        
    }

    public void TakeDamage(int damage)
    {
        stats.HEALTH -= damage;
        DeathCheck();
    }

    private void DeathCheck()
    {
        if(stats.HEALTH <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GameManager.gm.DropItem(transform.position);
        Destroy(gameObject);
    }

    void Move()
    {
        Vector3 playerpos = Player.player.gameObject.transform.position;
        if(Mathf.Abs(transform.position.x - playerpos.x) > proximity)
        {
            if(transform.position.x < playerpos.x)
            {
                xdir = 1;
            }
            else
            {
                xdir = -1;
            }
        } else
        {
            xdir = 0;
        }

        if (Mathf.Abs(transform.position.y - playerpos.y) > proximity)
        {
            if (transform.position.y < playerpos.y)
            {
                ydir = 1;
            }
            else
            {
                ydir = -1;
            }
        }
        else
        {
            ydir = 0;
        }

        Vector3 dir = new Vector3(xdir * xSpeed, ydir * ySpeed, 0);
        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("enemy hit");
            
            TakeDamage((int)Player.player.GetWeaponDamage());

            Debug.Log(stats.HEALTH);
        }
    }
}
