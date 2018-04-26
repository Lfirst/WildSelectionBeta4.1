using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public GameObject door; // the door which must be moved
	public int Yposition; // its initial position 
	private bool is_open = false;
	private bool upDoor = false;

	void Start(){
		door = this.door;
	}

	void OnTriggerStay(){
		if (is_open == false && Input.GetKeyDown ("e")) {
			is_open = true;
			upDoor = true;
		} else if (is_open && Input.GetKeyDown ("e")) {
			is_open = false;
			upDoor = false;
		}
	}

	void Update(){
		if(upDoor == false && door.transform.position.y > Yposition + 2)
			door.transform.position -= door.transform.up * Time.deltaTime;
		else if(upDoor && door.transform.position.y < Yposition + 6)
			door.transform.position += door.transform.up * Time.deltaTime;
			}
}
