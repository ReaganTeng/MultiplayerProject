using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayAudio : MonoBehaviour
{
    GameObject[] bombtype1;
    GameObject[] bombtype2;
    GameObject[] bombtype3;

    public AudioSource audio;

    //photonView.RPC("PlantSpreadBomb", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position);


    private PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    void PlayExplosion(Vector3 position)
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        bombtype1 = GameObject.FindGameObjectsWithTag("Bomb");
        bombtype2 = GameObject.FindGameObjectsWithTag("StrongBomb");
        bombtype3 = GameObject.FindGameObjectsWithTag("SpreadBomb");

        for(int i = 0; i < bombtype1.Length; i++)
        {
            if(bombtype1[i].GetComponent<BombScript>().getTime() <= 0.1)
            {
                photonView.RPC("PlayExplosion", RpcTarget.AllViaServer, GetComponent<Transform>().position);

            }
        }

        for (int x = 0; x < bombtype2.Length; x++)
        {
            if (bombtype2[x].GetComponent<BombScript>().getTime() <= 0.1)
            {
                photonView.RPC("PlayExplosion", RpcTarget.AllViaServer, GetComponent<Transform>().position);

            }
        }

        for (int z = 0; z < bombtype3.Length; z++)
        {
            if (bombtype3[z].GetComponent<BombScript>().getTime() <= 0.1)
            {
                photonView.RPC("PlayExplosion", RpcTarget.AllViaServer, GetComponent<Transform>().position);

            }
        }



    }
}
