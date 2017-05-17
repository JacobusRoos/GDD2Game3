using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    public int trust;

    public bool yeti;
    private bool yetiAttempt;
    
    private bool[] predictionStates;
    
    private bool[] predictionsUsed;
    
    private bool[] smallUsed;
    
    private int givenPredictionNum;

    private int givenSmallNum;

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

    public GameObject textOptionsGroup;
    public GameObject playStateGroup;

    public GameObject gameManager;

    private Vector3 loc;
    private Vector3 rot;

    private Vector3 startLoc;

    // Use this for initialization
    void Start () {

        startLoc = this.transform.localPosition;

        dialogue = new Dialogue();

        dialogue.ParseDialogue(NPCName);

        trust = 20 + (int)(Random.value * 30) ;

        predictionStates = new bool[dialogue.predNum];
        
        predictionsUsed = new bool[dialogue.predNum];
        
        smallUsed = new bool[dialogue.smallNum];
        
        givenPredictionNum = 0;
        givenSmallNum = 0;
        
        trueIndex = -1;
        falseIndex = -1;
        
        
		stopped = true;
		dist = 0;
		tracker = 0;
		currentGoal = goals[0];

        yetiAttempt = false;

        loc = this.transform.localPosition;
        rot = this.transform.localEulerAngles;

        

        RandomizePredictions();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(stopped);

        

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
        else
        {
            this.transform.localPosition = loc;
            this.transform.localEulerAngles = rot;
        }

	}

    
    public void RandomizePredictions()
    {
        for(int i = 0; i < dialogue.predNum; i++)
        {
            predictionStates[i] = false;
            predictionsUsed[i] = false;
        }
        
        for(int i = 0; i < smallUsed.Length; i++)
        {
            smallUsed[i] = false;
        }

        bool[] assigned = new bool[dialogue.predNum];
        

        for(int i = 0; i < dialogue.predNum / 2; i++)
        {
            int index = (int)((Random.value * 1000) % dialogue.predNum);
            
            if(assigned[index])
            {
                i--;
            }
            else
            {
                predictionStates[index] = true;
            }
        }

        for(int i = 0; i < predictionStates.Length; i++)
        {
            //Debug.Log(predictionStates[i]);
        }

        //Debug.Log("");
    }
    
    public void DisplayDialogueOptions()
    {
        bool trueFound = false;
        bool falseFound = false;
        bool smallFound = false;

        int r = 0;
        
        while((!trueFound || !falseFound) && givenPredictionNum < 1)
        {
            r = (int)((Random.value * 1000) % dialogue.predNum);

            //Debug.Log(r);

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

        //Debug.Log(smallUsed.Length);
        //Debug.Log(dialogue.smallNum);


        while(!smallFound && givenSmallNum < dialogue.smallNum)
        {
            r = (int)((Random.value * 1000) % dialogue.smallNum);
        
            if(!smallUsed[r])
            {
                smallIndex = r;
                smallFound = true;
                
            }
        }

        //Todo: put dialogue in the UI here

        if (givenPredictionNum >= 1)
        {
            textOptionsGroup.transform.GetChild(0).gameObject.SetActive(false);
            textOptionsGroup.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            textOptionsGroup.transform.GetChild(0).gameObject.SetActive(true);
            textOptionsGroup.transform.GetChild(1).gameObject.SetActive(true);

            textOptionsGroup.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Truth: " + dialogue.convDictionary["P" + trueIndex.ToString()].Key;
            textOptionsGroup.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Lie: " + dialogue.convDictionary["P" + falseIndex.ToString()].Key;
            textOptionsGroup.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["Y" + 0].Key;
        }

        if(givenSmallNum == dialogue.smallNum)
        {
            textOptionsGroup.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            textOptionsGroup.transform.GetChild(2).gameObject.SetActive(true);
            textOptionsGroup.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["S" + smallIndex.ToString()].Key;
        }

        if(yeti || yetiAttempt)
        {
            textOptionsGroup.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            textOptionsGroup.transform.GetChild(3).gameObject.SetActive(true);
            textOptionsGroup.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["Y" + 0].Key;
        }
        
        if((yeti || yetiAttempt) && givenSmallNum == dialogue.smallNum && givenPredictionNum >= 1)
        {
            gameManager.GetComponent<GameManager>().CloseDialogue();
        }
        else
        {
            gameManager.GetComponent<GameManager>().DisplayDialogue();
        }
    }
    
    public void NextDay()
    {
        RandomizePredictions();
        
        smallUsed = new bool[dialogue.smallNum];
        
        givenPredictionNum = 0;
        
        trust += trustBuffer;
        
        trustBuffer = 0;

        this.transform.localPosition = startLoc;

        stopped = true;

        tracker = 0;

        currentGoal = goals[tracker];
    }
    
    public void YetiPrediction()
    {
        if((int)(Random.value * 100) <= trust - 30)
        {
            yeti = true;
        }
        
        if(yeti)
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["Y" + 0].Value.Key;
        }
        else
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["Y" + 0].Value.Value;
        }

        yetiAttempt = true;
    }
    
    public void TruePrediction()
    {
        bool state;
        if ((int)(Random.value * 100) <= trust)
        {
            trustBuffer += trustModifier;
            state = true;
        }
        else
        {
            trustBuffer += (int)(trustModifier * .75);
            state = false;
        }

        if (state)
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["P" + trueIndex.ToString()].Value.Key;
        }
        else
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["P" + trueIndex.ToString()].Value.Value;
        }

        predictionsUsed[trueIndex] = true;

        givenPredictionNum++;
    }


    
    public void FalsePrediction()
    {
        bool state;
        if((int)(Random.value * 100) <= trust)
        {
            trustBuffer -= (int)(trustModifier * .75);
            state = true;
        }
        else
        {
            trustBuffer -= trustModifier;
            state = false;
        }

        if(state)
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["P" + falseIndex.ToString()].Value.Key;
        }
        else
        {
            playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["P" + falseIndex.ToString()].Value.Value;
        }

        predictionsUsed[falseIndex] = true;

        givenPredictionNum++;
    }
    
    public void SmallTalk()
    {
        trust += 4;

        //grab other small data using the smallIndex
        playStateGroup.transform.GetChild(0).GetComponent<Text>().text = dialogue.convDictionary["S" + smallIndex.ToString()].Value.Key;

        smallUsed[smallIndex] = true;

        smallUsed[smallIndex] = true;

        givenSmallNum++;
    }
    

	public void Interact(){
        loc = this.transform.localPosition;
        rot = this.transform.localEulerAngles;

        stopped = !stopped;
	}
}
