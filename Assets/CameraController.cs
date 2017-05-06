using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
	public Vector3 targetPosition;
	public float smooth;
	public float distanceUp;
	public float distanceAway;
	public float cameraAngle;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	// Use this for initialization
	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		targetPosition = player.transform.position + player.transform.up * distanceUp - player.transform.forward * distanceAway;
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + cameraAngle, player.transform.position.z));
	}
}