using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour {

    private Quaternion originalRotation;

    // Use this for initialization
    void Start() {
        originalRotation = this.GetComponent<Transform>().rotation;
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Transform>().Rotate(new Vector3(0, .8f * Time.deltaTime, 0));
	}

    public void Reset()
    {
        this.GetComponent<Transform>().rotation = originalRotation;
    }
}
