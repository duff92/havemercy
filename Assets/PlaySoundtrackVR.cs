using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundtrackVR : MonoBehaviour
{

    AudioSource[] soundtrack;

    // Use this for initialization
    void Start()
    {
        soundtrack = GetComponents<AudioSource>();
        soundtrack[1].loop = true;
        soundtrack[1].spatialize = false;
        soundtrack[1].spatialBlend = 0.6f;
        soundtrack[1].reverbZoneMix = 0.65f;
        soundtrack[1].volume = 0.6f;
        soundtrack[1].panStereo = 0;
        soundtrack[1].Play();
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
