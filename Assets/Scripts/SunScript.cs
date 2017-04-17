using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Transform>().Rotate(new Vector3(0, 10 * Time.deltaTime, 0));
	}
}
