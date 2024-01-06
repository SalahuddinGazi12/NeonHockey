using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class testScript : MonoBehaviour
{
    //public PhotonView _ph;
    private ScoreScript _sc;

    private void Start()
    {
        _sc = GameObject.Find("GameManager").GetComponent<ScoreScript>();
    }

    private void Update()
    {
        if(_sc.aiScore ==7)
        {
            _sc.GameOver_lose.SetActive(true);
        }
        else if(_sc.playerScore ==7)
        {
            _sc.GameOver_win.SetActive(true);
        }
    }
}
