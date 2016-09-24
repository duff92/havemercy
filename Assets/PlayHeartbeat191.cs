using UnityEngine;
using System.Collections;

public class PlayHeartbeat191 : MonoBehaviour {

    public AudioSource heartbeat191;
    GameObject timer;

    // Use this for initialization
    void Start()
    {
        timer = GameObject.FindGameObjectsWithTag("Finish")[0];
        heartbeat191 = GetComponent<AudioSource>();
        heartbeat191.mute = true;
        /*heartbeat191.loop = true;
        heartbeat191.volume = 0.1f;*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
