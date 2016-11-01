using UnityEngine;
using System.Collections;

public class DestroyWall : Photon.MonoBehaviour
{
    public float wallLifetime = 5.0f;
    private double initiateWall = 0.0f;
    private bool stopRPC = false;

    public void Start()
    {
        initiateWall = PhotonNetwork.time;
    }

    public void Update()
    {
        if(PhotonNetwork.time - initiateWall > wallLifetime)
        {
            Debug.Log("Destroy wall");
            if (!stopRPC)
            {
                stopRPC = true;
                photonView.RPC("RemoveWall", PhotonTargets.MasterClient);
            }
        }
    }

    [PunRPC]
    public void RemoveWall()
    {
        Debug.Log("PUNRPC REMOVE WALL");
        if (photonView.isMine)
        {
            Debug.Log("Photonview is mine and destroying wall");
            PhotonNetwork.Destroy(gameObject);
        }
    }
}