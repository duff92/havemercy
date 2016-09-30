using UnityEngine;
using System.Collections;

public class ObjectiveHandler : Photon.MonoBehaviour {

    public Transform objectivePosition;
    private int currentObjectivePosition;
    private Vector3 nextPosition;

    private Vector3[] positionlist = new Vector3[6];
    private Vector3 startPosition = new Vector3(0, 1.0f, 0);
    private Vector3 bottomleftcorner = new Vector3(19.05238f, 1.0f, 0f);
    private Vector3 rightcorner = new Vector3(-16.45444f, 1.0f, -1.499697f);
    private Vector3 leftcorner = new Vector3(8.66015f, 1.0f, 14.99981f);
    private Vector3 toprightcorner = new Vector3(-5.195637f, 1.0f, -14.9991f);
    private Vector3 topleftcorner = new Vector3(-6.928443f, 1.0f, 14.99982f);

    private double time = 0.0f;

    // Use this for initialization
    void Start () {
        positionlist[0] = startPosition;
        positionlist[1] = bottomleftcorner;
        positionlist[2] = rightcorner;
        positionlist[3] = leftcorner;
        positionlist[4] = toprightcorner;
        positionlist[5] = topleftcorner;

        currentObjectivePosition = 0;
        nextPosition = positionlist[0];
    }

    void Update()
    {
        if(nextPosition != transform.position)
            GetComponent<PhotonView>().RPC("UpdateObjectivePosition", PhotonTargets.MasterClient);
    }

    void OnTriggerEnter(Collider other)
    {
        if (Network.time - time < 2.0f)
            return;
        time = Network.time;

        if (other.tag == "Player")
        {
            //Debug.Log("Trigger!");
            HaveMercyLogic.numOfCollectedObjectives++;

            if (currentObjectivePosition == 0)
            {
                currentObjectivePosition = positionlist.Length - 1;
            } else
            {
                currentObjectivePosition--;
            }

            nextPosition = positionlist[currentObjectivePosition];
        }
    }

    [PunRPC]
    void UpdateObjectivePosition()
    {
        this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, nextPosition, 20 * Time.deltaTime);
    }
}
