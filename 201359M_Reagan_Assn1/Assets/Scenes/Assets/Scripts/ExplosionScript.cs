using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;



public class ExplosionScript : MonoBehaviour
{
    private GameObject[] photonView;

    private float explosionduration;

    private bool isFromBomb2 = false;

    //only applies to strongbomb
    public void setfromBomb2()
    {
        isFromBomb2 = true;
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int x = 0; x < players.Length; x++)
        {
            if (other == players[x].GetComponent<CapsuleCollider>())
            {
                players[x].GetComponent<QuitGame>().SetQuitTrue();

                for (int y = 0; y < players.Length; y++)
                {
                    players[y].GetComponent<QuitGame>().SetCurrentLength(players.Length);
                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        explosionduration = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
       

        //disappear according to duration  particle system
        explosionduration += Time.deltaTime;

        if (explosionduration > 0.5f)
        {
            PhotonNetwork.Destroy(gameObject);
        }


        //only applies to StrongBomb
        
            // PhotonNetwork.AutomaticallySyncScene = false;

            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.1f);
            foreach (var hitCollider in hitColliders)
            {

                if ( 
                    (hitCollider.tag == "wall") 
                || (hitCollider.tag == "ceiling" ) 
                    /*&&*/ )
                {
                    if (isFromBomb2 /*&& hitCollider.gameObject.GetComponent<DeleteWallCeiling>().PhotonBombed()*/)
                    {
                         //PhotonNetwork.Destroy(hitCollider.gameObject);
                       hitCollider.gameObject.GetComponent<DeleteWallCeiling>().PhotonBombed();
                        //hitCollider.gameObject.GetComponent<DeleteWallCeiling>().PhotonBombed();
                            
                    }
                }

            }
        
    }


}
