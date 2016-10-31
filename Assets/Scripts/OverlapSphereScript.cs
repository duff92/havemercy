using UnityEngine;
using System.Collections;

public class OverlapSphereScript : MonoBehaviour
{

    //might need Photon.MonoBehaviour
    public float radius = 1.5f;
    public Collider[] colliders;
    int i;
    public int wallrowcap = 4;

    //use arraylist instead

    public ArrayList wallist = new ArrayList();
    public ArrayList firstneighbours = new ArrayList();
    //public GameObject[] wallist;

    // Use this for initialization
    void Start()
    {
        colliders = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, (transform.position.y - 15f), transform.position.z), 1.5f);
        i = 0;
        wallist.Add(this.gameObject);
        while (i < colliders.Length)
        {
            //destroy cube if its adjacent to objective or player
            if (colliders[i].gameObject.tag == "objective" || colliders[i].gameObject.tag == "Player")
            {
                wallist.Remove(this.gameObject);
                GetComponent<PhotonView>().RPC("RemoveWall", PhotonTargets.MasterClient);
                return;
            }
            if (colliders[i].gameObject.tag == "Wall")
            {
                wallist.Add(colliders[i].gameObject);
                firstneighbours.Add(colliders[i].gameObject);

                if (wallist.Count > (wallrowcap + 1))
                {
                    wallist.Remove(this.gameObject);
                    GetComponent<PhotonView>().RPC("RemoveWall", PhotonTargets.MasterClient);
                    return;
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
            }         
            i++;
        }
        //go through all the "first" neighbours of the wall and check for their wallneighbours
        foreach (GameObject go in firstneighbours)
        {
            checkOtherWallNeighbours(go);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void checkOtherWallNeighbours(GameObject go)
    {
        Vector3 newpos = new Vector3(go.transform.position.x, transform.position.y, go.transform.position.z);
        Collider[] coll = Physics.OverlapCapsule(newpos, new Vector3(newpos.x, (newpos.y - 15f), newpos.z), 1.5f);
        i = 0;
        while (i < coll.Length)
        {
            if (coll[i].gameObject.tag == "Wall" && !wallist.Contains(coll[i].gameObject)) //check if collider is wall and not in list
            {
                wallist.Add(coll[i].gameObject);
                if (wallist.Count > (wallrowcap + 1)) //remove wall (aka not spawn it) if there already is 4 walls in clumped together
                {
                    wallist.Remove(gameObject);
                    GetComponent<PhotonView>().RPC("RemoveWall", PhotonTargets.MasterClient);
                    return;
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
                checkOtherWallNeighbours(coll[i].gameObject);
            }
            i++;
        }
    }
}