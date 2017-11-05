using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerComboSlider : MonoBehaviour {

	public Slider slider;
	public Player player;

	void Start() {
		slider.maxValue = player.maxCombo;
	}

	// Update is called once per frame
	void Update () {
		slider.value = player.combo;
	}
}
