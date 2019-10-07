using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string NAME;
    public bool IS_WEAPON;
    public Sprite SPRITE;
    public float DAMAGE;
    public int EFFECT;

    public Sprite slotDefault;

    public void Reset()
    {
        this.NAME = "Fist";
        this.IS_WEAPON = true;
        this.SPRITE = slotDefault;
        this.DAMAGE = 0;
        this.EFFECT = 0;
    }
}
