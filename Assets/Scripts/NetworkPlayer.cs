using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

    public GameObject myCamera;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    private Rigidbody vrPlayer;

    // Use this for initialization
    void Start () {
	    if(photonView.isMine)
        {
            myCamera.SetActive(true);
            if(this.tag == "Player")
            {
                this.GetComponent<VRMovement>().enabled = true;
                this.GetComponent<GvrViewer>().enabled = true;
                this.GetComponent<GvrHead>().enabled = true;

                foreach (Camera cam in this.GetComponentsInChildren<Camera>())
                    cam.enabled = true;

                this.GetComponentInChildren<StereoController>().enabled = true;
            }
        }
	}

    void Update()
    {
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
        if (stream.isWriting && this.tag == "Player")
        {
            stream.SendNext(GetComponent<Rigidbody>());
        }

        if (!stream.isWriting)
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
