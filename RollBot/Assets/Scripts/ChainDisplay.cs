using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChainDisplay : MonoBehaviour{
	private int chainStatus;
	public Text chainDisplay;
	public Player player;

	void Awake(){
		chainStatus = (int) player.comboStatus;	
	}
	
	void Update(){
		chainDisplay.text = player.comboStatus.ToString();
	}
}