using UnityEngine;
using System.Collections;

public class PlayHeartbeat110 : MonoBehaviour {

    public AudioSource heartbeat110;

    // Use this for initialization
    void Start()
    {
        heartbeat110 = GetComponent<AudioSource>();
        heartbeat110.mute = true;
        /*heartbeat110.loop = true;
        heartbeat110.volume = 0.1f;*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
