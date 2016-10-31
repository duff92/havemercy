using UnityEngine;
using System.Collections;

public class FakePowerUp : Photon.MonoBehaviour {

    GameObject player;
    public float slowtime = 3.0f;
    private double initiateSlowTime;
    private bool changedSpeed = false;
    public float slowValue = 2.0f;
	
	// Update is called once per frame
	void Update () {
        if (changedSpeed)
        {
            if (Network.time - initiateSlowTime > slowtime)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<VRMovement>().speed = 6.0f;
                photonView.RPC("RemoveFakeObjective", PhotonTargets.MasterClient);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger fake");
        if (other.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<VRMovement>().speed = player.GetComponent<VRMovement>().speed = slowValue;
            //slow for a short period of time, start countdown in update() with this code
            changedSpeed = true;
            initiateSlowTime = Network.time;
            //so you can't collide with the fakeobjective after hitting it
            transform.GetComponent<SphereCollider>().enabled = false;
        }
    }

    [PunRPC]
    void RemoveFakeObjective()
    {
        PhotonNetwork.Destroy(GameObject.Find("fakeObjective"));
    }
}
