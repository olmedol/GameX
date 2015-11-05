using UnityEngine;
using System.Collections;

public class LoadOnClickM : MonoBehaviour {
	
	public GameObject loadingImage;
	
	public void LoadScene(int level)
	{
		bool multi = true;
		loadingImage.SetActive(true);
		Application.LoadLevel(level);
	}
}
