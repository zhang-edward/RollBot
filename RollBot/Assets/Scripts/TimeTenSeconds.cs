using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTenSeconds : MonoBehaviour{
	public int tenSeconds = 0;
	public Text tenSecondsDisplay;

	void Awake(){
		tenSecondsDisplay.text = tenSeconds.ToString();
	}

	public void UpdateTenSeconds(){
		tenSeconds = (tenSeconds + 1) % 6;
		tenSecondsDisplay.text = tenSeconds.ToString();
	}

	public int GetTenSeconds(){
		return tenSeconds;
	}
}