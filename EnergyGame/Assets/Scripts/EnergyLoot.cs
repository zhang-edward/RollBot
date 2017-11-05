using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLoot : Loot {

	public float energyAmount;

		void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")){
			Player player = collision.gameObject.GetComponent<Player>();
			player.addEnergy(energyAmount);
			gameObject.SetActive(false);
		}
	}

}

