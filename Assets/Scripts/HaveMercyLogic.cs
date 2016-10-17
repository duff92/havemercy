using UnityEngine;
using System.Collections;

public class HaveMercyLogic : MonoBehaviour {

    public float gameTime;

    private bool gameIsRunning = false;
    public int collectedObjectives;
    public TimerClock timerClock;
    private Animator animator;

    // Use this for initialization
    public void Start ()
    {
        collectedObjectives = 0;
        timerClock = GetComponent<NetworkManager>().HUDCanvas.GetComponentInChildren<TimerClock>();
        animator = GetComponent<NetworkManager>().HUDCanvas.GetComponent<Animator>();
    }

    public void Update()
    {
        if (gameIsRunning && timerClock.gameTime <= 0) { 
            endGame();
            gameIsRunning = false;
        }
    }

    public void startGame()
    {
        animator.SetTrigger("StartGame");
        timerClock.startTimerClock(gameTime);
        gameIsRunning = true;
    }

    public void endGame()
    {
        collectedObjectives = 0;
        timerClock.StopAllCoroutines();
        animator.SetTrigger("GameOver");
    }

	// get the status of the game is start or not for VR UI
	public bool isGameStart()
	{
		return gameIsRunning;
	}
}
