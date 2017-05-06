using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    private int trust;

    private bool yeti;
    //whether the prediction is true or false for the day
    private bool[] predictionStates;
    
    private bool[] predictionsUsed;
    
    private KeyValuePair<bool, bool>[] givenPredictions;
    
    private int givenPredictionNum;
    
    public int smallIndex;
    public int trueIndex;
    public int falseindex;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dialogue();

        trust = 0;

        predictionStates = new bool[dialogue.predNum];
        
        predictionsUsed = new bool[dialogue.predNum];
        
        givenPredictions = new KeyValuePair<bool, bool>[3];
        
        givenPredictionNum = 0;
        
        trueIndex = -1;
        falseindex = -1;
        
        RandomizePredictions();
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
            predictionStates[i] = false;
            predictionsUsed[i] = false;
        }
        
        bool[] assigned = new bool[dialogue.predNum];
        
        for(int i = 0; i < dialogue.predNum / 2; i++)
        {
            int index = (int)(Random.value * (dialogue.predNum - 1));
            
            if(assigned[index])
            {
                i--;
            }
            else
            {
                predictionStates[index] = true;
            }
        }
    }
    
    public void DisplayDialogueOptions()
    {
        bool trueFound = false;
        bool falseFound = false;
        
        while(!trueFound || !falseFound)
        {
            int r = (int)(Random.value * (dialogue.predNum - 1));
            
            if(predictionStates[r] && !predictionsUsed[r])
            {
                trueFound = true;
                trueIndex = r;
            }
            
            if(!predictionStates[r] && !predictionsUsed[r])
            {
                falseFound = true;
                falseindex = r;
            }
        }
        
        smallIndex = (int)(Random.value * (dialogue.smallNum - 1));
        
        //Todo: put dialogue in the UI here
        
        //NOTE: please update predictionsUsed when a dialogue option is selected so the same one is not used multiple times
        //also increment the 
    }
    
    public void NextDay()
    {
        
    }
}
