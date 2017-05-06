using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	Animator animation;
	int moveHash = Animator.StringToHash("Base Layer.Run");
	void Start ()
	{
		animation = GetComponent<Animator> ();
	}
	void Update(){
		float move = Input.GetAxis ("Vertical");
		animation.SetFloat ("Speed", move);
	}
}
