using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayHeartbeat90 : MonoBehaviour {

    public AudioSource heartbeat90;

	// Use this for initialization
	void Start () {
        heartbeat90 = GetComponent<AudioSource>();
        heartbeat90.loop = true;
        heartbeat90.volume = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
