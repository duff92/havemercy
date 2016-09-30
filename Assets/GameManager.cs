using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static int numOfCollectedObjectives;

    public GameObject hudCanvas;

    TimerClock timerClock;
    Animator anim;
	// Use this for initialization
	void Start () {
        numOfCollectedObjectives = 0;
        timerClock = (TimerClock) hudCanvas.GetComponentInChildren<TimerClock>();
        anim = (Animator)hudCanvas.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (numOfCollectedObjectives == 1)
        {
            Debug.Log("Collected one objective");
            this.resetGame();
        }
	}

    public void resetGame()
    {
        numOfCollectedObjectives = 0;
        anim.SetTrigger("GameOver");
    }
}
