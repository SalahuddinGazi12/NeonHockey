
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRed : MonoBehaviour
{

    public List<PlayerMovementRed> Players1 = new List<PlayerMovementRed>();

    
    private Vector3 touchPosition;
    // Update is called once per frame
    Vector3 direction;
   // public GameObject loadingbar;

    private void Start()
    {

    }
    void Update()
    {

        mobiletouch();
    }

    public void mobiletouch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            foreach (var player in Players1)
            {
                if (player.LockedFingerID == null)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began || player.PlayerCollider.OverlapPoint(touchWorldPos))
                    // player.PlayerCollider.OverlapPoint(touchWorldPos))
                    {
                        player.LockedFingerID = Input.GetTouch(i).fingerId;


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
