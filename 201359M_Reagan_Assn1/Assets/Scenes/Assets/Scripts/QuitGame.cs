using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class QuitGame : MonoBehaviour
{
    private PhotonView photonView;
    private bool PlayerQuit;

    private int currentLength;


    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        currentLength = 2;

      PlayerQuit = false;
    }


    public void SetCurrentLength(int length)
    {
        currentLength = length;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length < 1)
        {
            PlayerQuit = true;
        }

        Debug.Log("Amount of players "  + currentLength);

        if (PlayerQuit == true)
        {
          Leave();

          QuitMethod();
        }


       
    }

    public void SetQuitTrue()
    {
        
        PlayerQuit = true;
    }

    public void QuitMethod()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (currentLength < 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void Leave()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }



       
    }

    //public override void OnLeftRoom()
    //{
    //    SceneManager.LoadScene(0);
    //    base.OnLeftRoom();
    //}

}
