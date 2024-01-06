using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class launcherM : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    //public Button multiplayerButton;

    private void Awake()
  
    {
        if (PhotonNetwork.IsConnected)
        {
            print("Already connected");
            PhotonNetwork.JoinRandomRoom();
           // PhotonNetwork.JoinLobby();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
           // PhotonNetwork.JoinLobby();
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("success");
        //PhotonNetwork.JoinLobby();
        PhotonNetwork.JoinRandomRoom();
        //SceneManager.LoadScene(4);
        //multiplayerButton.interactable = true;
    }


}
