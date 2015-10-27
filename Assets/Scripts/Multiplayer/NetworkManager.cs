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
		PhotonNetwork.ConnectUsingSettings (VERSION);
	}

	void OnJoinedLobby(){
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
