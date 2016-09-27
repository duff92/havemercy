using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class NetworkManager : Photon.MonoBehaviour
{

    private bool ConnectInUpdate = true;

    public bool AutoConnect = true;
    public byte Version = 1;

    public string vrPrefabName = "Robot Animator";
    public string bvPrefabName = "BV Player";
	public Camera vrCamera;
	public Camera bvCamera;
	public GameObject vrPlayer;
    public GameObject vrSpawnpoint;
    public GameObject bvSpawnpoint;

    public bool EnableBirdCamera;

    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms
    }

    public virtual void Update()
    {
        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
	
        }
    }


    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class PunBehaviour or in enum PhotonNetworkingMessage


    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

		if (EnableBirdCamera) {
			PhotonNetwork.Instantiate (bvPrefabName, bvSpawnpoint.transform.position, bvSpawnpoint.transform.rotation, 0);
			vrCamera.enabled = false;
			bvCamera.enabled = true;
			vrPlayer.GetComponent<GvrViewer> ().VRModeEnabled = false;
		}
		else{
			Debug.Log ("gvrview");
//			vrCamera.enabled = true;
			bvCamera.enabled = false;
		}
    }
}
