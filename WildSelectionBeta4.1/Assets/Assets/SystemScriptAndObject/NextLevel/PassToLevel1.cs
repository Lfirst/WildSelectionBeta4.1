using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassToLevel1 : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter()
	{
		SceneManager.LoadScene ("First_map_after_elevator_crash");
	}
}
