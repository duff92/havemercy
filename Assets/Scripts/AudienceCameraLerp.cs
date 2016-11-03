using UnityEngine;
using System.Collections;

public class AudienceCameraLerp : MonoBehaviour {

    private GameObject vrPlayer;
    public float smoothness = 10.0f;
	// Use this for initialization
	void Start () {
        vrPlayer = (GameObject) GameObject.Find("VR Player(Clone)");
        if (vrPlayer)
        {
            Debug.Log("Found VR player");
        }
        else
        {
            Debug.LogWarning("Didn't find VR Player for audience camera");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(vrPlayer)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, vrPlayer.transform.position, Time.deltaTime * smoothness);
            transform.rotation = Quaternion.Lerp(transform.rotation, vrPlayer.transform.rotation, Time.deltaTime * smoothness);
        }
        else
        {
            vrPlayer = (GameObject)GameObject.Find("VR Player(Clone)");
        }
	}
}
