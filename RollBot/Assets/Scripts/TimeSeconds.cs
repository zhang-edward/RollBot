using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSeconds : MonoBehaviour{
	public int seconds = 0;
	public Text secondsDisplay;

	public void UpdateSeconds(){
		seconds = (seconds + 1) % 10;
		secondsDisplay.text = seconds.ToString();
	}
}