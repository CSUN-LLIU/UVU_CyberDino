﻿//Samantha Spray © 2014

using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour 
{

	// Class Variables and Properties
	
	#region Fields
	// Respawn Variables
	private RacerRespawnStats racerRespawn;
	private RacerHealth racerHealth;
	private RespawnNode theRespawnNode;

	[SerializeField]
	private float thresholdAngle = 90.0F;
	
	// Variables for Respawning when racer is off the track.
	private float offTrackStartTime;
	[SerializeField]
	private float offTrackTimer = 2;
	private float offTrackRespawnTime;

	[SerializeField]
	private float heavyHealth = 125.0f;
	[SerializeField]
	private float standardHealth = 100.0f;
	[SerializeField]
	private float lightHealth = 75.0f;
	
	#endregion Fields
	
	#region Properties
	// Respawn Variables
	private RacerRespawnStats RacerRespawn{get{return racerRespawn;}set{racerRespawn = value;}}
	private RacerHealth RacerHealth{get{return racerHealth;}set{racerHealth = value;}}

	// Directional Respawn Variables
	private RespawnNode TheRespawnNode{get{return theRespawnNode;}set{theRespawnNode = value;}}
	private float ThresholdAngle{get{return thresholdAngle;}}

	// Variables for Respawning when racer is off the track.
	private float OffTrackStartTime { get{return offTrackStartTime;} set{offTrackStartTime = value;}}
	private float OffTrackTimer { get{return offTrackTimer;} set{offTrackTimer = value;}}
	private float OffTrackRespawnTime { get{return offTrackRespawnTime;} set{offTrackRespawnTime = value;}}


	public float HeavyHealth{get{return heavyHealth;} set{heavyHealth = value;}}
	public float StandardHealth{get{return standardHealth;} set{standardHealth = value;}}
	public float LightHealth{get{return lightHealth;} set{lightHealth = value;}}
	
	#endregion Properties

	void OnEnable() 
	{
		RacerRespawnStats.spawned += RacerStart;
		RacerInteractionManager.stayingOnSomething += OnTrack;
		RacerInteractionManager.notStayingOnSomething += OffTrack;
		RacerInteractionManager.hitSomething += RespawnNodeSwitch;

		RacerRespawnStats.spawned += RacerStart;
		RacerHealth.died += DeadRacer;
	}
	
	void OnDisable() 
	{
		RacerInteractionManager.stayingOnSomething -= OnTrack;
		RacerInteractionManager.notStayingOnSomething -= OffTrack;
		RacerInteractionManager.hitSomething -= RespawnNodeSwitch;

		RacerRespawnStats.spawned -= RacerStart;

		RacerHealth.died -= DeadRacer;
	}

	void OnTrack(Transform other)
	{
		if(other.gameObject.tag == "Track")
		{

		}
	}

	void OffTrack(Transform other)
	{
		if(other.gameObject.tag == "Track")
		{

		}
	}


	// Methods

	public void RacerStart(Transform player)
	{
		RacerRespawn = player.gameObject.GetComponent<RacerRespawnStats>();
		RacerHealth = player.gameObject.GetComponent<RacerHealth>();

		RacerRespawn.ThresholdAngle = ThresholdAngle;

		RacerRespawn.RespawnCheckpoint = GameObject.Find("RespawnPoint_001").transform;
		TheRespawnNode = RacerRespawn.RespawnCheckpoint.gameObject.GetComponent<RespawnNode>();
		RacerRespawn.CurrentNode = RacerRespawn.RespawnCheckpoint;
		RacerRespawn.PreviousNode = TheRespawnNode.PreviousNode;
		RacerRespawn.NextNode = TheRespawnNode.NextNode;

		switch(RacerHealth.theSize)
		{
		case RacerHealth.DinoSize.Heavy:
			RacerHealth.TotalHealth = HeavyHealth;
			break;
		case RacerHealth.DinoSize.Standard:
			RacerHealth.TotalHealth = StandardHealth;
			break;
		case RacerHealth.DinoSize.Light:
			RacerHealth.TotalHealth = LightHealth;
			break;
		default:
			RacerHealth.TotalHealth = StandardHealth;
			break;
		}
		RacerHealth.CurrentHealth = RacerHealth.TotalHealth;
		
	}

	public void DeadRacer(Transform player)
	{
		RacerHealth = player.gameObject.GetComponent<RacerHealth>();
		RacerRespawn = player.gameObject.GetComponent<RacerRespawnStats>();

		RacerHealth.CurrentHealth = RacerHealth.TotalHealth;

		UseRespawn(player, RacerRespawn);
	}

	
//	public float RespawnNodeAngle()
//	{
//		
//		Vector3 targetDir = CurrentNode.transform.position - transform.position;
//		Vector3 forward = transform.forward;
//		return Vector3.Angle(targetDir, forward);
//	}
	
	public void RespawnNodeSwitch(Transform player, Transform other)
	{
		if(other.gameObject.tag == "RespawnPoint")
		{
			RacerRespawn = player.gameObject.GetComponent<RacerRespawnStats>();
			RacerRespawn.RespawnCheckpoint = other;

			TheRespawnNode = RacerRespawn.RespawnCheckpoint.gameObject.GetComponent<RespawnNode>();

			RacerRespawn.CurrentNode = RacerRespawn.RespawnCheckpoint;
			RacerRespawn.PreviousNode = TheRespawnNode.PreviousNode;
			RacerRespawn.NextNode = TheRespawnNode.NextNode;

		}
	}
	
	public void UseRespawn(Transform racer, RacerRespawnStats respawn)
	{
		StartCoroutine(Respawn(racer, respawn));
	}
	
	IEnumerator Respawn(Transform player, RacerRespawnStats respawn)
	{

		Debug.Log("Respawning. Please wait....");
//		IsRespawning = true;
//		Move.SetRunning(false);
//		if(IsDead)
//		{
//			if(!IsRagdoll)
//			{
//				TheRagdoll.GoRagdoll();
//				IsRagdoll = true;
//			}
//		}
//		RespawnEffect.Play();
		yield return new WaitForSeconds(1);
		Debug.Log("Respawning. Please wait....");

		player.position = respawn.PreviousNode.position;
		player.rotation = respawn.PreviousNode.rotation;
		
//		RespawnEffect.Play();
		yield return new WaitForSeconds(1);
		Debug.Log("Ready to go!");
//		RespawnEffect.Stop();
//		IsRespawning = false;
//		Move.SetRunning(true);
//		yield return null;
	}
}