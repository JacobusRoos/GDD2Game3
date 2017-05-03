using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dialogue dialogue;

    private int trust;

    private bool yeti;
	public bool stopped;

	public List<GameObject> goals;
	private float dist;
	private GameObject currentGoal;
	private int tracker;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dialogue();

        trust = 0;

		stopped = false;
		dist = 0;
		tracker = 0;
		currentGoal = goals [0];

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("test");

        dialogue.ParseDialogue("test");

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

	public void Interact(){
		stopped = !stopped;
	}
}
