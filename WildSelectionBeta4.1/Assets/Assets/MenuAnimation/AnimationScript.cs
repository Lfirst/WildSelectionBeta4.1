using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

	public Animator anim;
	int Breath = Animator.StringToHash("Mutant Breathing Idle");
	int Walk = Animator.StringToHash("Orc Walk");
	int turn = Animator.StringToHash("Left Turn 45");


	void Start ()
	{
		anim = GetComponent<Animator>();
	}


	void Update ()
	{
		anim.SetTrigger (Breath);
	}
}
