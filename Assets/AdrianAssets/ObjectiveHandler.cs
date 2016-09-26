using UnityEngine;
using System.Collections;

public class ObjectiveHandler : Photon.MonoBehaviour {

    public int winNr = 6;
    public float mindist = 7;
    private float dist;
    private Vector2 newpos;
    private Vector3 newpos3d = new Vector3(0f, 1.5f, 0f);
    private GameObject player;
    private Vector2 playerpos;

    private Vector3[] positionlist = new Vector3[6];
    private Vector3 bottomrightcorner = new Vector3(10.39226f, 1.5f, -17.99931f);
    private Vector3 bottomleftcorner = new Vector3(-10.39187f,1.5f, -17.99924f);
    private Vector3 rightcorner = new Vector3(20.78427f, 1.5f, 0f);
    private Vector3 leftcorner = new Vector3(-20.78427f, 1.5f, 0f);
    private Vector3 toprightcorner = new Vector3(10.39226f, 1.5f, 17.99931f);
    private Vector3 topleftcorner = new Vector3(-10.39187f, 1.5f, 17.99924f);

    // Use this for initialization
    void Start () {

        positionlist[0] = bottomrightcorner;
        positionlist[1] = bottomleftcorner;
        positionlist[2] = rightcorner;
        positionlist[3] = leftcorner;
        positionlist[4] = toprightcorner;
        positionlist[5] = topleftcorner;
    }
	
	// Update is called once per frame
	void Update () {
        //kanske borde ha en random nummer som skapas på vrspelaren, så läser båda bollarna från den för sin random ordning
        transform.position = Vector3.MoveTowards(transform.position, newpos3d, 20 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerpos = new Vector2(player.transform.position.x, player.transform.position.z);
            /*dist = mindist;
            while(dist <= mindist)
            {
                newpos = Random.insideUnitCircle * 18;
                dist = Vector2.Distance(newpos, playerpos);
            }*/
            winNr -= 1;
            //newpos3d = new Vector3(newpos.x, 1.5f, newpos.y);

            if(winNr < 0)
            {
                Debug.Log("winner!");
                winNr = 5;
            }
            newpos3d = positionlist[winNr];
            /*winNr -= 1;
            if(winNr == 0)
            {
                Debug.Log("You win");
                winNr = 5;
            }*/
        }
    }

}
