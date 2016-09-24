using UnityEngine;
using System.Collections;

public class TimerClock : MonoBehaviour {

	float timeLeft = 120.0f;	// Seconds (2 minutes)
	bool stop = true;
	float minutes;
	float seconds;
	string text;

	GUIStyle myStyle;

	public UnityEngine.Font myFont;

	// Use this for initialization
	void Start () {
		startTimer (timeLeft);
	}
	
	// Update is called once per frame
	void Update () {
		if(stop) return;
		timeLeft -= Time.deltaTime;

		minutes = Mathf.Floor(timeLeft / 60);
		seconds = timeLeft % 60;
		if(seconds > 59) seconds = 59;
		if(minutes < 0) {
			stop = true;
			minutes = 0;
			seconds = 0;
		}
		//        fraction = (timeLeft * 100) % 100;
	}

	public void startTimer(float from){
		stop = false;
		timeLeft = from;
		Update();
		StartCoroutine(updateCoroutine());
	}

	private IEnumerator updateCoroutine(){
		while(!stop){
			text = string.Format("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds(0.2f);
		}
	}

	void OnGUI() {

		myStyle = new GUIStyle (GUI.skin.GetStyle("label"));
		myStyle.font = myFont;
		myStyle.fontSize = 26;
		GUI.Label (new Rect (25, 25, Screen.width / 4, Screen.height / 4), text.ToString(), myStyle);


		if (minutes == 0 && seconds == 0) {
			GUI.Label(new Rect (Screen.width-100, 25, Screen.width / 4, Screen.height / 4), "Game\nOver!", myStyle);
		}
		/* GUIStyle style = new GUIStyle();
		GUI.skin.font = myFont;
		style.font = myFont;
		GUI.Label (new Rect (100, 100, 200, 200), "12:00", style); */
	}
}
