using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


    private bool ConnectInUpdate = true;
    public Camera standByCamera;
    SpawnSpot[] spawnSpots;
	// Use this for initialization
	void Start () {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        PhotonNetwork.autoJoinLobby = false;
	}

    void Update()
    {
        if (ConnectInUpdate && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings("HaveMercy v001");
        }
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnGUI() {
        GUILayout.Label("Ping: " + PhotonNetwork.GetPing().ToString());
        GUILayout.Label("Connection status: " + PhotonNetwork.connectionStateDetailed.ToString());

    }

    void OnJoinedLobby() {
        Debug.Log("OnJoinedLobby()");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() ");
       
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        SpawnTestObject();
    }

    void SpawnTestObject()
    {
        if (spawnSpots == null)
        {
            Debug.Log("No spawn spot");
            return;
        }
        SpawnSpot mySpawnSpot = spawnSpots[Random.Range (0, spawnSpots.Length)];
        PhotonNetwork.Instantiate("TestPlayer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
        standByCamera.enabled = false;
    }
}
