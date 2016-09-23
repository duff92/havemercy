using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnFall : MonoBehaviour {

	public AudioSource wallfall;
	public AudioSource[] soundList;
	float initYPos = 0;

	// Use this for initialization
	void Start () {
		initYPos = transform.position.y;

		soundList = GetComponents<AudioSource> ();
		wallfall = soundList [1];
		wallfall.loop = false;
		wallfall.pitch = 0.5f;
		wallfall.volume = 0.1f;
		wallfall.spatialBlend = 0.8f;
		wallfall.dopplerLevel = 3.5f;
		wallfall.Play ();
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log ("Y: " + transform.position.y);
		if (transform.position.y <= 3.0326f) {
			wallfall.Stop();
		}
	}
}
