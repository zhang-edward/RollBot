using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour{
	public static int enemiesKilled = 0;
	public static float timeAlive = 0;
	public static float totalScore = 0;
	public TimeSeconds seconds;
	public TimeMinutes minutes;
	public TimeTenSeconds tenSeconds;

	public static void UpdateEnemiesKilled(){
		enemiesKilled++;
	}

	public static void AddTime(){
		timeAlive++;
	}

	public static float GetTime(){
		return timeAlive;
	}

	public IEnumerator TotalTimer(){
		for(;;){
			Statistics.AddTime();
			seconds.UpdateSeconds();
			if(GetTime() % ((int) 10) == 0)
				tenSeconds.UpdateTenSeconds();
			if(GetTime() % ((int) 60) == 0)
				minutes.UpdateMinutes();
			yield return new WaitForSeconds(1f);
		}

	}
}

