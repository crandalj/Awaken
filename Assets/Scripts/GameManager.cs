using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public float timer;
    public Text timerUI;
    public bool isPlaying;
    public bool bossPhase;
    public Transform spawn;

    public GameObject MenuUI;
    public GameObject StartMenu;
    public GameObject GameOverMenu;

    public Item[] itemlist;

    public void StartButton()
    {
        MenuUI.SetActive(false);
        StartMenu.SetActive(false);
        isPlaying = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        isPlaying = false;
        bossPhase = false;
        timer = 60;
        MenuUI.SetActive(true);
        StartMenu.SetActive(true);
        GameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (!bossPhase)
            {
                if (timer < 5 && timer > 0)
                {
                    // make player start to glow
                }
                else if (timer <= 0)
                {
                    // enable bossphase
                    bossPhase = true;
                    // teleport player back to spawn
                    Player.player.gameObject.transform.position = spawn.position;
                    // lock the exits

                    // wake up guardian

                    // start boss time
                }
                timer -= Time.deltaTime;
            } else
            {
                timer += Time.deltaTime;
            }
            UpdateTimer();
        }
        
    }

    private void UpdateTimer()
    {
        timerUI.text = ((int)timer) + "";
    }

    public void GameOver()
    {
        // show game over menu + score if relevant
        GameOverMenu.SetActive(true);
        MenuUI.SetActive(true);
    }
}
