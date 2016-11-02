using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class OnClickInstantiate : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject fakePrefab;

    public int InstantiateType;
    private string[] InstantiateTypeNames = { "Mine", "Scene" };
    public int WallAmount;

    private float xstep = 1.732f;
    private float zstep = 1.5f;

    Ray ray;
    RaycastHit hit;
    Camera cam;
    public bool showGui;

    private bool drawWalls = true;

    //connecting sphere code
    public float radius = 1.5f;
    public Collider[] colliders;
    int i;
    public int wallrowcap = 4;
    ArrayList wallist = new ArrayList();

    void Start()
    {
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!PhotonNetwork.inRoom)
        {
            // only use PhotonNetwork.Instantiate while in a room.
            return;
        }

        //touchbased input instead of clicks
        if (Input.touchCount > 0 && drawWalls)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (ray.origin == null || ray.direction == null)
                return;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitpos = hit.point;
                Vector3 wallposition = GetSpawnPosition(hitpos.x, hitpos.z);

                Vector3 dir = wallposition - Camera.main.transform.position;
                if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 1000))
                {
                    if (!(hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "objective" || hit.transform.gameObject.tag == "fakeobjective"))
                    {
                        //instanciate walls aslong as there are fewer than 10 walls in the scene
                        GameObject[] gol = GameObject.FindGameObjectsWithTag("Wall");
                        if (gol.Length < WallAmount)
                        {
                            //check if there is too many walls connected to it
                            Vector3 checkdroppoint = new Vector3(hit.point.x, 15.0f, hit.point.z);
                            if (CheckIfDroppable(checkdroppoint))
                            {
                                PhotonNetwork.Instantiate(Prefab.name, wallposition + new Vector3(0, 3.5f, 0), Quaternion.identity, 0);
                            }
                        }
                    }
                }

            }
        }
        if (Input.GetMouseButton(0) && drawWalls)
        {
            //ger error om man spelar som vr och försöker skapa väggar
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitpos = hit.point;
                Vector3 wallposition = GetSpawnPosition(hitpos.x, hitpos.z);

                Vector3 dir = wallposition - Camera.main.transform.position;
                if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 1000))
                {
                    if (!(hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "objective" || hit.transform.gameObject.tag == "fakeobjective"))
                    {
                        //instanciate walls aslong as there are fewer than 10 walls in the scene
                        GameObject[] gol = GameObject.FindGameObjectsWithTag("Wall");
                        if (gol.Length < WallAmount)
                        {
                            Vector3 checkdroppoint = new Vector3(hit.point.x, 15.0f, hit.point.z);
                            if (CheckIfDroppable(checkdroppoint))
                            {
                                PhotonNetwork.Instantiate(Prefab.name, wallposition + new Vector3(0, 3.5f, 0), Quaternion.identity, 0);
                            }
                        }
                    }
                }
            }
        }
        if (!drawWalls)
        {
            if (Input.touchCount > 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (ray.origin == null || ray.direction == null)
                    return;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitpos = hit.point;
                    Vector3 wallposition = GetSpawnPosition(hitpos.x, hitpos.z);
                    if (!(hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "objective" || hit.transform.gameObject.tag == "fakeobjective"))
                    {
                        PhotonNetwork.Instantiate(fakePrefab.name, wallposition + new Vector3(0, 1.0f, 0), Quaternion.identity, 0);
                        drawWalls = true;
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (ray.origin == null || ray.direction == null)
                    return;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitpos = hit.point;
                    Vector3 wallposition = GetSpawnPosition(hitpos.x, hitpos.z);
                    if (!(hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "objective" || hit.transform.gameObject.tag == "fakeobjective"))
                    {
                        PhotonNetwork.Instantiate(fakePrefab.name, wallposition + new Vector3(0, 1.0f, 0), Quaternion.identity, 0);
                        drawWalls = true;
                    }
                }
            }

        }
    }

    void OnGUI()
    {
        if (showGui)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 180, 0, 180, 50));
            InstantiateType = GUILayout.Toolbar(InstantiateType, InstantiateTypeNames);
            GUILayout.EndArea();
        }
    }

    public void changeToPowerup()
    {
        // set drawWalls so you drop powerups instead of creating walls
        drawWalls = false;
    }

    // instanciate all wallpieces on top of the hexagonal tiles by calculating which tile index in x- and z-axis it should be placed at
    // http://stackoverflow.com/questions/7705228/hexagonal-grids-how-do-you-find-which-hexagon-a-point-is-in 
    // BUT ALOT OF THINGS CHANGED BECAUSE OF DIFFERENCES BETWEEN JAVA AND UNITY AXIS AND ORIGOS
    // start to check what square it is then check whether or not you clickposition is in any of the triangles overlapping the squares
    Vector3 GetSpawnPosition(float x, float z)
    {
        //shift zinput because origo is not at the correct position compared to the hexagongrid
        float shiftedinput_z = z - 0.25f;
        float x_index_float;
        float xindex;
        float z_index_float = shiftedinput_z / zstep;
        float zindex = Mathf.Round(z_index_float);
        // is the z position in an odd z-row 
        bool zIsOdd = (zindex % 2) != 0;
        if (zIsOdd)
        {
            //offset odd z-rows with half a xstep
            x_index_float = (x - (xstep * 0.5f)) / xstep;
            xindex = Mathf.Round(x_index_float);
        }
        else
        {
            x_index_float = x / xstep;
            xindex = Mathf.Round(x_index_float);
        }

        //check position of click relative to box it is in
        float relZ = shiftedinput_z - (zindex * zstep);
        float relX;
        if (zIsOdd)
        {
            relX = (x - (xindex * xstep)) - (xstep * 0.5f);
        }
        else
        {
            relX = x - (xindex * xstep);
        }

        //check if click position is above the top edges of the hexagon (meaning its inside the hexagon tile above)
        // change zIsOdd if you are in another row.
        float c = 0.5f;
        float m = c / (xstep * 0.5f);
        if ((relZ - 0.25f) > ((m * relX) + c)) //left edge
        {
            //Debug.Log("Topleft!");
            zindex += 1;
            if (!zIsOdd)
            {
                xindex -= 1;
            }
            zIsOdd = !zIsOdd;

        }
        else if ((relZ - 0.25f) > (-m * relX) + c) //right edge
        {
            //Debug.Log("Topright!");
            zindex += 1;
            if (zIsOdd)
            {
                xindex += 1;
            }
            zIsOdd = !zIsOdd;
        }
        //Debug.Log("xindex: " + xindex + ",relx: " + relX);
        //Debug.Log("zindex: " + zindex + ",relz: " + relZ);
        Vector3 spawnpos = new Vector3(xstep * xindex, 0, zstep * zindex);
        if (zIsOdd)
        {
            spawnpos = new Vector3((xstep * xindex) + (xstep * 0.5f), 0, zstep * zindex);
        }

        return spawnpos;
    }

    bool CheckIfDroppable(Vector3 pos)
    {
        wallist = new ArrayList();
        ArrayList firstneighbours = new ArrayList();
        colliders = Physics.OverlapCapsule(pos, new Vector3(pos.x, (pos.y - 15f), pos.z), 1.5f);
        i = 0;
        while (i < colliders.Length)
        {
            //destroy cube if its adjacent to objective or player
            if (colliders[i].gameObject.tag == "objective" || colliders[i].gameObject.tag == "Player")
            {
                return false;
            }
            if (colliders[i].gameObject.tag == "Wall")
            {
                wallist.Add(colliders[i].gameObject);
                firstneighbours.Add(colliders[i].gameObject);

                if (wallist.Count > wallrowcap)
                {
                    return false;
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
            }
            i++;
        }
        //go through all the "first" neighbours of the wall and check for their wallneighbours
        foreach (GameObject go in firstneighbours)
        {
            bool candrop = checkOtherWallNeighbours(go);
            if (!candrop)
            {
                return false;
            }
        }
        return true;
    }

    bool checkOtherWallNeighbours(GameObject go)
    {
        Vector3 newpos = new Vector3(go.transform.position.x, 15.0f, go.transform.position.z);
        Collider[] coll = Physics.OverlapCapsule(newpos, new Vector3(newpos.x, (newpos.y - 15f), newpos.z), 1.5f);
        i = 0;
        while (i < coll.Length)
        {
            if (coll[i].gameObject.tag == "Wall" && !wallist.Contains(coll[i].gameObject)) //check if collider is wall and not in list
            {
                wallist.Add(coll[i].gameObject);
                if (wallist.Count > wallrowcap) //remove wall (aka not spawn it) if there already is 4 walls in clumped together
                {
                    return false;
                    //alternatively:
                    //Network.Destroy(GetComponent<NetworkView>().viewID);
                }
                if (!checkOtherWallNeighbours(coll[i].gameObject))
                {
                    return false;
                }
            }
            i++;
        }
        return true;
    }

}