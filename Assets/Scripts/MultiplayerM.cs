using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using System;
using UnityEngine.SceneManagement;
public class MultiplayerM : MonoBehaviourPunCallbacks
{
   
    [SerializeField]
    const byte MaxPlayersPerRoom = 2;
     public PhotonView player1;
     public PhotonView player2;
   // [SerializeField] Transform playerpre1;
   // [SerializeField] Transform playerpre2;
    public PhotonView puck;
   // public PhotonView puck1;
    public Transform[] spawnPoints;
    // public float timeRemaining;
    float MaxConnectionTime = 10;
    float _connnectionTime;
    Transform _activeBall;
   // public GameObject loadingScene;
    public GameObject fakeWaitingScene;

    [SerializeField] Transform _onlinePlayerPrefab;
    Transform _onlinePlayer;
    Transform _onlinePlayer1;
    public GameManager _gm;
    const string GameVersion = "1";

    bool joined;
    bool owner;

    void Awake()
    {
        //SetupPhoton();
       
    }
    void Start()
    {

      PhotonNetwork.JoinRandomRoom();

       
    }
   
    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            MaxConnectionTime -= Time.deltaTime;

           
            
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            if (MaxConnectionTime <= 0)
            {
                _gm.onleave();
                // SceneManager.LoadScene(1);

            }
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            fakeWaitingScene.SetActive(false);
        }
       
   }
    void SetupPhoton()
    {
      
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 20;
       

    }
    //void Connect()
    //{
    //    if (PhotonNetwork.IsConnected)
    //    {
    //        PhotonNetwork.JoinRandomRoom();
    //    }
    //    else
    //    {
    //        PhotonNetwork.ConnectUsingSettings();
    //    }
    //}
    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinRandomRoom();
    //}
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        CreateRoom();
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayersPerRoom;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            _onlinePlayer1 = PhotonNetwork.Instantiate(player1.name, spawnPoints[0].position, Quaternion.identity, 0).transform;
            //SetrupBall();
            //photonView.RPC("SetrupBall", RpcTarget.All);
            PhotonNetwork.Instantiate(puck.name, new Vector3(0, 0, 0), Quaternion.identity, 0);                          
            //SetrupBall();
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            onGamesessionstart();
           
            // PhotonNetwork.Instantiate(puck1.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        }
        // StartCoroutine(WaitingForSecondPlayer());

    }
    IEnumerator WaitingForSecondPlayer()
    {
        _connnectionTime = 0;
        
        
        
        while (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            _connnectionTime += Time.deltaTime;

            if (_connnectionTime > MaxConnectionTime)
            {
                PhotonNetwork.LeaveRoom();               
                SceneManager.LoadScene(1);
                _gm.onleave();
            }
            yield return null;
        }

        StartSession();
    }
    void StartSession()
    {
        StopAllCoroutines();
        Invoke("onGamesessionstart",0.1f);
        fakeWaitingScene.SetActive(false);
           
    
    }
   void onGamesessionstart()
    {
        
      
            _onlinePlayer = PhotonNetwork.Instantiate(player2.name, spawnPoints[1].position, Quaternion.identity, 0).transform;
             //photonView.RPC("SetrupBall", RpcTarget.All);
             //SetrupBall();
        //_onlinePlayer1 = PhotonNetwork.Instantiate(playerpre2.name, spawnPoints[1].position, Quaternion.identity, 0).transform;



    }

   
    //void SetrupBall()
    //{
        
    //        _activeBall =
    //                    PhotonNetwork.Instantiate(puck.name, new Vector3(0, 0, 0), Quaternion.identity, 0)
    //                        .transform;

    //    photonView.RPC("Init", RpcTarget.All, _activeBall.gameObject.GetPhotonView().ViewID);
    
        
    //}

    /// <summary>
    /// Sends _activeBall id to other players
    /// </summary>
    /// <param name="activeBallViewId">_activeBall's id</param>
    [PunRPC]
    void Init(int activeBallViewId)
    {
        _activeBall = PhotonView.Find(activeBallViewId).transform;
    }
}











