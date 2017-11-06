using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour {

	public Text enemiesText, scoreText, timeText;

	public void ShowScore(int enemies, int score, int time) {
		gameObject.SetActive(true);
		enemiesText.text = "Enemies: " + enemies.ToString();
		scoreText.text = "Score: " + score.ToString();
		timeText.text = "Time: " + time.ToString();
	}
}
