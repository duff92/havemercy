using UnityEngine;
using System.Collections;

public class CubeNetwork : Photon.MonoBehaviour
{

    private Vector3 correctPos = Vector3.zero;
    private Quaternion correctRot = Quaternion.identity;

    void Awake()
    {
        correctPos = transform.position;
        correctRot = transform.rotation;
        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            correctPos = (Vector3)stream.ReceiveNext();
            correctRot = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctRot, Time.deltaTime * 5);
            print("your turn" + gameObject.name + photonView.isMine);
        }
    }
}