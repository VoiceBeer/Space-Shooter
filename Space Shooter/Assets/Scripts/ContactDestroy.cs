using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDestroy : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController controller;

	void Start () {
		GameObject tmp = GameObject.FindGameObjectWithTag ("GameController");
		controller = tmp.GetComponent<GameController> ();
		if (controller == null) {
			Debug.LogError ("Unable to find the GameController script");
		}
	}

	void OnTriggerEnter (Collider other) {
		//Check if other is the Boundary object
		if (other.tag == "Boundary") {
			//If so, do nothing
			return;
		}

		if (other.tag != "Player") {
			controller.AddScore (10);
		}

		//Destroy the collision object, i.e. the Bolt
		Destroy (other.gameObject);
		//Destroy the asteroid itself
		Destroy (gameObject);

		//Instantiate the Explosion game object at the
		//same position as the Asteroid object
		GameObject tmp = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		Destroy (tmp, 1);

		if (other.tag == "Player") {
			//If the other object is the ship, instantiate an
			//explosion at the same position as the Player object.
			tmp = Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			Destroy (tmp, 1);
			//Notify the GameController that the game is over
			controller.EndGame();
		}
	}
}
