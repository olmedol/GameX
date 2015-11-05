using UnityEngine;
using System.Collections;

public class LoadOnClickM : MonoBehaviour {
	
	public GameObject loadingImage;
	public static bool onlineStatus;
	
	public void LoadScene(int level, int onlineStatus2)
	{

		onlineStatus = true;
		loadingImage.SetActive(true);
		Application.LoadLevel(level);
	}
}
