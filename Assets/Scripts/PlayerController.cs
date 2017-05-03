using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("left")) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * -30, Space.World);
		}
		else if (Input.GetKey ("right")) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * 30, Space.World);
		}

		if (Input.GetKey ("up")) {
			this.transform.position += this.transform.forward * Time.deltaTime* 5;
		}
		else if (Input.GetKey ("down")) {
			this.transform.position -= this.transform.forward * Time.deltaTime* 5;
		}
	}
}
