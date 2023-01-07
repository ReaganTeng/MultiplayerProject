using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//using ExitGames.Client.Photon;
//using Photon.Realtime;

public class GhostRPC : MonoBehaviour
{
    public GameEnding gameEnding;
    private PhotonView photonView;

    // Start is called before the first frame update

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void GhostBombed()
    {
        photonView.RPC("CallBombed", RpcTarget.AllViaServer);
    }

    // Start is called before the first frame update
    [PunRPC]
    public void CallCaughtPlayer()
    {
       if (!gameEnding)
            gameEnding = GameObject.Find("GameEnding").GetComponentInChildren<GameEnding>();
       gameEnding.CaughtPlayer();
    }

    [PunRPC]
    public void CallBombed()
    {
        if (PhotonNetwork.IsMasterClient)

            PhotonNetwork.Destroy(gameObject);
    }

}
