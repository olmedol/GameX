using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerSingle : MonoBehaviour {
	const string VERSION = "v0.0.1";
	public string roomName = "VVR";
	public string playerPrefabName = "Player";
	public Transform spawnPoint;
	private bool Online;
	// Use this for initialization
	void Start () {
		//comment out connectusingsettings line if you want offline. 
		if (!LoadOnClick.onlineStatus) {
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom (roomName);
		} else {
			PhotonNetwork.ConnectUsingSettings (VERSION);
		}
		print(LoadOnClick.onlineStatus);
		print ("I am currently online:"+ PhotonNetwork.offlineMode);

	}
	
	void OnJoinedLobby(){
		
		//Don't use these 2 lines if you wan't offline mode to work
		if (LoadOnClick.onlineStatus) {
			RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 4};
			PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
		}
		
	}
	
	
	void OnJoinedRoom() { 
		//print(PhotonNetwork.isMasterClient);
		
		PhotonNetwork.Instantiate (playerPrefabName,
		                           spawnPoint.position, 
		                           spawnPoint.rotation,
		                           0);
		
	}
	
}
