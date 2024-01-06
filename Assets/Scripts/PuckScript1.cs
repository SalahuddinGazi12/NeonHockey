using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
public class PuckScript1: MonoBehaviourPunCallbacks
{
   
     ScoreScript ScoreScriptInstance;
    GameManager gm;
    public static bool WasGoal { get; private set; }
    public float MaxSpeed;
    private Rigidbody2D rb;
    PhotonView ph;
     public GameObject[] particeeffect;
     public ParticleSystem[] ps;
    Vector3 latestPos;
    Quaternion latestRot;
    Vector3 velocity;
    Vector3 angularVelocity;
    protected float _ballSpeed;
    float minSpeed = 4;
    float maxSpeed = 7;
    bool valuesReceived = false;
   
    //[SerializeField] PhotonView _lowerPlayer, _upperPlayer;
    Vector3 lastvelocity;

    private void Awake()
    {
        
            ScoreScriptInstance = GameObject.Find("GameManager").GetComponent<ScoreScript>();
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
      
            ph = GetComponent<PhotonView>();
            WasGoal = false;

   
    }
    private void Update()
    {
       
          
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
           

            if (other.tag == "AiGoal")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    photonView.RPC("PlayerincreaseScore", RpcTarget.All);
                }
                
                   
                   WasGoal = true;
               Instantiate(particeeffect[4], transform.position, Quaternion.identity);
                AudioManager.instance.Play("Goal");
                ps[4].Play();
                // audioManager.PlayGoal();
                StartCoroutine(ScoreScriptInstance.goalstate());

                if (ScoreScriptInstance.playerScore == 7)
                {
                    if (GameObject.Find("MultiplayerManager"))
                    {
                        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 100);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("win_num", PlayerPrefs.GetInt("win_num") + 1);
                        if (PlayerPrefs.GetInt("easy") == 1)
                        {
                            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 20);
                          //  AdsManager.instance.Showinterstitial();
                           // gm.rewardshow.text = 20 + "+ " + "Coins";
                        }
                        if (PlayerPrefs.GetInt("normal") == 1)
                        {
                            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 30);
                           // AdsManager.instance.Showinterstitial();
                            //  gm.rewardshow.text = 30 + "+ " + "Coins";
                        }
                        if (PlayerPrefs.GetInt("Hard") == 1)
                        {
                            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 50);
                           // AdsManager.instance.Showinterstitial();
                            //  gm.rewardshow.text = 50 + "+ " + "Coins";
                        }
                        if (PlayerPrefs.GetInt("win_num") > PlayerPrefs.GetInt("lose_num"))
                        {
                            ScoreScriptInstance.winperctanges = ((PlayerPrefs.GetInt("win_num") / PlayerPrefs.GetInt("lose_num")) / 100);
                        }
                        else
                        {
                            ScoreScriptInstance.winperctanges = 0;
                        }
                    }

                    
                }

               // photonView.RPC("resetpk", RpcTarget.Others, false);
                resetpk(false);
            }
            else if (other.tag == "PlayerGoal")
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    photonView.RPC("AIincreaseScore", RpcTarget.All);
                }
                
                WasGoal = true;
                AudioManager.instance.Play("Goal");
                Instantiate(particeeffect[4], transform.position, Quaternion.identity);
                ps[4].Play();
                // audioManager.PlayGoal();
                StartCoroutine(ScoreScriptInstance.goalstate());
                if (ScoreScriptInstance.aiScore == 7)
                {
                   
                  //  AdsManager.instance.Showinterstitial();

                    if (GameObject.Find("MultiplayerManager"))
                    {
                        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 100);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("lose_num", PlayerPrefs.GetInt("lose_num") + 1);
                        if (PlayerPrefs.GetInt("win_num") > PlayerPrefs.GetInt("lose_num"))
                        {
                            ScoreScriptInstance.winperctanges = (PlayerPrefs.GetInt("win_num") / PlayerPrefs.GetInt("lose_num")) / 100;
                        }
                        else
                        {
                            ScoreScriptInstance.winperctanges = 0;
                        }
                    }
                }
               // photonView.RPC("resetpk", RpcTarget.Others,true);
                resetpk(true);
            }


           
        }


        if (other.gameObject.tag == "playerred")
        {
            Debug.Log("tr");
            Instantiate(particeeffect[0], transform.position, Quaternion.identity);
            ps[0].Play();
        }
        if (other.gameObject.tag == "playerblue")
        {
            Debug.Log("tr");
            Instantiate(particeeffect[1], transform.position, Quaternion.identity);
            ps[1].Play();
        }
        if (other.gameObject.tag == "boundaryred")
        {
            Debug.Log("tr");
            Instantiate(particeeffect[2], transform.position, Quaternion.identity);
            ps[2].Play();
        }
        if (other.gameObject.tag == "boundaryblue")
        {
            Debug.Log("tr");
            Instantiate(particeeffect[3], transform.position, Quaternion.identity);
            ps[3].Play();
        }




    }
   [PunRPC]
    public void resetpk(bool did)
    {
        if (did)
        {
            StartCoroutine(ResetPuck(true));
           //testfun();
        }
        else
        {
            StartCoroutine(ResetPuck(false));
           // testfun();
        }
    }

    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0, 0);

        if (didAiScore)
            rb.position = new Vector2(0, -1);
        else
            rb.position = new Vector2(0, 1);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (ph.IsMine)
        {
            if (rb.velocity.magnitude > MaxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
            }
        }
        


    }
    //[PunRPC]
    //private void UpdateObjectPhysics(Vector3 velocity, float angularVelocity)
    //{
    //    // receive the object's velocity and angular velocity from the master client
    //    if (rb.velocity.magnitude > MaxSpeed)
    //    {
    //        rb.velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
    //    }
    //    rb.angularVelocity = angularVelocity;
    //}


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "playerblue")
    //    {
    //        Debug.Log("bluetrye");
    //        _lowerPlayer.TransferOwnership(PhotonNetwork.LocalPlayer);


    //    }
    //    if (collision.gameObject.tag == "playerred")
    //    {
    //        Debug.Log("bluetrye");
    //        _upperPlayer.TransferOwnership(PhotonNetwork.LocalPlayer);


    //    }
    //}

    


    [PunRPC]
    public void PlayerincreaseScore()
    {
        ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore); 
    }

    [PunRPC]
    public void AIincreaseScore()
    {
        ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
       
    }

    //[System.Obsolete]
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(rb.position);      
    //        stream.SendNext(rb.velocity);
    //        stream.SendNext(MaxSpeed);
    //        //   stream.SendNext(rb.sharedMaterial);
    //    }
    //    else
    //    {
    //        rb.position = (Vector3)stream.ReceiveNext();          
    //        rb.velocity = (Vector3)stream.ReceiveNext();
    //        MaxSpeed = (float)stream.ReceiveNext();

    //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
    //        rb.position += rb.velocity * lag;
    //    }
    //}
}
