using UnityEngine;
using System.Collections;

public class DeactivateObject : MonoBehaviour
{

    GameObject hideObjective;
    private int HIDE_DURATION = 300;
    private Vector3 positionHide = new Vector3(-100.0f, 1.0f, -100.0f);
    //new Vector3(this.transform.position.x, -5.0f, this.transform.position.z)

    // Use this for initialization
    void Start()
    {
        hideObjective = GameObject.FindGameObjectWithTag("hideobjective");
        // hideObjective = GameObject.FindWithTag("hideobjective");
    }

    // Update is called once per frame
    void Update()
    {

        if (hideObjective == null)
        {
            // Debug.Log ("hideObjective is NULL");
        }
        else
        {
            // Debug.Log (hideObjective.GetComponent<ShakeCooldown>().isPhoneShaking);
            if (hideObjective.GetComponent<ShakeCooldown>().hideTheObject == true)
            {
                GetComponent<PhotonView>().RPC("HideObjectivePosition", PhotonTargets.MasterClient);
            }
        }
    }

    [PunRPC]
    void HideObjectivePosition()
    {
        this.GetComponent<Transform>().position = Vector3.MoveTowards(this.GetComponent<Transform>().position, new Vector3(this.transform.position.x, -10.0f, this.transform.position.z), HIDE_DURATION * Time.deltaTime);
    }
}