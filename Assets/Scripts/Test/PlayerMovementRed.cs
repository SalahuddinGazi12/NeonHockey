
using UnityEngine;
using Photon.Pun;
using Photon;
public class PlayerMovementRed : MonoBehaviourPunCallbacks
{

    public Rigidbody2D rb;
    Vector2 startingPosition;
    bool wasJustClicked = true;
    bool canMove;
    Vector2 playerSize;
    public Transform BoundaryHolder;

    Boundary playerBoundary;

    public Collider2D PlayerCollider { get; private set; }

    PlayerControllerRed Controller1;
    CameraSetup camerasetup;
  
    public int? LockedFingerID { get; set; }
    PhotonView ph;


    // Use this for initialization

    private void Awake()
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            ph = GetComponent<PhotonView>();
        }

        Controller1 = GameObject.Find("GameManager").GetComponent<PlayerControllerRed>();
        camerasetup = GameObject.Find("GameManager").GetComponent<CameraSetup>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {



     
          
        





        startingPosition = rb.position;
        PlayerCollider = GetComponent<Collider2D>();

        if (GameObject.Find("PlayeriBlue(Clone)"))
        {
            BoundaryHolder = GameObject.Find("PlayerBlueBoundaryHolder").transform;

        }
        else
        if (GameObject.Find("PlayerRed(Clone)"))
        {
            BoundaryHolder = GameObject.Find("PlayerRedBoundaryHolder").transform;

        }


        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);


        if (GameObject.Find("MultiplayerManager"))
        {

            if (!PhotonNetwork.IsMasterClient)
            {
                camerasetup.SetupCamera();

            }
        }

    }

    private void Update()
    {
       
    }


#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnEnable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            if (ph.IsMine)
            {
                // Controller = GameObject.Find("GameManager").GetComponent<PlayerController>();
                Controller1.Players1.Add(this);
            }

        }
        else
        {
           // Controller1.Players1.Add(this);
        }

    }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnDisable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            if (ph.IsMine)
            {
                // Controller = GameObject.Find("GameManager").GetComponent<PlayerController>();
                 Controller1.Players1.Remove(this);
            }

        }
    }

    public void MoveToPosition(Vector2 position)
    {
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(position.x, playerBoundary.Left,
                                                  playerBoundary.Right),
                                      Mathf.Clamp(position.y, playerBoundary.Down,
                                                  playerBoundary.Up));
        rb.MovePosition(clampedMousePos);
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (photonView.IsMine)
    //    {
    //        Transform collisionObjectRoot = col.transform.root;
    //        if (collisionObjectRoot.CompareTag("Player"))
    //        {
    //            //Transfer PhotonView of Rigidbody to our local player
    //         photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    //        }
    //    }
    //}

    public void pcmode()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (wasJustClicked)
            {
                wasJustClicked = false;

                if ((mousePos.x >= transform.position.x && mousePos.x < transform.position.x + playerSize.x ||
                mousePos.x <= transform.position.x && mousePos.x > transform.position.x - playerSize.x) &&
                (mousePos.y >= transform.position.y && mousePos.y < transform.position.y + playerSize.y ||
                mousePos.y <= transform.position.y && mousePos.y > transform.position.y - playerSize.y))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }

            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                  playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                  playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }

   
}