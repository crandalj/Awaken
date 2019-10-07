using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player player;

    private Health health;
    public Stats stats;
    private Rigidbody2D rb;
    private Animator anim;

    private string facing;
    private float moveX;
    private float moveY;
    private bool isInvuln;
    private float invulnCount;
    private float invulnTime;

    public Item[] inventory;
    private int slotSelected;
    public Image[] slot;
    public Image[] slotFrame;
    public GameObject itemHeld;
    private SpriteRenderer heldSR;
    public GameObject fist;
    private bool canUseItem;

    private Color selectedColor = Color.red;
    private Color unselectedColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        heldSR = itemHeld.GetComponent<SpriteRenderer>();
        stats = new Stats(1,10,5);
        health = gameObject.GetComponent<Health>();
        facing = "right";
        moveX = 0;
        moveY = 0;
        isInvuln = false;
        invulnCount = 0;
        invulnTime = 1;
        slotSelected = 0;
        inventory[0].Reset();
        inventory[1].Reset();
        inventory[2].Reset();
        UpdateSlotSelected(slotSelected);
        canUseItem = true;
    }

    // Update is called once per frame
    void Update()
    {
        InvulnCheck();
        GetInput();
        Move();
        AnimUpdate();
    }

    private void UpdateSlotSelected(int slotnum)
    {
        slotFrame[slotSelected].color = unselectedColor;
        slotSelected = slotnum;
        slotFrame[slotSelected].color = selectedColor;
        UpdateItemHeldSprite();

        Debug.Log(inventory[slotSelected].IS_WEAPON);
        Debug.Log(inventory[slotSelected].SPRITE);
        Debug.Log(inventory[slotSelected].DAMAGE);
        Debug.Log(inventory[slotSelected].EFFECT);
    }

    private void UpdateItemSlot()
    {
        // Update UI sprite
        slot[slotSelected].sprite = inventory[slotSelected].SPRITE;
    }

    private void UpdateItemHeldSprite()
    {
        // Update player weapon sprite
        if (slotSelected < inventory.Length && inventory[slotSelected].NAME != "Fist" && inventory[slotSelected].IS_WEAPON)
        {
            Debug.Log(inventory[slotSelected]);
            heldSR.sprite = inventory[slotSelected].SPRITE;
            fist.SetActive(false);
        } else
        {
            Debug.Log("Item is not a weapon");
            heldSR.sprite = null;
            fist.SetActive(true);
        }
    }

    private void PickupItem(Item item)
    {
        inventory[slotSelected].NAME = item.NAME;
        inventory[slotSelected].IS_WEAPON = item.IS_WEAPON;
        inventory[slotSelected].SPRITE = item.SPRITE;
        inventory[slotSelected].DAMAGE = item.DAMAGE;
        inventory[slotSelected].EFFECT = item.EFFECT;

        UpdateItemSlot();
        UpdateItemHeldSprite();
    }

    private void InvulnCheck()
    {
        if(isInvuln && (Time.time > invulnCount))
        {
            isInvuln = false;
        }
    }

    private void AnimUpdate()
    {
        anim.SetFloat("velocity", moveX);
    }

    public void ActionFinished()
    {
        canUseItem = true;
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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            UpdateSlotSelected(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            UpdateSlotSelected(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            UpdateSlotSelected(2);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        if (canUseItem)
        {
            if (slotSelected < inventory.Length && inventory[slotSelected].NAME != "Fist" && !inventory[slotSelected].IS_WEAPON)
            {
                
                if(inventory[slotSelected].EFFECT == 1)
                {
                    // Health potion
                    health.UpdateHealth(10);   
                }

                inventory[slotSelected].Reset();
                UpdateItemSlot();
            }
            else
            {
                // attack
                anim.SetTrigger("attack");
                canUseItem = false;
            }
            
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isInvuln)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                Debug.Log(stats.HEALTH);
                health.UpdateHealth(-enemy.stats.ATTACK);
                isInvuln = true;
                invulnCount = Time.time + invulnTime;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.X))
        {

            if (collision.gameObject.tag == "Item")
            {
                Item item = collision.gameObject.GetComponent<Item>();
                PickupItem(item);
                Destroy(collision.gameObject);
            }
        }
    }
}
