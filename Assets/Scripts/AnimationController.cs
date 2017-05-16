using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	Animator animator;
    Animation animation;
	int moveHash = Animator.StringToHash("Base Layer.Run");

	void Start ()
	{
		animator = GetComponent<Animator> ();
        animation = GetComponent<Animation>();
	}
	void Update(){
		float move = Input.GetAxis ("Vertical");

		animator.SetFloat ("Speed", move);

        if (Input.GetButton("Fire1"))
        {
            animator.SetTrigger("PlayerAttack");
        }

    }
}
