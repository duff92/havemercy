using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour
{
    private Vector3 correctPos = Vector3.zero; // We lerp towards this
    private Quaternion correctRot = Quaternion.identity; // We lerp towards this
    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPos, 0.3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctRot, 0.3f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            
        }
        else
        {
            // Network player, receive data
            this.correctPos = (Vector3)stream.ReceiveNext();
            this.correctRot = (Quaternion)stream.ReceiveNext();
            
        }
    }
}
