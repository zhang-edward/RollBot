using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour{
	public int enemiesKilled = 0;
	public float timeAlive = 0;
	public float totalScore = 0;
	public TimeSeconds seconds;
	public TimeMinutes minutes;
	public TimeTenSeconds tenSeconds;

	public void UpdateEnemiesKilled(){
		enemiesKilled++;
	}

	public void AddTime(){
		timeAlive++;
	}

	public float GetTime(){
		return timeAlive;
	}

	public IEnumerator TotalTimer(){
		for(;;){
			AddTime();
			seconds.UpdateSeconds();
			if(GetTime() % ((int) 10) == 0)
				tenSeconds.UpdateTenSeconds();
			if(GetTime() % ((int) 60) == 0)
				minutes.UpdateMinutes();
			yield return new WaitForSeconds(1f);
		}
	}
}

