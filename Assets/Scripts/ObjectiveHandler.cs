using UnityEngine;
using System.Collections;

public class ObjectiveHandler : Photon.MonoBehaviour {

    private int currentPositionIndex;
    private Vector3 nextPosition;

    private Vector3[] positionlist = new Vector3[6];
    private Vector3 startPosition = new Vector3(0, 1.0f, 0);
    private Vector3 position1 = new Vector3(19.05238f, 1.0f, 0f);
    private Vector3 position2 = new Vector3(-16.45444f, 1.0f, -1.499697f);
    private Vector3 position3 = new Vector3(8.66015f, 1.0f, 14.99981f);
    private Vector3 position4 = new Vector3(-5.195637f, 1.0f, -14.9991f);
    private Vector3 position5 = new Vector3(-6.928443f, 1.0f, 14.99982f);

    private double time = 0.0f;

    // Use this for initialization
    void Start () {
        positionlist[0] = startPosition;
        positionlist[1] = position1;
        positionlist[2] = position2;
        positionlist[3] = position3;
        positionlist[4] = position4;
        positionlist[5] = position5;

        currentPositionIndex = 0;
        nextPosition = positionlist[0];
    }

    void Update()
    {
        if(GameObject.Find("_SCRIPTS").GetComponent<HaveMercyLogic>().timerClock.gameTime <= 0)
        {
            resetPositionList();
        }

        if(nextPosition != transform.position)
        {
            GetComponent<PhotonView>().RPC("UpdateObjectivePosition", PhotonTargets.MasterClient);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Handler to keep player from triggering event more than once
        if (Network.time - time < 2.0f)
            return;
        time = Network.time;

        if (other.tag == "Player")
        {
            if (currentPositionIndex == 0)
            {
                GameObject.Find("_SCRIPTS").GetComponent<HaveMercyLogic>().startGame();
                currentPositionIndex = positionlist.Length - 1;
            } else
            {
                GameObject.Find("_SCRIPTS").GetComponent<HaveMercyLogic>().collectedObjectives++;

                if (currentPositionIndex == 1)
                {
                    resetPositionList();
                    GameObject.Find("_SCRIPTS").GetComponent<HaveMercyLogic>().endGame();
                    return;
                }
          
                currentPositionIndex--;
            }

            nextPosition = positionlist[currentPositionIndex];
        }
    }

    public void resetPositionList()
    {
        currentPositionIndex = 0;
        nextPosition = positionlist[0];
    }

    [PunRPC]
    void UpdateObjectivePosition()
    {
        this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, nextPosition, 20 * Time.deltaTime);
    }
}
