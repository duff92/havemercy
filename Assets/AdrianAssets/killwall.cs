using UnityEngine;
using System.Collections;

public class killwall : Photon.MonoBehaviour
{
    public int lifetime = 1000;

    // Update is called once per frame
    void Update()
    {
        if (lifetime == 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            lifetime -= 1;
        }
    }

    // called by InputToEvent.
    // we use a short timeout to detect double clicks.
    // on double click, the networked object gets destroyed (on all clients).
    /*private void OnClick()
    {
        if (!this.photonView.isMine)
        {
            // this networkView (provided by Photon.MonoBehaviour) says the object is not ours.
            // so this client can't destroy it.
            return;
        }

        if (Time.time - this.timeOfLastClick < ClickDeltaForDoubleclick)
        {
            // double click => destory in network
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            this.timeOfLastClick = Time.time;
        }
    }*/
}
