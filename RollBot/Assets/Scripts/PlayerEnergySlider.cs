using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerEnergySlider : MonoBehaviour {

	public Slider slider;
	public Player player;

	void Start() {
		slider.maxValue = player.maxEnergy;
	}

	// Update is called once per frame
	void Update () {
		slider.value = player.energy;
	}
}
