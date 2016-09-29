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
    //public GameObject[] wallist;


    // Use this for initialization
    void Start()
    {
        colliders = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, (transform.position.y - 15f), transform.position.z), 1.5f);
        i = 0;
        wallist.Add(this.gameObject);
        while (i < colliders.Length)
        {
            if (colliders[i].gameObject.tag == "Wall")
            {
                wallist.Add(colliders[i].gameObject);
                if (wallist.Count > (wallrowcap + 1))
                {
                    Debug.Log(wallist.Count);
                    PhotonNetwork.Destroy(gameObject);
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
            }
            i++;
        }
        //go through all neighbours and check for more wallneighbours
        foreach (GameObject go in wallist)
        {
            if (go != this.gameObject)
            {
                checkOtherWallNeighbours(go);
            }
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

                if (wallist.Count > (wallrowcap + 1))
                {
                    Debug.Log("recursion error");
                    PhotonNetwork.Destroy(gameObject);
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
                checkOtherWallNeighbours(coll[i].gameObject);
            }
            i++;
        }
    }

}