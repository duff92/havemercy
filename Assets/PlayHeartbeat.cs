using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayHeartbeat : MonoBehaviour {
    public AudioSource rate90;
    public AudioSource rate110;
    public AudioSource rate145;
    public AudioSource rate170;
    public AudioSource rate191;
    public AudioSource[] heartrates;

    // Use this for initialization
    void Start () {
        heartrates = GetComponents<AudioSource>();
        rate90 = heartrates[0];
        rate90.mute = true;
        rate110 = heartrates[1];
        rate110.mute = true;
        rate145 = heartrates[2];
        rate145.mute = true;
        rate170 = heartrates[3];
        rate170.mute = true;
        rate191 = heartrates[4];
        rate191.mute = true;

        heartCalm(true);
        StartCoroutine(WaitTimer());
        heartCalm(false);
        heartScared(true);
    }

    public IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(10.0f); // waits 10 seconds
    }

    // Update is called once per frame
    void Update () {

    }

    void heartCalm(bool isPlaying)
    {
        rate90.mute = false;
        rate90.volume = 0.2f;

        if (isPlaying == true)
        {
            rate90.loop = true;
            rate90.Play();
        }
        if (isPlaying == false)
        {
            rate90.Stop();
        }
    }

    void heartMedium(bool isPlaying)
    {
        rate110.mute = false;
        rate110.volume = 0.4f;

        if (isPlaying == true)
        {
            rate110.loop = true;
            rate110.Play();
        }
        if (isPlaying == false)
        {
            rate110.Stop();
        }
    }

    void heartScared(bool isPlaying)
    {
        rate191.mute = false;
        rate191.volume = 0.35f;

        if (isPlaying == true)
        {
            rate191.loop = true;
            rate191.Play();
        }
        if (isPlaying == false)
        {
            rate191.Stop();
        }
    }
}
