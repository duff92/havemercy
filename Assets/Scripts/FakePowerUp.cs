﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FakePowerUp : Photon.MonoBehaviour {

    GameObject player;
    public float slowtime = 3.0f;
    private double initiateSlowTime;
    private bool changedSpeed = false;
    public float slowValue = 2.0f;
    // Use this for initialization

	public AudioSource slowdownSound;

	void Start () {
		slowdownSound = GetComponent<AudioSource>();
		slowdownSound.volume = 0.6f;
		slowdownSound.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (changedSpeed)
        {
            if (Network.time - initiateSlowTime > slowtime)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<VRMovement>().speed = 6.0f;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<VRMovement>().speed = player.GetComponent<VRMovement>().speed = slowValue;
            //slow for a short period of time, start countdown in update() with this code
            changedSpeed = true;
            initiateSlowTime = Network.time;
            //so you can't collide with the fakeobjective after hitting it
            transform.GetComponent<SphereCollider>().enabled = false;

			slowdownSound.Play();
        }
    }
}
