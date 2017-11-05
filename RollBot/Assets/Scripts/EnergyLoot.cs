using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLoot : Loot {

	public float energyAmount;
	public Transform energyLootTransform;
	public float pickupRadius;

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")){
			Player player = collision.gameObject.GetComponent<Player>();
			player.addEnergy(energyAmount);
			gameObject.SetActive(false);
		}
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
	}
}

