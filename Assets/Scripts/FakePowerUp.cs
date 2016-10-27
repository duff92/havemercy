﻿using UnityEngine;
using System.Collections;

public class FakePowerUp : Photon.MonoBehaviour {

    GameObject player;
    public float slowtime = 3.0f;
    private double initiateSlowTime;
    private bool changedSpeed = false;
    public float slowMultiple = 3.0f;
    // Use this for initialization

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (changedSpeed)
        {
            if (Network.time - initiateSlowTime > slowtime)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<VRMovement>().speed = player.GetComponent<VRMovement>().speed * slowMultiple;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<VRMovement>().speed = player.GetComponent<VRMovement>().speed = player.GetComponent<VRMovement>().speed / slowMultiple;
            //slow for a short period of time, start countdown in update() with this code
            changedSpeed = true;
            initiateSlowTime = Network.time;
            //so you can't collide with the fakeobjective after hitting it
            transform.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
