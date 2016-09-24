using UnityEngine;
using System.Collections;

public class PlayHeartbeat145 : MonoBehaviour {

    public AudioSource heartbeat145;

    // Use this for initialization
    void Start()
    {
        heartbeat145 = GetComponent<AudioSource>();
        heartbeat145.loop = true;
        heartbeat145.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
