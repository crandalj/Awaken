using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Stats stats;
    private float proximity = 1f;
    private float xdir;
    private float ydir;
    private float xSpeed;
    private float ySpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isAwake;
    private bool isAttacking;

    private float attackRate;
    private float attackDist;

    private Vector3 playerpos;

    // Start is called before the first frame update
    void Start()
    {
        stats = new Stats(3, 25, 3);
        xSpeed = stats.SPEED;
        ySpeed = stats.SPEED;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAttacking = false;
        attackDist = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAwake == true)
        {
            playerpos = Player.player.gameObject.transform.position;
            Attack();
            if (!isAttacking)
            {
                Move();
            }
        }
    }

    public void Awaken()
    {
        isAwake = true;
    }


    public void TriggerAwaken()
    {
        anim.SetTrigger("Awaken");
    }

    public void AttackFinished()
    {
        isAttacking = false;
    }

    void Move()
    {
        if (Mathf.Abs(transform.position.x - playerpos.x) > proximity)
        {
            if (transform.position.x < playerpos.x)
            {
                xdir = 1;
            }
            else
            {
                xdir = -1;
            }
        }
        else
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
        if (collision.gameObject.tag == "Weapon" && isAwake)
        {
            Debug.Log("enemy hit");

            TakeDamage((int)Player.player.GetWeaponDamage());

            Debug.Log(stats.HEALTH);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.HEALTH -= damage;
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (stats.HEALTH <= 0)
        {
            Dead();
        }
    }

    void Attack()
    {
        if(!isAttacking && Vector3.Distance(playerpos, transform.position) <= attackDist)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
    }

    void Dead()
    {
        GameManager.gm.GameOver("victory");
        Destroy(gameObject);
    }
}
