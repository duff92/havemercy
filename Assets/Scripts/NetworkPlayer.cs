using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

    public Camera vrCamera;

	// Use this for initialization
	void Start () {
	    if(photonView.isMine)
        {
            vrCamera.enabled = true;
        }
	}
}
