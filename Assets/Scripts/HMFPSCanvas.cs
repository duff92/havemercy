using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HMFPSCanvas : MonoBehaviour {

	private const string DISPLAY_TEXT_FORMAT = "Time: {0}\nObjectives: {1}/{2}";
	public Text textField;
	private int numOfObjectives;
	private float timer;
	private string Timer;
	private bool startGame;

	void Awake(){
		textField = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {

		// put this canvas as a child of the vr player
		GameObject VRPlayer = GameObject.FindGameObjectWithTag("Player");
		if (VRPlayer != null)
			transform.SetParent(VRPlayer.GetComponent<Transform>(), true);

		// get the status of the game start or not 
		startGame = GameObject.FindGameObjectWithTag("_SCRIPTS").GetComponent<HaveMercyLogic>().isGameStart();

		if (!startGame) 
		{
			return;
		} 
		else 
		{

			// get the current objective index calculate the catched number
			ObjectiveHandler Objective = GameObject.FindGameObjectWithTag("objective").GetComponent<ObjectiveHandler>();
			numOfObjectives = 5 - Objective.getCurrentObjectIndex ();

			// get the time sync
			TimerClock TimerClock = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerClock>();
			timer = TimerClock.gameTime;
			Timer = string.Format("{0:0}:{1:00}", Mathf.Floor(timer / 60), (float)timer % 60);

			// display the text with the right format
			textField.text = string.Format(DISPLAY_TEXT_FORMAT,
				Timer, numOfObjectives, Objective.getNumberOfObjectives());
		}
	}
}
