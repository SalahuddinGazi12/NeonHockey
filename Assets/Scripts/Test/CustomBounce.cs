using UnityEngine;
using Photon.Pun;
using System.Collections;

public class CustomBounce : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    private PhotonView photonView;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        //PhotonNetwork.SendRate = 90;
        //PhotonNetwork.SerializationRate = 60;
    }

    //private IEnumerator Wait(GameObject other)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    other.gameObject.GetComponent<Collider2D>().isTrigger = true;
    //   // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), false);

    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "playerred" || other.gameObject.tag == "playerblue" )
        {
            //get the direction and magnitude of the collision

            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {

                Vector2 collisionDirection = (transform.position - other.gameObject.transform.position).normalized;
                Debug.Log(collisionDirection);
                //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), true);
                // Do something with the collision direction
                rb.AddForce(collisionDirection * 2f, ForceMode2D.Impulse);
                //other.gameObject.GetComponent<Collider2D>().isTrigger = false;
                //StartCoroutine(Wait(other.gameObject));
                //photonView.RPC("CustomBounceRPC", RpcTarget.All, collisionDirection);
            }

        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag != null)
    //    {
    //        StartCoroutine(Wait(collision.gameObject));
    //    }
    //}



    [PunRPC]
    private void CustomBounceRPC(Vector2 relativeVelocity)
    {
        // Apply custom bounce based on relativeVelocity
        rb.AddForce(relativeVelocity*3, ForceMode2D.Impulse);
    }

   
}
