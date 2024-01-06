using Photon.Pun;
using UnityEngine;
public class PlayerDistrubuter : MonoBehaviourPunCallbacks
{
    [SerializeField] PhotonView _lowerPlayer, _upperPlayer;

   
    

    void DistributePlayers()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (i == 0)
                _lowerPlayer.TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);

            if (i == 1)
                _lowerPlayer.TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);
        }
    }

    private void OnEnable()
    {
        DistributePlayers();
    }
}
