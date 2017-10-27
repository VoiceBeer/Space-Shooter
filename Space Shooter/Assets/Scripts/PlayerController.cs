using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float tilt;

	// Shots
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody> ().velocity = movement * speed;

		//If position is outside boundary, set it to the boundary edge
		//Clamp: Clamps a number between a minimum and a maximum value.

		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax));

		// Tilt along the Z-axis (multiplied by a speed factor)
		// Use tilt * -1 to tilt in correct direction
		// Quaternions are used for rotations in Unity
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (
			0.0f,
			0.0f,
			GetComponent<Rigidbody> ().velocity.x * -tilt);
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
