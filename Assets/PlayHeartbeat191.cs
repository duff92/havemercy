using UnityEngine;
using System.Collections;

public class PlayHeartbeat191 : MonoBehaviour {

    public AudioSource[] heartbeat191;
    GameObject[] wall;
    float playerPosX;
    float playerPosZ;
    bool activeRoutine;
    float closeCall = 3.0f;

    // Use this for initialization
    void Start()
    {
        activeRoutine = false;
        playerPosX = this.transform.position.x;
        playerPosZ = this.transform.position.z;

        heartbeat191 = GetComponents<AudioSource>();
        heartbeat191[0].volume = 1.0f;
        heartbeat191[0].loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        wall = GameObject.FindGameObjectsWithTag("Wall");
        // Debug.Log("Amount in Tag Wall: " + wall.Length);

        if (activeRoutine == false)
        {

            foreach (GameObject w in wall)
            {
                if ((Mathf.Abs(playerPosX - w.transform.position.x) <= closeCall) && (Mathf.Abs(playerPosZ - w.transform.position.z) <= closeCall) && w.transform.position.y > 3.5f)
                {
                    // Debug.Log("Active routine = " + activeRoutine);
                    activeRoutine = true;
                    heartPumping();
                    // Debug.Log("heartPumping() was called, activeRoutine = " + activeRoutine);
                }
            }

        }
    }

    void heartPumping()
    {
        // Debug.Log("Set to true? " + activeRoutine);
        StartCoroutine(timeWait());
    }

    private IEnumerator timeWait()
    {
        heartbeat191[0].Play();
        // Debug.Log("Play() called");
        yield return new WaitForSeconds(5.0f);
        heartbeat191[0].Stop();
        activeRoutine = false;
        // Debug.Log("Play() stopped");
    }

}
