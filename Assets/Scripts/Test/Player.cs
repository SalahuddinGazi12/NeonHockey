using UnityEngine;
using Photon.Pun;
using System.Collections;
public class Player : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    PhotonView ph;
    // public int playerindex;
    int collisionstate;
 
    void Start()
    {
        //PhotonNetwork.SendRate = 60;
        //PhotonNetwork.SerializationRate = 30;
        //get the Rigidbody2D component of the player
        rb = GetComponent<Rigidbody2D>();
        ph = GetComponent<PhotonView>();
       
    }

    private void Update()
    {
        //if(collisionstate == 1)
        //{
        //    StartCoroutine(waitC());
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Puck" )
        {
            //get the direction and magnitude of the collision



            Vector2 collisionDirection = (collision.gameObject.transform.position - transform.position).normalized;
            Debug.Log(collisionDirection);
            // Do something with the collision direction
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
            PhotonView pView = collision.gameObject.GetComponent<PhotonView>();
            if (pView != null && pView.IsMine)
            {
                pView.RPC("SyncPuck", RpcTarget.AllBuffered, collision.gameObject.transform.position, collision.gameObject.GetComponent<Rigidbody2D>().velocity);
            }
            // collisionstate = 1;
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Puck"  && collision.gameObject.GetComponent<PhotonView>().IsMine)
    //    { 
    //        Vector2 collisionDirection = (collision.gameObject.transform.position - transform.position).normalized;
    //    Debug.Log(collisionDirection);
    //    // Do something with the collision direction
    //    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
    //    PhotonView pView = collision.gameObject.GetComponent<PhotonView>();
    //    if (pView != null && pView.IsMine)
    //    {
    //        pView.RPC("SyncPuck", RpcTarget.AllBuffered, collision.gameObject.transform.position, collision.gameObject.GetComponent<Rigidbody2D>().velocity);
    //    }
    //}
    //}

    [PunRPC]
    private void SyncPuck(Vector2 position, Vector2 velocity)
    {
        transform.position = position;
        rb.velocity = velocity;
    }
    //IEnumerator waitC()
    //{
    //    gameObject.GetComponent<Collider2D>().isTrigger = false;
    //    yield return new WaitForSeconds(0.1f);
    //    gameObject.GetComponent<Collider2D>().isTrigger = true;
    //   // collisionstate = 2;

    //}




}
