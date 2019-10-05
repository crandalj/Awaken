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

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        stats = gameObject.GetComponent<Stats>();
        health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
