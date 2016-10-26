using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnFall : MonoBehaviour {

	public AudioSource[] wallfall;
	float initYPos = 0;

	// Use this for initialization
	void Start () {
		initYPos = transform.position.y;

		wallfall = GetComponents<AudioSource>();
		wallfall[1].loop = false;
		wallfall[1].pitch = 0.5f;
		wallfall[1].volume = 0.1f;
		wallfall[1].spatialBlend = 0.8f;
		wallfall[1].dopplerLevel = 3.5f;
		wallfall[1].Play ();
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log ("Y: " + transform.position.y);
		if (transform.position.y <= 3.0326f) {
			wallfall[1].Stop();
		}
	}
}
