using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviourPunCallbacks
{

   public Rigidbody2D rb;
    Vector2 startingPosition;
    bool wasJustClicked = true;
    bool canMove;
    Vector2 playerSize;
    public Transform BoundaryHolder;

    Boundary playerBoundary;

    public Collider2D PlayerCollider { get; private set; }

    PlayerController Controller;
   // PlayerControllerRed Controller1;
    CameraSetup camerasetup;
    public int? LockedFingerID { get; set; }
    PhotonView ph;

   
    // Use this for initialization

    private void Awake()
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            ph = GetComponent<PhotonView>();
          //  Controller1 = GameObject.Find("GameManager").GetComponent<PlayerControllerRed>();
        }
      
            Controller = GameObject.Find("GameManager").GetComponent<PlayerController>();
        camerasetup = GameObject.Find("GameManager").GetComponent<CameraSetup>();


    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
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

        // 
        //if (GameObject.Find("MultiplayerManager"))
        //{
        //    if (ph.IsMine)
        //{
        //    Controller = GameObject.Find("GameManager").GetComponent<PlayerController>();
        //    Controller.Players.Add(this);
        //}
        //    }
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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Puck")
    //    {
    //        //get the direction and magnitude of the collision



    //        Debug.Log("hit");

    //        Vector2 collisionDirection = collision.gameObject.transform.position - transform.position;
    //        Debug.Log(collisionDirection);
    //        // Do something with the collision direction
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
            
           
           
                
          
    //        //if this is the client that owns the player, send updates about the player's position, velocity and force to all other clients

    //    }
    //}

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnEnable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            if (ph.IsMine)
            {
               // Controller = GameObject.Find("GameManager").GetComponent<PlayerController>();
             //   Controller1.Players1.Add(this);
            }

        }
        else
        {
            Controller.Players.Add(this);
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
               // Controller1.Players1.Remove(this);
            }

        }
        else
        {
            Controller.Players.Remove(this);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Puck" )
    //    {
    //        //get the direction and magnitude of the collision



    //        Vector2 collisionDirection = (collision.gameObject.transform.position - transform.position).normalized;
    //        Debug.Log(collisionDirection);
    //        // Do something with the collision direction
    //        //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
    //        //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    //    }
    //    //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //}
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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Puck" )
    //    {
    //        //get the direction and magnitude of the collision



    //        Vector2 collisionDirection = (collision.gameObject.transform.position - transform.position).normalized;
    //        Debug.Log(collisionDirection);
    //        // Do something with the collision direction
    //        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
            

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