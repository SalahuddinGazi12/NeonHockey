using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ScoreScript : MonoBehaviour
{
    public GameObject winlostimage; 
    public GameObject gameWon;
    public GameObject gamelose;
    public GameObject GameOver_win;
    public GameObject GameOver_lose;
    public GameObject GameOver2panel;
    public GameObject image_goal;
    public TextMeshProUGUI win_num;
    public TextMeshProUGUI Lose_num;
    public TextMeshProUGUI Win_Ratio;
    public float winperctanges;
    GameManager gm;
    public GameObject coin_image;
    

    public enum Score
    {
        AiScore, PlayerScore
    }
    public TextMeshProUGUI AiScoreTxt;
    public TextMeshProUGUI PlayerScoreTxt;
    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public int aiScore, playerScore;
    private void Start()
    {
        PlayerPrefs.SetInt("win_num", PlayerPrefs.GetInt("win_num", 0));
        PlayerPrefs.SetInt("lose_num", PlayerPrefs.GetInt("lose_num", 0));
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0));
        // win_num.text = "" + PlayerPrefs.GetInt("win_num").ToString();
        //  Lose_num.text = "" + PlayerPrefs.GetInt("lose_num").ToString();
        winperctanges = 0;
    }

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            AiScoreTxt.text = (++aiScore).ToString();
        else
            PlayerScoreTxt.text = (++playerScore).ToString();
    }
    private void Update()
    {
        if (!GameObject.Find("MultiplayerManager"))
        {
            if (GameObject.Find("PlayeriBlue").GetComponent<AiScript>().enabled == true)
            {
                if (aiScore == 7)
                {
                    
                    StartCoroutine(timestate());
                 
                    StartCoroutine(winlostimestate());
                    gamelose.SetActive(true);
                    gameWon.SetActive(false);
                    coin_image.SetActive(false);

                }
               else if (playerScore == 7)
                {
                    
                    StartCoroutine(timestate());
                    
                    StartCoroutine(winlostimestate());
                    if (PlayerPrefs.GetInt("easy") == 1)
                    {
                       // PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 20);
                        gm.rewardshow.text = 20 + "+ " + "Coins";
                    }
                    if (PlayerPrefs.GetInt("normal") == 1)
                    {
                       // PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 30);
                         gm.rewardshow.text = 30 + "+ " + "Coins";
                    }
                    if (PlayerPrefs.GetInt("Hard") == 1)
                    {
                       // PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 50);
                          gm.rewardshow.text = 50 + "+ " + "Coins";
                    }
                    gameWon.SetActive(true);
                    gamelose.SetActive(false);
                    coin_image.SetActive(true);
                }
                if (aiScore == 7 || playerScore == 7)
                {


                    win_num.text = "" + PlayerPrefs.GetInt("win_num");
                    Lose_num.text = "" + PlayerPrefs.GetInt("lose_num");
                    Win_Ratio.text = "WINNER RATE " + winperctanges + "%";
                    
                }

            }
            else if (GameObject.Find("PlayeriBlue").GetComponent<AiScript>().enabled == false)
            {
                if (aiScore == 7)
                {
                    GameOver_lose.SetActive(true);
                    StartCoroutine(timestate());
                }
                if (playerScore == 7)
                {
                    GameOver_win.SetActive(true);
                    StartCoroutine(timestate());

                }
            }
        }
        else if (GameObject.Find("MultiplayerManager"))
        {

            if (aiScore == 7)
            {
                GameOver_lose.SetActive(true);
                //gm.rewardshow.text = 100 + "+ " + "Coins";
                // GameOver2panel.transform.rotation = Quaternion.Euler(0, 0, 180);
                StartCoroutine(timestate());

            }
            if (playerScore == 7 )
            {
                GameOver_win.SetActive(true);
                //gm.rewardshow.text = 100 + "+ " + "Coins";
                StartCoroutine(timestate());

            }

        }
        // win_num.text = "" + PlayerPrefs.GetInt("win_num").ToString();
        // Lose_num.text = "" + PlayerPrefs.GetInt("lose_num").ToString();

    }

   public IEnumerator goalstate()
    {
        image_goal.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        image_goal.SetActive(false);
    }

    IEnumerator winlostimestate()
    {


        yield return new WaitForSeconds(0.6f);
        winlostimage.SetActive(true);
        

    }
    IEnumerator timestate()
    {
        
       
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0;
    }
}
