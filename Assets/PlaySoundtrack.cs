using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundtrack : MonoBehaviour {

    AudioSource soundtrack;

	// Use this for initialization
	void Start () {
        soundtrack = GetComponent<AudioSource>();
        soundtrack.loop = false;
        soundtrack.spatialize = true;
        soundtrack.spatialBlend = 0.7f;
        soundtrack.reverbZoneMix = 0.65f;
        soundtrack.volume = 0.1f;
        soundtrack.panStereo = 0;
        soundtrack.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*private IEnumerator playTimer()
    {
        yield return new WaitForSeconds(2.0f);
        soundtrack.Play();
    }*/
}
