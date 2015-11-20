using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {
	
	public GameObject loadingImage;
	public static bool onlineStatus;
	
	public void LoadGame(bool online){
		onlineStatus = online;
		LoadScene (1);
	}

	public void LoadScene(int level)
	{
		if (PhotonNetwork.connected)
			PhotonNetwork.Disconnect ();
		//onlineStatus = (level!=0);
		//loadingImage.SetActive(true);
		Time.timeScale = 1;
		Application.LoadLevel(level);
	}

}
