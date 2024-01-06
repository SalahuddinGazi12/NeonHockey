using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PuckScript : MonoBehaviourPunCallbacks
{
   
     ScoreScript ScoreScriptInstance;
    GameManager gm;
    public static bool WasGoal { get; private set; }
    public float MaxSpeed;
    private Rigidbody2D rb;
    PhotonView ph;
    public GameObject[] particeeffect;
    public ParticleSystem[] ps;
    Vector3 targetVel = new Vector3();
    Vector3 refVel = Vector3.zero;
    float smoothVal = 10f; // Higher = 'Smoother' 

    Vector3 lastvelocity;
    private void Awake()
    {
        ScoreScriptInstance = GameObject.Find("GameManager").GetComponent<ScoreScript>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        if (GameObject.Find("MultiplayerManager"))
        {
            ph = GetComponent<PhotonView>();

        }
       
        WasGoal = false;
    }
    private void Update()
    {
     // lastvelocity = rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {

            //if ( other.tag == "playerred" || other.tag == "playerblue")
            //{
            //    //get the direction and magnitude of the collision



            //    Vector2 collisionDirection = (transform.position - other.gameObject.transform.position).normalized;
            //    Debug.Log(collisionDirection);
            //    // Do something with the collision direction
            //    rb.AddForce(collisionDirection * 2f, ForceMode2D.Impulse);


            //}



            if (other.tag == "AiGoal")
            {
                if (GameObject.Find("MultiplayerManager"))
                {
                    
                        photonView.RPC("PlayerincreaseScore", RpcTarget.All);
                    
                   // ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                }
                else
                {
                    ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
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
                            AdsManager.instance.Showinterstitial();
                            gm.rewardshow.text = 20 + "+ " + "Coins";
                        }
                        if (PlayerPrefs.GetInt("normal") == 1)
                        {
                            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 30);
                            AdsManager.instance.Showinterstitial();
                              gm.rewardshow.text = 30 + "+ " + "Coins";
                        }
                        if (PlayerPrefs.GetInt("Hard") == 1)
                        {
                            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 50);
                            AdsManager.instance.Showinterstitial();
                              gm.rewardshow.text = 50 + "+ " + "Coins";
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

                //StartCoroutine(ResetPuck(false));
                resetpk(false);
            }
            else if (other.tag == "PlayerGoal")
            {

                if (GameObject.Find("MultiplayerManager"))
                {
                    
                       photonView.RPC("AIincreaseScore", RpcTarget.All );
                    
                    //ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                }
                else
                {
                    ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                }
              
                WasGoal = true;
                AudioManager.instance.Play("Goal");
                Instantiate(particeeffect[4], transform.position, Quaternion.identity);
                ps[4].Play();
                //audioManager.PlayGoal();
                StartCoroutine(ScoreScriptInstance.goalstate());
                if (ScoreScriptInstance.aiScore == 7)
                {
                   
                    AdsManager.instance.Showinterstitial();

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
                // StartCoroutine(ResetPuck(true));
                resetpk(true);
            }


           
        }
       
            if (other.gameObject.tag == "Playerblue" || other.gameObject.tag == "Playerred")
            {
                //get the direction and magnitude of the collision



                Vector2 collisionDirection = (other.gameObject.transform.position - transform.position).normalized;
                Debug.Log(collisionDirection);
            // Do something with the collision direction
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);


            }

        



    }
    [PunRPC]
    public void resetpk(bool did)
    {
        if (did)
        {
            StartCoroutine(ResetPuck(true));
        }
        else
        {
            StartCoroutine(ResetPuck(false));
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

    //[PunRPC]
    //public void moveballspeed()
    //{

    //    if (rb.velocity.magnitude > MaxSpeed)
    //    {
    //        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    //    }
    //}

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        }

        //Vector2 vel = rb.velocity;

        //vel.x = Mathf.Clamp(vel.x, -MaxSpeed, MaxSpeed);
        //rb.velocity = vel;
        // rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref refVel, smoothVal);
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        //var speed = lastvelocity.magnitude;
        //var direction = Vector3.Reflect(lastvelocity.normalized, col.contacts[0].normal);
        //rb.velocity = direction * Mathf.Max(speed, 0f);




        //if (!photonView.IsMine)
        //{
        //    Transform collisionObjectRoot = col.transform.root;
        //    if (collisionObjectRoot.CompareTag("playerblue") || collisionObjectRoot.CompareTag("playerred"))
        //    {
        //        //Transfer PhotonView of Rigidbody to our local player
        //        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        //    }
        //}

        if (col.gameObject.tag == "playerred")
        {
            Debug.Log("tr");
             Instantiate(particeeffect[0], transform.position, Quaternion.identity);
             ps[0].Play();
        }
        if (col.gameObject.tag == "playerblue")
        {
            Debug.Log("tr");
               Instantiate(particeeffect[1], transform.position, Quaternion.identity);
              ps[1].Play();
        }
        if (col.gameObject.tag == "boundaryred")
        {
            Debug.Log("tr");
               Instantiate(particeeffect[2], transform.position, Quaternion.identity);
               ps[2].Play();
        }
        if (col.gameObject.tag == "boundaryblue")
        {
            Debug.Log("tr");
             Instantiate(particeeffect[3], transform.position, Quaternion.identity);
              ps[3].Play();
        }
    }

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
}
