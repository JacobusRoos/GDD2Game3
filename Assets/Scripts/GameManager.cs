using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        dialogue,
		transition,
		results
	};
	GameState currentState;
	GameState lastState;

	//[HideInInspector] public GameObject mainCanvas;			// reference to MainCanvas obj
	//[HideInInspector] public GameObject mainEventSystem;	// reference to MainEventSystem obj

	// ** Make sure all groups in Canvas are active to start and dragged into inspector
	// Turning off and on will be handled in start/update
	// Must be brought into inspector
	public GameObject mainMenuGroup;
	public GameObject playStateGroup;	
	public GameObject textOptionsGroup;
    public GameObject transitionGroup;


    public List<GameObject> NPCs;
    public GameObject player;
    
    private float timer;

    private float transitionTimer;
    
    
    private GameObject SelectedNPC;
    
    private Ray ray;
    private RaycastHit hit;
    
    public Camera mainCamera;

    public bool endDialogue;

    public GameObject timerOBJ;

    private int day;

	// Use this for initialization
	void Start () {
		currentState = GameState.mainmenu;

		//mainCanvas = GameObject.Find ("MainCanvas");
		//mainEventSystem = GameObject.Find ("MainEventSystem");

		timer = 180f;

        transitionTimer = 5f;

        day = 1;
        
		// activate only mainmenu to start

		mainMenuGroup.SetActive (true);
		playStateGroup.SetActive (false);
		textOptionsGroup.SetActive (false);

        endDialogue = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != GameState.pause)
        {       // Pretty much stop updating everything when paused

            

            // States ------------------------------------------
            // MAIN MENU STATE
            if (currentState == GameState.mainmenu)
            {
                // Set cooresponding canvas group to display
                if (!mainMenuGroup.activeSelf)
                {
                    mainMenuGroup.SetActive(true);
                }
                if (playStateGroup.activeSelf)
                {
                    playStateGroup.SetActive(false);
                }

                // INPUT HANDLERS------------------------------------


            }

            // PLAY STATE

            else if (currentState == GameState.play)
            {
                timer -= Time.deltaTime;

                SetTimer();

                if (timer <= 0)
                {
                    NextDay();
                }


                if (currentState == GameState.play)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ray = mainCamera.ScreenPointToRay(Input.mousePosition);


                        if (Physics.Raycast(ray, out hit, 70))
                        {
                            //Debug.Log(hit.transform.tag);

                            if (hit.transform.tag == "NPC")
                            {
                                player.GetComponent<PlayerController>().canMove = false;

                                hit.transform.gameObject.GetComponent<NPC>().Interact();

                                hit.transform.gameObject.GetComponent<NPC>().DisplayDialogueOptions();

                                SelectedNPC = hit.transform.gameObject;

                                textOptionsGroup.transform.GetChild(4).GetComponent<Text>().text = "Trust: " + SelectedNPC.GetComponent<NPC>().trust.ToString();

                                currentState = GameState.dialogue;
                            }
                        }
                    }

                    if (mainMenuGroup.activeSelf)
                    {
                        mainMenuGroup.SetActive(false);
                    }

                    if (!playStateGroup.activeSelf)
                    {
                        playStateGroup.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        ChangeState(2);
                    }

                }
            }

            else if(currentState == GameState.dialogue)
            {
                timer -= Time.deltaTime;

                SetTimer();

                if (timer <= 0)
                {
                    NextDay();
                }

                if (endDialogue)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        endDialogue = false;

                        playStateGroup.transform.GetChild(0).GetComponent<Text>().text = "";

                        player.GetComponent<PlayerController>().canMove = true;

                        SelectedNPC.GetComponent<NPC>().Interact();

                        currentState = GameState.play;
                    }
                }
            }

            else if (currentState == GameState.pause)
            {
                // Unpause - go back to last state
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ChangeState(1); // default to play 
                }
            }


            else if (currentState == GameState.transition)
            {
                transitionTimer -= Time.deltaTime;

                //enable transition UI

                if (transitionTimer <= 0)
                {
                    StartDay();
                }
            }

            // Debug: catch state change
            if (lastState != currentState)
            {
                //Debug.Log("State changed to: " + currentState);
            }

            lastState = currentState;

        }
    }

    
    private void NextDay()
    {
        for(int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].GetComponent<NPC>().NextDay();
        }
        
        player.GetComponent<PlayerController>().Reset();

        player.GetComponent<PlayerController>().canMove = false;

        timer = 180f;
        
        currentState = GameState.transition;

        playStateGroup.SetActive(false);
        textOptionsGroup.SetActive(false);

        transitionGroup.SetActive(true);

        day++;

        transitionGroup.transform.GetChild(1).GetComponent<Text>().text = "Day " + day;
    }

    private void StartDay()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].GetComponent<NPC>().Interact();
        }

        player.GetComponent<PlayerController>().canMove = true;

        transitionGroup.SetActive(false);

        currentState = GameState.play;
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

    public void StartGame()
    {
        ChangeState(1);

        player.GetComponent<PlayerController>().canMove = true;

        foreach(GameObject n in NPCs)
        {
            n.GetComponent<NPC>().stopped = false;
        }
    }

    public void DisplayDialogue()
    {
        currentState = GameState.dialogue;

        textOptionsGroup.SetActive(true);
    }

    public void NPCTruth()
    {
        textOptionsGroup.SetActive(false);

        playStateGroup.SetActive(true);

        SelectedNPC.GetComponent<NPC>().TruePrediction();

        endDialogue = true;
    }

    public void NPCLie()
    {
        textOptionsGroup.SetActive(false);

        playStateGroup.SetActive(true);

        SelectedNPC.GetComponent<NPC>().FalsePrediction();

        endDialogue = true;
    }

    public void NPCSmall()
    {
        textOptionsGroup.SetActive(false);

        playStateGroup.SetActive(true);

        SelectedNPC.GetComponent<NPC>().SmallTalk();

        endDialogue = true;
    }

    public void NPCYeti()
    {
        textOptionsGroup.SetActive(false);

        playStateGroup.SetActive(true);

        SelectedNPC.GetComponent<NPC>().YetiPrediction();

        endDialogue = true;
    }

    public void CloseDialogue()
    {

        textOptionsGroup.SetActive(false);

        playStateGroup.transform.GetChild(0).GetComponent<Text>().text = "";

        player.gameObject.GetComponent<PlayerController>().canMove = true;

        SelectedNPC.GetComponent<NPC>().Interact();

        currentState = GameState.play;
    }

    public void SetTimer()
    {
        if((int)(timer % 60) < 10)
        {
            timerOBJ.GetComponent<Text>().text = (int)(timer / 60) + ":0" + (int)(timer % 60);
        }
        else
        {
            timerOBJ.GetComponent<Text>().text = (int)(timer / 60) + ":" + (int)(timer % 60);
        }
    }

    public void Quit() {
		Application.Quit ();
	}

}
