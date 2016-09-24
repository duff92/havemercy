using UnityEngine;
using System.Collections;

public class PlayHeartbeat170 : MonoBehaviour {

    public AudioSource heartbeat170;

    // Use this for initialization
    void Start()
    {
        heartbeat170 = GetComponent<AudioSource>();
        heartbeat170.mute = true;
        /*heartbeat170.loop = true;
        heartbeat170.volume = 0.1f;*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
