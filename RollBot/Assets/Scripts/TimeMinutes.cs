using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMinutes : MonoBehaviour{
	public int minutes = 0;
	public Text minutesDisplay;

	void Awake(){
		minutesDisplay.text = minutes.ToString();
	}

	public void UpdateMinutes(){
		minutes++;
		minutesDisplay.text = minutes.ToString();
	}
}