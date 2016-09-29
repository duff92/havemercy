using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

    public GameObject myCamera;

    // Use this for initialization
    void Start () {
	    if(photonView.isMine)
        {
            myCamera.SetActive(true);
            if(this.tag == "Player")
            {
                GvrViewer gvrViewer = this.GetComponent<GvrViewer>();
                this.GetComponent<VRMovement>().enabled = true;
                this.GetComponent<GvrHead>().enabled = true;

                gvrViewer.enabled = true;
                gvrViewer.VRModeEnabled = true;

                foreach (Camera cam in this.GetComponentsInChildren<Camera>())
                    cam.enabled = true;

                this.GetComponentInChildren<StereoController>().enabled = true;
            }
        }
	}
}
