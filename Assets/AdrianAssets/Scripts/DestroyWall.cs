using UnityEngine;
using System.Collections;

public class DestroyWall : Photon.MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (lifetime == 0)
        {
            if (photonView.isMine)
                PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            lifetime -= 1;
        }
    }*/
}