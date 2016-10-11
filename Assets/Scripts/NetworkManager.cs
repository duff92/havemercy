using System;
using UnityEngine;
using System.Collections;
using UnityEngine.VR;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class NetworkManager : Photon.MonoBehaviour
{

    public bool AutoConnect = true;
    public byte Version = 1;

    public Transform vrSpawnpoint;
    public Transform bvSpawnpoint;
    public Transform objSpawnpoint;
    public bool VRMode;

    public GameObject HUDCanvas;
    public GameObject startButton;
    public GameObject restartButton;
    public Camera sceneCamera;

    private string vrPrefabName = "VR Player";
    private string bvPrefabName = "BV Player";
    private string objPrefabName = "Objective";

    private bool ConnectInUpdate = true;
    private int state = 0;
    
    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms. 
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

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinOrCreateRoom("Sven3", new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinOrCreateRoom("Sven3", new RoomOptions() { MaxPlayers = 4 }, null);
        
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        sceneCamera.enabled = true;
        if (VRMode)
        {
            PhotonNetwork.Instantiate(vrPrefabName, vrSpawnpoint.position, vrSpawnpoint.rotation, 0);
            startButton.SetActive(false);
            restartButton.SetActive(false);
        } else
        {
            PhotonNetwork.Instantiate(bvPrefabName, bvSpawnpoint.position, bvSpawnpoint.rotation, 0);
        }
        sceneCamera.enabled = false;
        HUDCanvas.GetComponent<Animator>().SetTrigger("ShowStartMenu");
    }

    void OnCreatedRoom()
    {
        PhotonNetwork.InstantiateSceneObject(objPrefabName, objSpawnpoint.position, objSpawnpoint.rotation, 0, null);
    }

    public void OnStartButtonClick()
    {
        if(state == 0)
        { 
            this.GetComponent<PhotonView>().RPC("StartGameAnimation", PhotonTargets.All);
        } else
        {
            HUDCanvas.GetComponent<Animator>().SetTrigger("StartGame");
        }
    }

    [PunRPC]
    public void StartGameAnimation()
    {
        this.HUDCanvas.GetComponent<Animator>().SetTrigger("StartGame");
    }
}
