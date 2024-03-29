﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;


	public Text restartText;
	public Text gameOverText;
	public Text scoreText;
	
	private int score;
	private bool restart;
	private bool gameOver;

	void Start()
	{
		score = 0;
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString();
	}
}
