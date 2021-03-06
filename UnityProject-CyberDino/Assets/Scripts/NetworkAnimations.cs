﻿using UnityEngine;
using System.Collections;

public class NetworkAnimations : MonoBehaviour {

	int jumping = 0;
	int attacking = 0;
	int shooting = 0;

	public Animator anim;
	private NetworkView netView;

	void Start () {
		netView = GetComponent<NetworkView>();
		anim = GetComponent<Animator>();
	}

	public void AnimSetSpeed (string speed, float velocity, float topSpeed){
		netView.RPC ("AnimatorSpeed", RPCMode.All, speed, velocity, topSpeed);
	}

	public void AnimSetDirection (string dir, float horizontalDir){
		netView.RPC ("AnimatorDirection", RPCMode.All, dir, horizontalDir);
	}

	public void AnimSetJump (string jump, bool isJumping){
		if (isJumping == true) {
			jumping = 1;
			netView.RPC ("AnimatorJump", RPCMode.All, jump, jumping);
		}

		else {
			jumping = 0;
			netView.RPC ("AnimatorJump", RPCMode.All, jump, jumping);
		}
	}

	// added by Lee

	public void AnimSetMelee(string melee, bool isAttacking){
		if (isAttacking == true) {
			attacking = 1;
			netView.RPC ("AnimatorMelee", RPCMode.All, melee, attacking);
		}

		else{
			attacking = 0;
			netView.RPC ("AnimatorMelee", RPCMode.All, melee, attacking);
		}
	}

	[RPC]
	void AnimatorSpeed (string speed, float velocity, float topSpeed) {
		anim.SetFloat("Speed", velocity/topSpeed);
	}

	[RPC]
	void AnimatorDirection (string dir, float horizontalDir) {
		anim.SetFloat("Direction", horizontalDir);
	}

	[RPC]
	void AnimatorJump (string jump, int jumping) {
		if (jumping == 1) {
			anim.SetBool ("Jump", true);
		}
		else {
			anim.SetBool ("Jump", false);
		}
	}

	// Added by Lee
	[RPC]
	void AnimatorMelee (string melee, int isAttacking) {
		if(isAttacking == 1) {
			anim.SetBool (melee, true);
		}
		else{
			anim.SetBool (melee, false);
		}
	}
}
