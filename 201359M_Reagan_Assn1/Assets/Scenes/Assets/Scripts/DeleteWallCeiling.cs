using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

//using ExitGames.Client.Photon;
//using Photon.Realtime;

public class DeleteWallCeiling : MonoBehaviour
{
    private PhotonView photonView;

    // Start is called before the first frame update

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        //PhotonNetwork.AutomaticallySyncScene = false;

    }



    public void PhotonBombed()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        photonView.RPC("Bombed", RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void Bombed()
    {
      
        if (PhotonNetwork.IsMasterClient)     
            PhotonNetwork.Destroy(gameObject);



    }

}
