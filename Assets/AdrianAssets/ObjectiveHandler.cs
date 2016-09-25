using UnityEngine;
using System.Collections;

public class ObjectiveHandler : MonoBehaviour {

    public int winNr = 5;
    public float mindist = 7;
    private float dist;
    private Vector2 newpos;
    private Vector3 newpos3d = new Vector3(0f, 1.5f, 0f);
    private GameObject player;
    private Vector2 playerpos;
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, newpos3d, 20 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.tag == "Player")
        {
            player = other.gameObject;
            playerpos = new Vector2(player.transform.position.x, player.transform.position.z);
            dist = mindist;
            while(dist <= mindist)
            {
                newpos = Random.insideUnitCircle * 18;
                dist = Vector2.Distance(newpos, playerpos);
            }

            newpos3d = new Vector3(newpos.x, 1.5f, newpos.y);
            //transform.Translate(new Vector3(newpos.x, 1.5f, newpos.y)*Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, newpos, 10 * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, newpos, Time.deltaTime * 5);
            winNr -= 1;
            if(winNr == 0)
            {
                Debug.Log("You win");
                winNr = 5;
            }
        }
    }

}
