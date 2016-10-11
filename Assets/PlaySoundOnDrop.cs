using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnDrop : MonoBehaviour {

	public AudioSource[] walldrop;
	float initYPos = 0;
	System.DateTime timerInit;
	System.DateTime timerCurrent;

	// Use this for initialization
	void Start () {
		initYPos = transform.position.y;
		timerInit = System.DateTime.Now;

		walldrop = GetComponents<AudioSource> ();
		walldrop[0].loop = false;
		walldrop[0].volume = 0.3f;
		walldrop[0].spatialBlend = 0.8f;
		walldrop[0].dopplerLevel = 3.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Y: " + transform.position.y);
		if (transform.position.y <= 3.0326f) {
			walldrop[0].Play();
			timerCurrent = System.DateTime.Now;
			//Debug.Log (timerCurrent - timerInit);
		}
	}
}
