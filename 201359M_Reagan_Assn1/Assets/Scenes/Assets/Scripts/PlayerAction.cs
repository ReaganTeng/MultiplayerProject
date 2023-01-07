using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerAction : MonoBehaviour
{
    public GameObject bombPrefab;
    private PhotonView photonView;
    private PlayerInventory playerInventory;


    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerInventory = GameObject.Find("GameManager").GetComponent<PlayerInventory>();
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown("1"))
            {
                if (playerInventory.GetBomb1() > 0)
                {
                    playerInventory.SubtractBomb1();
                    photonView.RPC("PlantBomb", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position);
                }
            }
            else if (Input.GetKeyDown("2"))
            {
                if (playerInventory.GetBomb2() > 0)
                {
                    playerInventory.SubtractBomb2();
                    photonView.RPC("PlantStrongBomb", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position);
                }
            }
            else if (Input.GetKeyDown("3"))
            {
                if (playerInventory.GetBomb3() > 0)
                {
                    playerInventory.SubtractBomb3();
                    photonView.RPC("PlantSpreadBomb", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position);
                }
            }
        }
    }



    [PunRPC]
    public void PlantBomb(Vector3 position)
    {
        if (PhotonNetwork.IsMasterClient)
        {

            GameObject bomb = PhotonNetwork.Instantiate("Bomb", new Vector3(Mathf.RoundToInt(position.x), 
			  bombPrefab.transform.position.y, Mathf.RoundToInt(position.z)),
			  bombPrefab.transform.rotation);  
        }
    }

    [PunRPC]
    public void PlantStrongBomb(Vector3 position)
    {
        if (PhotonNetwork.IsMasterClient)
        {

            GameObject bomb = PhotonNetwork.Instantiate("StrongBomb", new Vector3(Mathf.RoundToInt(position.x),
              bombPrefab.transform.position.y, Mathf.RoundToInt(position.z)),
              bombPrefab.transform.rotation);
        }
    }


    [PunRPC]
    public void PlantSpreadBomb(Vector3 position)
    {


        if (PhotonNetwork.IsMasterClient)
        {

            GameObject bomb = PhotonNetwork.Instantiate("SpreadBomb", new Vector3(Mathf.RoundToInt(position.x),
              bombPrefab.transform.position.y, Mathf.RoundToInt(position.z)),
              bombPrefab.transform.rotation);
        }
    }

}
