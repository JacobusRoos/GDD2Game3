using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 startLocation;
    
    private Vector3 startForward;

    public bool canMove;
    
	// Use this for initialization
	void Start () {
        canMove = false;

        startLocation = this.GetComponent<Transform>().position;
        startForward = this.GetComponent<Transform>().forward;
	}
	
    public void Reset()
    {
        this.GetComponent<Transform>().position = startLocation;
        this.GetComponent<Transform>().forward = startForward;
    }
    
	// Update is called once per frame
	void Update () {
        //Debug.Log(canMove);

        if(canMove)
        {
            if (Input.GetKey("a"))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * -30, Space.World);
            }
            else if (Input.GetKey("d"))
            {
                this.transform.Rotate(Vector3.up * Time.deltaTime * 30, Space.World);
            }

            if (Input.GetKey("w"))
            {
                this.transform.position += this.transform.forward * Time.deltaTime * 5;
            }
            else if (Input.GetKey("s"))
            {
                this.transform.position -= this.transform.forward * Time.deltaTime * 5;
            }
        }
	}
}
