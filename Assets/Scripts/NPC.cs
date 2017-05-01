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
    
    private bool[] smallUsed;
    
    private int givenPredictionNum;
    
    public int smallIndex;
    public int trueIndex;
    public int falseindex;
    
    private int trustBuffer;
    
    const int trustModifier = 15;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dialogue();

        trust = 20 + (int)(Random.value * 30) ;

        predictionStates = new bool[dialogue.predNum];
        
        predictionsUsed = new bool[dialogue.predNum];
        
        smallUsed = new bool[dialogue.smallNum];
        
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
        
        int r = 0;
        
        while(!trueFound || !falseFound)
        {
            r = (int)(Random.value * (dialogue.predNum - 1));
            
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
        
        r = (int)(Random.value * (dialogue.smallNum - 1));
        
        while(!smallUsed[r])
        {
            smallUsed[r] = false;
            smallIndex = r;
        }
        
        //Todo: put dialogue in the UI here
        
        //NOTE: please update predictionsUsed when a dialogue option is selected so the same one is not used multiple times
        //also increment the givenPredictionNum
    }
    
    public void NextDay()
    {
        RandomizePredictions();
        
        smallUsed = new bool[dialogue.smallNum];
        
        givenPredictionNum = 0;
        
        trust += trustBuffer;
        
        trustBuffer = 0;
    }
    
    public void YetiPrediction()
    {
        if((int)(Random.value * 100) <= trust - 30)
        {
            yeti = true;
        }
        
        if(yeti)
        {
            //UI place NPC response to yeti in UI
        }
        else
        {
            //UI place NPC response to yeti in UI
        }
    }
    
    public void TruePrediction()
    {
        
        if((int)(Random.value * 100) <= trust)
        {
            trustBuffer += trustModifier;
        }
        else
        {
            trustBuffer += (int)(trustModifier * .75);
        }
    }
    
    public void FalsePrediction()
    {
        if((int)(Random.value * 100) <= trust)
        {
            trustBuffer -= (int)(trustModifier * .75);
        }
        else
        {
            trustBuffer -= trustModifier;
        }
    }
    
    public void SmallTalk()
    {
        trust += 4;
        
        //grab other small data using the smallIndex
    }
}
