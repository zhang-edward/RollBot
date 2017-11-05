using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLoot : Loot {

	public float energyAmount;
	public Transform energyLootTransform;
	public float pickupRadius;
	private float timeAlive;

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")){
			Player player = collision.gameObject.GetComponent<Player>();
			player.AddEnergy(energyAmount);
			gameObject.SetActive(false);
		}
	}

	void OnEnable(){
		timeAlive = 0;
	}

	void Update(){
		Transform playerTransform = GameManager.instance.player;
		Vector3 playerPos = playerTransform.position;
		Vector3 energyPos = energyLootTransform.position;
		Vector3 distance = playerPos - energyPos;
		if(distance.magnitude <= pickupRadius){
			energyPos = Vector3.Lerp(energyPos, playerPos, 0.5f); 
			energyLootTransform.position = energyPos;
		}

		timeAlive += Time.deltaTime;
		if(timeAlive >= 10){
			gameObject.SetActive(false);
		}
	}
}

