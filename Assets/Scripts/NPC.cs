using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    private int trust;

    private bool yeti;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dialogue();

        trust = 0;


	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("test");

        dialogue.ParseDialogue("test");
	}
}
