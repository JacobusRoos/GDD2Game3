using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game Manager instantiated on a GameManager Object in MainMenu scene. 
/// ****Make sure to start game from MainMenu scene.
/// </summary>
public class GameManager : MonoBehaviour {
	SceneChanger sc;
	enum GameState { 
		mainmenu, 
		play, 
		pause,
        transition,
        results
	};
	GameState currentState;
	GameState lastState;

	[HideInInspector] public GameObject mainCanvas;			// reference to MainCanvas obj
	[HideInInspector] public GameObject mainEventSystem;	// reference to MainEventSystem obj

	// ** Make sure all groups in Canvas are active to start
	// Turning off and on will be handled in start/update
	// Must be brought into inspector
	public GameObject mainMenuGroup;		// parent object that holds all objects for mainMenu state/scene
	public GameObject playStateGroup;		// parent object that holds all objects for play state/scene

    public List<GameObject> NPCs;
    
    private float timer;

	// Use this for initialization
	void Start () {
		currentState = GameState.mainmenu;

		mainCanvas = GameObject.Find ("MainCanvas");
		mainEventSystem = GameObject.Find ("MainEventSystem");

        timer = 300f;
        
		mainMenuGroup.SetActive (true);
		playStateGroup.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentState != GameState.pause) {		// Pretty much stop updating everything when paused
			// States ------------------------------------------
			// MAIN MENU STATE
			if (currentState == GameState.mainmenu) {
				// Set cooresponding canvas group to display
				if (!mainMenuGroup.activeSelf) {
					mainMenuGroup.SetActive (true);
				}	
				if (playStateGroup.activeSelf) {			
					playStateGroup.SetActive (false);
				}

				// INPUT HANDLERS------------------------------------


			}

			// PLAY STATE
			else if (currentState == GameState.play) {
                
                timer -= Time.deltaTime;
                
                //update UI timer
                
                if(timer <= 0)
                {
                    NextDay();
                }
                
				if (mainMenuGroup.activeSelf) {
					mainMenuGroup.SetActive (false);
				}

				if (!playStateGroup.activeSelf) {
					playStateGroup.SetActive (true);
				}

				// INPUT HANDLERS-----------------------------------
				if (Input.GetMouseButtonDown (0)) {	// AND ALSO CHECK DIALOGUE IS HAPPENING
					// advance the text
				} 

				if (Input.GetKeyDown (KeyCode.P)) {
					ChangeState (2);
				}
			}
		} 
		else if (currentState == GameState.pause) {
			// Unpause - go back to last state
			if (Input.GetKeyDown(KeyCode.P)) {
				currentState = lastState;
			}
		}


		// catch state change
		if (lastState != currentState) {
			Debug.Log ("State changed to: " + currentState);
		}

		lastState = currentState;
	}
    
    private void NextDay()
    {
        for(int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].GetComponent<NPC>().NextDay();
        }
        
        timer = 300f;
    }

	// helper function to change state
	public void ChangeState(int id) {
		switch (id) {
		case 0:	// mainmenu
			currentState = GameState.mainmenu;
			break;
		case 1: // play
			currentState = GameState.play;
			break;
		case 2:	// pause
			currentState = GameState.pause;
			break;
		}
	}

	public void Quit() {
		Application.Quit ();
	}

}
