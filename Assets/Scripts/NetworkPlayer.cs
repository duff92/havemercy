using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

    public Camera myCamera;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    private Rigidbody vrPlayer;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            myCamera.enabled = true;
            if(GetComponent<GvrViewer>())
                GetComponent<GvrViewer>().VRModeEnabled = true;
        }
    }

    void Update()
    {
        if (photonView.isMine)
        {
            //MOVEMENT/ACTION SCRIPT ENABLED
        }

        if (!photonView.isMine && vrPlayer != null)
            SyncedMovement();
    }

    void SyncedMovement()
    {
        syncTime += Time.deltaTime;
        vrPlayer.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && GetComponent<Rigidbody>() != null)
        {
            stream.SendNext(GetComponent<Rigidbody>());
        }

        if (!stream.isWriting && vrPlayer != null)
        {
            vrPlayer = (Rigidbody)stream.ReceiveNext();

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = vrPlayer.position + vrPlayer.velocity * syncDelay;
            syncStartPosition = vrPlayer.position;
        }
    }
}
