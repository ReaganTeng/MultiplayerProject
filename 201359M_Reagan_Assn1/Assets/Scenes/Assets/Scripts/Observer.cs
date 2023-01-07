using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Observer : MonoBehaviour
{
    private Transform player;
    bool m_IsPlayerInRange;

    private PhotonView photonView;

    private void Awake()
    {
       
        photonView = transform.parent.GetComponent<PhotonView>();
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            player = other.transform;
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            //If theres a wall
            Vector3 direction = player.position - transform.position + Vector3.up;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    Debug.Log("caughtplayer!");
                    photonView.RPC("CallCaughtPlayer", RpcTarget.All);
                }
            }
        }
    }
}
