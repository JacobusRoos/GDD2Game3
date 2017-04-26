using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    private int trust;

    private bool yeti;
    
    private bool[] predictionStates;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dialogue();

        trust = 0;

        predictionStates = new bool[dialogue.predNum];
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("test");

        dialogue.ParseDialogue("test");
	}
    
    public void RandomizePredictions()
    {
        for(int i = 0; i < dialogue.predNum; i++)
        {
            predictionState[i] = false;
        }
        
        bool[] assigned = new bool[dialogue.predNum];
        Random r = new Random();
        
        for(int i = 0; i < dialogue.predNum / 2; i++)
        {
            int index = r.Next(dialogue.predNum);
            if(assigned[index])
            {
                i--;
            }
            else
            {
                predictionState[index] = true;
            }
        }
    }
}
