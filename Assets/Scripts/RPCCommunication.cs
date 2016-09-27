using UnityEngine;
using System.Collections;

public class RPCCommunication : Photon.MonoBehaviour {

    public GameObject HUDCanvas;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [PunRPC]
    public void StartGameAnimation()
    {
        this.HUDCanvas.GetComponent<Animator>().SetTrigger("StartGame");
    }
}
