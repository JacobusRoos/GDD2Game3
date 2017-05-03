using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game Manager instantiated on a GameManager Object in MainMenu scene. 
/// ****Make sure to start game from MainMenu scene.
/// </summary>
public class GameManager : MonoBehaviour {
	SceneChanger sm;
	enum GameState { mainmenu, play };
	GameState currentState;
	GameState lastState;

	[HideInInspector] public GameObject mainCanvas;			// reference to MainCanvas obj
	[HideInInspector] public GameObject mainEventSystem;	// reference to MainEventSystem obj

	private GameObject mainMenuGroup;		// parent object that holds all objects for mainMenu state/scene
	private GameObject playStateGroup;		// parent object that holds all objects for play state/scene

	// Use this for initialization
	void Start () {
		currentState = GameState.mainmenu;

		mainCanvas = GameObject.Find ("MainCanvas");
		mainEventSystem = GameObject.Find ("MainEventSystem");
		mainMenuGroup = GameObject.Find ("MainMenuGroup");		// could make a list of the groups if we were to have many more screens and scenes, easier to activate/deactivate
		playStateGroup = GameObject.Find ("PlayStateGroup");

		mainMenuGroup.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		lastState = currentState;


		// States ------------------------------------------
		if (currentState == GameState.mainmenu) {
			// Set cooresponding canvas group to display
			if (!mainMenuGroup.activeSelf) {
				mainMenuGroup.SetActive (true);
			}	
			if (playStateGroup.activeSelf) {			//////////////////////////
				playStateGroup.SetActive (false);
			}

			// INPUT HANDLERS------------------------------------


		}
		else if (currentState == GameState.play) {
			if (mainMenuGroup.activeSelf) {
				mainMenuGroup.SetActive (false);
			}
			if (!playStateGroup.activeSelf) {
				playStateGroup.SetActive (true);
			}

			// INPUT HANDLERS-----------------------------------
			if (Input.GetMouseButtonDown(0)) {	// AND ALSO CHECK DIALOGUE IS HAPPENING
				// advance the text
			} 
		}




		// catch state change
		if (lastState != currentState) {
			Debug.Log ("State changed to: " + currentState);
		}



	}

	public void ChangeState(int id) {
		switch (id) {
		case 0:
			currentState = GameState.mainmenu;
			break;
		case 1:
			currentState = GameState.play;
			break;
		}
	}

	public void Quit() {
		Application.Quit ();
	}

}
