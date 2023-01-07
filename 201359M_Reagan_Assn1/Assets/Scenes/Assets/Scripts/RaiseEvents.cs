using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class RaiseEvents : MonoBehaviour, IOnEventCallback
{
    public const byte BOMBTRIGGER = 1;
    public const byte BOMBHIT = 2;

    public delegate void OnExplode(string position);
    public static event OnExplode ExplodeEvent;

    public delegate void OnHIt(int targetID);
    public static event OnHIt HitEvent;


    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == BOMBTRIGGER)
        {
            ExplodeEvent?.Invoke(photonEvent.CustomData.ToString());
        }
        else if (eventCode == BOMBHIT)
        {
            HitEvent?.Invoke((int)photonEvent.CustomData);
        }
    }

}
