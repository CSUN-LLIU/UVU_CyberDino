﻿//Samantha Spray © 2014

using UnityEngine;
using System;
using System.Collections;

public class FlakCannonFire : MonoBehaviour 
{

	//This script needs to know when the fire button is being continuously pressed and when it has stopped being pressed.
	public static Action shooting;	
	public static Action stopping;
	
	public float fireTime = .5f;
	private bool firing = false;
	private bool onCoolDown = false;
	[SerializeField]
	private float coolDownDuration;
	[SerializeField]
	private int maxNumberOfUses;
	[SerializeField]
	private int numberOfUses;

	[SerializeField]
	private ParticleSystem cannonFire;
	//This will also need to play an animation

	public bool OnCoolDown{get{return onCoolDown;} set{onCoolDown = value;}}
	public float CoolDownDuration{get{return coolDownDuration;} set{coolDownDuration = value;}}
	public int MaxNumberOfUses{get{return maxNumberOfUses;} set{maxNumberOfUses = value;}}
	public int NumberOfUses{get{return numberOfUses;} set{numberOfUses = value;}}

	public ParticleSystem CannonFire{get{return cannonFire;}set{cannonFire = value;}}
	//This will also need to play an animation
	
	void OnEnable() 
	{
		CannonFire.Stop();
		FireButton.range += GunFire;
		NumberOfUses = MaxNumberOfUses;
		
	}
	
	void OnDisable() 
	{
		FireButton.range -= GunFire;
	}
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(shooting != null)
			{
				shooting();
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			if(stopping != null)
			{
				stopping();
			}
		}
	}
	
	public void GunFire()
	{
		if(NumberOfUses > 0)
		{
			if(OnCoolDown == false)
			{
				if(firing == false)
				{
					StartCoroutine(FireWeapon());
				}
			}
		}
	}
	
	public void StopFire()
	{
		if(OnCoolDown == false)
		{
			StartCoroutine(Cooldown());
		}
	}
	
	IEnumerator FireWeapon() 
	{
		firing = true;
		CannonFire.Play();
		Transform obj = FlakCannonPooling.current.GetPooledObject();
		
		if(obj == null) yield return null;
		obj.position = transform.position;
		obj.rotation = transform.rotation;
		obj.gameObject.SetActive(true);
		NumberOfUses--;
		yield return new WaitForSeconds(fireTime);
		CannonFire.Stop();
		firing = false;

	}

	IEnumerator Cooldown()
	{
		OnCoolDown = true;
		yield return new WaitForSeconds(CoolDownDuration);
		OnCoolDown = false;
	}

}
