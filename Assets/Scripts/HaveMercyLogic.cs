using UnityEngine;
using System.Collections;

public class HaveMercyLogic : MonoBehaviour {

    public static int numOfCollectedObjectives;

    public GameObject hudCanvas;
    public int objectivesToCollect = 6; 

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
        if (numOfCollectedObjectives == objectivesToCollect)
        {
            Debug.Log("Collected all objectives");
            this.resetGame();
        }
	}

    public void resetGame()
    {
        numOfCollectedObjectives = 0;
        timerClock.StopAllCoroutines();
        anim.SetTrigger("GameOver");
    }
}
