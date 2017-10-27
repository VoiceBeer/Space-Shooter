using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	public Vector3 spawnValues;
	public int asteroidCount;
	public float spawnWait;
	public float startWait;

	public GUIText scoreText;
	public int score = 0;

	public GUIText endText;
	private bool gameEnded;

	// Use this for initialization
	void Start () {
		gameEnded = false;
		endText.gameObject.SetActive (false);
		scoreText.text = "Score: 0";
		StartCoroutine (SpawnWaves ());
	}

	// Update is called once per frame
	void Update () {
		if (gameEnded) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Scene level = SceneManager.GetActiveScene ();
				SceneManager.LoadScene (level.name);
			}
		}	
	}
	
	public void EndGame () {
		gameEnded = true;
		endText.gameObject.SetActive (true);
	}
					
			
	public void AddScore (int points) {
		score += points;
		scoreText.text = "Score: " + score;
	}

	IEnumerator SpawnWaves () {
		//Wait some time before spawning the first wave
		yield return new WaitForSeconds (startWait);

		//Endless spawn loop
		while (true) {
			for (int i = 0; i < asteroidCount; i++) {
				//Position to spawn an asteriod at. The position is a random point
				//at the X axis (between a min and max value) and the pre-set spawn
				//values for the Y and Z axis.
				Vector3 spawnAt = new Vector3 (
					                  Random.Range (-spawnValues.x, spawnValues.x),
					                  spawnValues.y,
					                  spawnValues.z);
				//Quaternion.identity equals no rotation. We deal with rotation in the
				//RandomRotator script.
				Instantiate (asteroid, spawnAt, Quaternion.identity);
				//Wait for next spawn
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}
		
}
