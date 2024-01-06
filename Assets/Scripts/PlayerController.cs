using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public List<PlayerMovement> Players = new List<PlayerMovement>();
   
    public GameObject Aiplayer;
    public GameManager gamemanager;
    private Vector3 touchPosition;
    // Update is called once per frame
    Vector3 direction;


    private void Start()
    {
       
    }
    void Update()
    {

        if (!GameObject.Find("MultiplayerManager"))
        {
            if (gamemanager.isAi)
            {
                Aiplayer.GetComponent<PlayerMovement>().enabled = false;
                Aiplayer.GetComponent<AiScript>().enabled = true;
            }
            if (!gamemanager.isAi)
            {
                Aiplayer.GetComponent<PlayerMovement>().enabled = true;
                Aiplayer.GetComponent<AiScript>().enabled = false;
            }
        }
   
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            foreach (var player in Players)
            {
                if (player.LockedFingerID == null)
                {
                    if (GameObject.Find("PlayeriBlue").GetComponent<AiScript>().enabled == false)
                    {
                        if (Input.GetTouch(i).phase == TouchPhase.Began && player.PlayerCollider.OverlapPoint(touchWorldPos))
                    // player.PlayerCollider.OverlapPoint(touchWorldPos))



                    {
                                player.LockedFingerID = Input.GetTouch(i).fingerId;


                            }
                    }
                    else if (GameObject.Find("PlayeriBlue").GetComponent<AiScript>().enabled == true)
                    {
                      if (Input.GetTouch(i).phase == TouchPhase.Began || player.PlayerCollider.OverlapPoint(touchWorldPos))
                        // player.PlayerCollider.OverlapPoint(touchWorldPos))



                        {
                            player.LockedFingerID = Input.GetTouch(i).fingerId;


                        }
                    }


             


                }
                else if (player.LockedFingerID == Input.GetTouch(i).fingerId)
                {
                    player.MoveToPosition(touchWorldPos);
                    if (Input.GetTouch(i).phase == TouchPhase.Ended ||
                        Input.GetTouch(i).phase == TouchPhase.Canceled)
                        player.LockedFingerID = null;
                        player.rb.velocity = Vector2.zero;
                }
            }

        }
    }





}
