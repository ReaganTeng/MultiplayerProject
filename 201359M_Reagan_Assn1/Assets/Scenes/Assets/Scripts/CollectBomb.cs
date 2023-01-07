using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;


public class CollectBomb : MonoBehaviour
{

    public bool isMine = false;
   
    private PhotonView photonView;
    private PlayerInventory playerInventory;
    private bool touched;
    //

    void Awake()
    {
        
        playerInventory = GameObject.Find("GameManager").GetComponent<PlayerInventory>();
        photonView = GetComponent<PhotonView>();
        touched = false;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "BombCollectible")
            {
                playerInventory.AddBomb1();
            }
            else if (gameObject.tag == "StrongBombCollectible")
            {
                playerInventory.AddBomb2();

            }
            else if (gameObject.tag == "SpreadBombCollectible")
            {
                playerInventory.AddBomb3();

            }
            touched = true;
        }
    }

    [PunRPC]
    public void CollectingBomb()
    {
        
        PhotonNetwork.Destroy(gameObject);
        touched = false;
        
        
    }


    // Update is called once per frame
    void Update()
    {
        if (touched)
        {
            photonView.RPC("CollectingBomb", RpcTarget.AllViaServer);
        }
       
    }
}
