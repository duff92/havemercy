using UnityEngine;
using System.Collections;

public class ObjectiveHandler : MonoBehaviour {

    public int winNr = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Vector2 newpos = Random.insideUnitCircle * 18;
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
