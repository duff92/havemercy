using UnityEngine;
using System.Collections;

public class DestroyWall : Photon.MonoBehaviour
{

    public int lifetime = 5;


    // Update is called once per frame
    void Update () {
	    if(lifetime == 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            lifetime -= 1;
        }
    }
}
