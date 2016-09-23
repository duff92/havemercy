using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnDrop : MonoBehaviour {

	public AudioSource walldrop;
	float initYPos = 0;
	System.DateTime timerInit;
	System.DateTime timerCurrent;

	// Use this for initialization
	void Start () {
		initYPos = transform.position.y;
		timerInit = System.DateTime.Now;

		walldrop = GetComponent<AudioSource> ();
		walldrop.loop = false;
		walldrop.volume = 0.15f;
		walldrop.spatialBlend = 0.8f;
		walldrop.dopplerLevel = 3.5f;
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log ("Y: " + transform.position.y);
		if (transform.position.y <= 3.0326f) {
			walldrop.Play();
			timerCurrent = System.DateTime.Now;
			Debug.Log (timerCurrent - timerInit);
		}
	}
}
