using UnityEngine;
using System.Collections;

public class DestroyWall : Photon.MonoBehaviour
{
    public float wallLifetime = 5.0f;
    private double initiateWall;

    public void Start()
    {
        initiateWall = Network.time;
    }

    public void Update()
    {
        if(Network.time - initiateWall > wallLifetime)
        {
            photonView.RPC("RemoveWall", PhotonTargets.MasterClient );
        }
    }

    [PunRPC]
    public void RemoveWall()
    {
        Debug.Log("PUNRPC REMOVE WALL");
        if(PhotonNetwork.isMasterClient)
            PhotonNetwork.Destroy(this.gameObject);
    }
}