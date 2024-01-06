using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PuckSync : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D rb;
    private float speed = 17;
    PhotonView ph;


    //set the send rate and serialization rate to the desired value
    void Start()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //calculate the new direction of the puck after the collision
        Vector2 newDirection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);

        //apply custom bounce based on the collider type
        if (collision.collider.CompareTag("Player"))
        {
            newDirection *= 1.1f;
        }
        else
        {
            newDirection *= 0.9f;
        }

        //update the velocity and position of the puck after the collision
        rb.velocity = newDirection;
        rb.position = collision.contacts[0].point;

        //if this is the client that owns the puck, send updates about the puck's velocity and position to all other clients
        if (photonView.IsMine)
        {
            photonView.RPC("UpdatePuck", RpcTarget.OthersBuffered, rb.position, newDirection);
        }
    }

    [PunRPC]
    void UpdatePuck(Vector2 position, Vector2 velocity)
    {
        //update the position and velocity of the puck based on the received data
        rb.position = position;
        rb.velocity = velocity;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //if this is the client that owns the puck, send updates about the puck's position and velocity to the master client
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }
        else
        {
            //update the position and velocity of the puck based on the received data
            rb.position = (Vector2)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();
        }
    }
}