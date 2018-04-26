using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject objet;
	public Transform pos;
	// Use this for initialization
	void Start () {
		Instantiate(objet, pos.position, pos.rotation);
	}
}
