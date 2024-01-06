using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    PuckScript puck;
    public GameObject pausepanel;
    public bool isAi;
    public TextMeshProUGUI rewardshow;
    CameraSetup camerasetup;
    private void Awake()
    {
        if (!GameObject.Find("MultiplayerManager"))
        {
            puck = GameObject.Find("Puck").GetComponent<PuckScript>();
        }

    }
    void Start()
    {
        isAi = false;
        Time.timeScale = 1;
        if (GameObject.Find("MultiplayerManager"))
        {
            camerasetup = GameObject.Find("GameManager").GetComponent<CameraSetup>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("isAifalse") == 1)
        {
            isAi = false;
        }
        if (PlayerPrefs.GetInt("isAi") == 1)
        {
            isAi = true;
           
        }
        if (!GameObject.Find("MultiplayerManager"))
        {
            if (PlayerPrefs.GetInt("easy") == 1)
            {

                AiScript.instance.MaxMovementSpeed = 6;
                puck.MaxSpeed = 13;
                // isAi = true;
            }
            if (PlayerPrefs.GetInt("normal") == 1)
            {
                AiScript.instance.MaxMovementSpeed = 13;
                puck.MaxSpeed = 15;
                // isAi = true;
            }
            if (PlayerPrefs.GetInt("Hard") == 1)
            {
                AiScript.instance.MaxMovementSpeed = 15;
                puck.MaxSpeed = 18;
                // isAi = true;
            }
        }



        if (GameObject.Find("MultiplayerManager"))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                if (GameObject.Find("GameOver_Win"))
                {
                    camerasetup.setuppanel1();
                }
                else if (GameObject.Find("GameOver_lose"))
                {
                    camerasetup.setuppanel2();
                }
            }
        }
    }
    //public void aiplayer()
    //{
    //    isAi = true;
    //    playpanel.SetActive(false);
    //}
    //public void v1player()
    //{
    //    isAi = false;
    //    playpanel.SetActive(false);
    //}
   
    public void pauseOrResume(int i)
    {
        if (i == 1)
        {
            AudioManager.instance.Play("Button");
            pausepanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(i == 2)
        {
            AudioManager.instance.Play("Button");
            pausepanel.SetActive(false);
            Time.timeScale = 1;
        }

    }
    public void restart()
    {
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(2);

    }
    public void HOmeBtn()
    {
       // AudioManager.instance.Play("Button");
        PlayerPrefs.SetInt("easy", 0);
        PlayerPrefs.SetInt("normal", 0);
        PlayerPrefs.SetInt("Hard", 0);
        if (GameObject.Find("MultiplayerManager"))
        {
            onleave();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    
    }

    public void onleave()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
            //PhotonNetwork.Disconnect();
            SceneManager.LoadScene(1);
            
            Debug.Log("onleave");
        }
    }

    public override void OnLeftRoom()
    {
        //SceneManager.LoadScene(1);
        PhotonNetwork.LoadLevel(1);
        Debug.Log("onleavesuccess");
    }

}
