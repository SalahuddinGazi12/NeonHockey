using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Uimanager : MonoBehaviourPunCallbacks
{
    public static Uimanager instance;
    // Start is called before the first frame update
    public TextMeshProUGUI show_user;
    public TextMeshProUGUI show_coins;
    public GameObject not_EnoughcoinBox;
    public GameObject waiting_Scene;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
        

        show_user.text = PlayerPrefs.GetString("username");
        show_coins.text = ""+ PlayerPrefs.GetInt("coin").ToString();
        boxstate = 0;
    }
    //public override void OnConnectedToMaster()
    //{
    //    Debug.Log("success");
    //    //  PhotonNetwork.JoinLobby();
    //    //  PhotonNetwork.JoinRandomRoom();
    //}
    // Update is called once per frame
    void Update()
    {
        show_coins.text = "" + PlayerPrefs.GetInt("coin").ToString();
        if(boxstate == 1)
        {
            StartCoroutine(wait_notenoughBox());
        }
        else if(boxstate == 2)
        {
            StartCoroutine(waitingscene());
        }
    }
    public void cpu()
    {
        AudioManager.instance.Play("Button");
    }
    public void v1player()
    {
        AudioManager.instance.Play("Button");
        PlayerPrefs.SetInt("isAifalse",1);
        PlayerPrefs.SetInt("isAi", 0);
        //PlayerPrefs.SetInt("isAi", PlayerPrefs.GetInt("isAi") - 1);
        SceneManager.LoadScene(2);
    }
    public void aiplayer()
    {
        AudioManager.instance.Play("Button");
        PlayerPrefs.SetInt("isAi",1);
       // PlayerPrefs.SetInt("isAifalse", PlayerPrefs.GetInt("isAifalse") - 1);

    }
    public void Multiplayer()
    {
        if (PlayerPrefs.GetInt("coin") >= 25)
        {
            AudioManager.instance.Play("Button");
            //SceneManager.LoadScene(3);
            waiting_Scene.SetActive(true);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 25);
            boxstate = 2;
        }
        else
        {
            boxstate = 1;
        }
    }
    public void EasyOrNormalOrHard(int i)
    {
        if (i == 1)
        {
            AudioManager.instance.Play("Button");
            PlayerPrefs.SetInt("easy",1);
            SceneManager.LoadScene(2);
            // AiScript.instance.MaxMovementSpeed = 8; 
        }
       else if (i == 2)
        {
            AudioManager.instance.Play("Button");
            PlayerPrefs.SetInt("normal",1);
            SceneManager.LoadScene(2);
            //  AiScript.instance.MaxMovementSpeed = 10;
        }
       else if (i == 3)
        {
            AudioManager.instance.Play("Button");
            PlayerPrefs.SetInt("Hard", 1);
            SceneManager.LoadScene(2);
            //  AiScript.instance.MaxMovementSpeed = 12;
        }
    }
    public void getrewwrd()
    {
        //PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 300);
    }

    public void getrewardafterwatchvideo()
    {
        AdsManager.instance.showvideoad();
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 300);
    }
    int boxstate;

    IEnumerator waitingscene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }
       
    IEnumerator wait_notenoughBox()
    {
        not_EnoughcoinBox.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        not_EnoughcoinBox.SetActive(false);
        boxstate = 0;
    }
}
