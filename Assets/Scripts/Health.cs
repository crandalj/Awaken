using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;

    private Player player;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    public void UpdateHealth(int value)
    {
        player.stats.HEALTH += value;

        if (player.stats.HEALTH > player.stats.MAX_HEALTH)
        {
            player.stats.HEALTH = player.stats.MAX_HEALTH;
        }

        healthBar.fillAmount = player.stats.HEALTH / player.stats.MAX_HEALTH;

        DeathCheck();
    }

    private void DeathCheck()
    {
        if(player.stats.HEALTH <= 0)
        {
            //Trigger gameover
        }
    }
}
