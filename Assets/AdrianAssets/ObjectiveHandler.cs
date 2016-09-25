using UnityEngine;
using System.Collections;

public class ObjectiveHandler : MonoBehaviour {

    public int winNr = 5;
    public float mindist = 7;
    private float dist;
    private Vector2 newpos;
    private GameObject player;
    private Vector2 playerpos;
    
	// Use this for initialization
	void Start () {
        GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerpos = new Vector2(player.transform.position.x, player.transform.position.z);
            dist = mindist;
            while(dist <= mindist)
            {
                newpos = Random.insideUnitCircle * 18;
                dist = Vector2.Distance(newpos, playerpos);
            }
            transform.position = new Vector3(newpos.x, 1.5f, newpos.y);
            winNr -= 1;
            if(winNr == 0)
            {
                Debug.Log("You win");
                winNr = 5;
            }
        }
    }

}
