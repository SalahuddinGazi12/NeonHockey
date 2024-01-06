using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AirHockey : MonoBehaviourPun, IPunOwnershipCallbacks
{
    GameObject puck;
    PhotonView ph;

    private void Start()
    {
        //puck = GameObject.Find("Puck");
        ph = GetComponent<PhotonView>();
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("playerblue") || collision.gameObject.CompareTag("playerred"))
    //    {
    //        ph.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerblue") || collision.gameObject.CompareTag("playerred"))
        {
            Debug.Log("istrigeerd");
            ph.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
        }
    }


    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
        targetView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        Debug.Log("Ownership transferred to: " + targetView.Owner);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Photon.Realtime.Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }
}