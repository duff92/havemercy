using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ConnectAndJoinAvatar : Photon.MonoBehaviour {

	public bool AutoConnect = true;

	public bool ConnectInUpdate = true;
	public byte Version = 1;

	public Camera BirdCamera;
	public Camera VRCamera;


	public virtual void Start() {
		PhotonNetwork.autoJoinLobby = true;

		if (VRSettings.enabled) {
			BirdCamera.enabled = false;
			VRCamera.enabled = true;
		} else {
			BirdCamera.enabled = true;
			VRCamera.enabled = false;
		}
	}

	public virtual void Update() {

		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected) {
			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings (Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}
	}

	public virtual void OnConnectedToMaster() {
		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnPhotonJoinFailed() {
		PhotonNetwork.CreateRoom (null, new RoomOptions () { MaxPlayers = 4 }, null);
	}

	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause) {
		Debug.LogError ("Cause of fail: " + cause);
	}

	public void OnJoinedRoom() {
		Debug.Log("OnJoinedRoom() called");
	}
		
}
