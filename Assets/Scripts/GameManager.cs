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
    public Text GameOverTitle;
    public Text scoreText;
    public GameObject[] itemlist;

    public GameObject[] walls;

    public GameObject boss;

    private string score;

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
                    foreach(GameObject wall in walls)
                    {
                        wall.SetActive(true);
                    }
                    // wake up guardian
                    Boss bossEnemy = boss.GetComponent<Boss>();
                    bossEnemy.TriggerAwaken();
                }
                timer -= Time.deltaTime;
            } else
            {
                timer += Time.deltaTime;
            }
            UpdateTimer();
        }
        
    }

    public void DropItem(Vector3 pos)
    {
        if(Random.Range(0, 100) >= 75)
        {
            int drop = Random.Range(0, itemlist.Length);
            Instantiate(itemlist[drop], pos, Quaternion.identity);
        }
    }

    private void UpdateTimer()
    {
        timerUI.text = ((int)timer) + "";
    }

    public void GameOver(string condition)
    {
        isPlaying = false;
        // show game over menu + score if relevant
        score = ((int)timer) + "";
        

        if(condition == "player_dead" && bossPhase)
        {
            GameOverTitle.text = "YOU DIED";
            scoreText.text = "You lasted " + score + " seconds against the boss...";
        } else if (condition == "player_dead" && !bossPhase)
        {
            GameOverTitle.text = "YOU DIED";
            scoreText.text = "You were dead " + score + " seconds before the boss awakened...";
        } else if (condition == "victory")
        {
            GameOverTitle.text = "BOSS DEFEATED";
            scoreText.text = "You defeated the boss in " + score + " seconds!";
        }
        
        GameOverMenu.SetActive(true);
        MenuUI.SetActive(true);
    }
}
