using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string NPCName;

    private Dictionary<string, KeyValuePair<string, KeyValuePair<string, string>>> dialogue;

    private int trust;

    private bool yeti;

	// Use this for initialization
	void Start () {
        NPCName = "";

        dialogue = new Dictionary<string, KeyValuePair<string, KeyValuePair<string, string>>>();

        trust = 0;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
