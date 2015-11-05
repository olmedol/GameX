using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {
	
	public GameObject loadingImage;
	public static bool onlineStatus;
	
	public void LoadScene(int level)
	{
		onlineStatus = (level!=0);
		loadingImage.SetActive(true);
		Application.LoadLevel(1);
	}

}
