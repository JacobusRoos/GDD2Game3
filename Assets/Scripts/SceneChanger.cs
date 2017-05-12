using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void ChangeScene (int targetSceneID) {
		//Debug.Log("ChangeScene called: #" + targetSceneID);
		SceneManager.LoadScene(targetSceneID, LoadSceneMode.Single);
	}

	public void AddScene (int targetSceneID) {
		//Debug.Log("AddScene called: #" + targetSceneID);
		SceneManager.LoadScene(targetSceneID, LoadSceneMode.Additive);
	}

	public void UnloadScene (int targetSceneID) {
		//Debug.Log("UnloadScene called: #" + targetSceneID);
		SceneManager.UnloadSceneAsync(targetSceneID);

	}
}