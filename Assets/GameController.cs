using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private float delay = 0.1f;

	public float delayBeforeScaleReset = 0.5f;

	public float objectScaleTransitionTime = 0.75f;

	private float timer;

	public bool gameHasStarted = false;

	// Use this for initialization
	void Start () {

		gameHasStarted = false;

		timer = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > timer + delay) {

			gameHasStarted = true;
		}
	
	}
}
