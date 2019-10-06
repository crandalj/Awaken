using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = new Stats(1, 2, 3);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        // TODO: check for item drop
        Destroy(gameObject);
    }
}
