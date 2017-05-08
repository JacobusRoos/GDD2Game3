using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    private int trust;

    private bool yeti;
    
    private bool[] predictionStates;
    
    private bool[] predictionsUsed;
    
    private bool[] smallUsed;
    
    private int givenPredictionNum;
    
    public int smallIndex;
    public int trueIndex;
    public int falseIndex;
    
    private int trustBuffer;
    
    const int trustModifier = 15;
    
    
	public bool stopped;
    
	public List<GameObject> goals;
	private float dist;
	private GameObject currentGoal;
	private int tracker;

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
        falseIndex = -1;
        
        
		stopped = false;
		dist = 0;
		tracker = 0;
		currentGoal = goals[0];
        
        dialogue.ParseDialogue(NPCName);
        
        RandomizePredictions();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("test");

        

		if(!stopped){
			this.transform.LookAt(currentGoal.transform.position);

			//this.transform.position += this.transform.forward/10;
			this.transform.position -= (this.transform.position - currentGoal.transform.position).normalized/8;

			dist = Vector3.Distance (currentGoal.transform.position, this.transform.position);

			if (dist <= 8.0f) {
				//int num = Random.Range (0, goals.Count);
				tracker++;
				if (tracker >= goals.Count)
					tracker =0;
				currentGoal = goals [tracker];
				this.transform.LookAt(currentGoal.transform.position);
			}
		}
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
        
        while((!trueFound || !falseFound) && givenPredictionNum < 3)
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
                falseIndex = r;
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
        
        if(givenPredictionNum >= 3)
        {
            
        }
        else
        {
            
        }
        
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
    
	public void Interact(){
		stopped = !stopped;
	}
}
