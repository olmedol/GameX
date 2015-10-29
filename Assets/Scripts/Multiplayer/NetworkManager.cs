using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.0.1";
	public string roomName = "VVR";
	public string playerPrefabName = "Player";
	public Transform spawnPoint;
	// Use this for initialization
	void Start () {
		//comment out connectusingsettings line if you want offline. 
		//PhotonNetwork.offlineMode = true;
		//PhotonNetwork.CreateRoom (roomName);
		PhotonNetwork.ConnectUsingSettings (VERSION);
	}

	void OnJoinedLobby(){

		//Don't use these 2 lines if you wan't offline mode to work
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 4};
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);

	}


	void OnJoinedRoom() { 
		print(PhotonNetwork.isMasterClient);

		PhotonNetwork.Instantiate (playerPrefabName,
		                          spawnPoint.position, 
		                          spawnPoint.rotation,
		                          0);

	}

}
