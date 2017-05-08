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
    public GameObject player;
    
    private float timer;

    private float transitionTimer;
    
    
    private GameObject SelectedNPC;
    
    private Ray ray;
    private RaycastHit hit;
    
    public Camera mainCamera;
    

	// Use this for initialization
	void Start () {
		currentState = GameState.mainmenu;

		mainCanvas = GameObject.Find ("MainCanvas");
		mainEventSystem = GameObject.Find ("MainEventSystem");

        timer = 300f;
        
        transitionTimer = 5f;
        
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
                    
                    ray = mainCamera.ViewportPointToRay(new Vector3(.5f, .5f, .5f));
                    
                    Vector3 loc = mainCamera.transform.position;
                    
                    if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
                    {
                        if(hit.transform.gameObject.tag == "NPC")
                        {
                            Debug.Log("NPChit");
                        }
                    }
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

        
        if(currentState == GameState.transition)
        {
            transitionTimer--;
            
            //enable transition UI
            
            if(transitionTimer <= 0)
            {
                currentState = GameState.play;
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
        
        player.GetComponent<PlayerController>().Reset();
        
        timer = 300f;
        
        currentState = GameState.transition;
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
