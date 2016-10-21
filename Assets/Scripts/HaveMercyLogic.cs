using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HaveMercyLogic : MonoBehaviour {

    public float gameTime;

    private bool gameIsRunning;
    public int collectedObjectives;
    public TimerClock timerClock;
    private Animator animator;

    // Use this for initialization
    public void Start ()
    {
        collectedObjectives = 0;
        gameIsRunning = false;
        timerClock = GetComponent<NetworkManager>().HUDCanvas.GetComponentInChildren<TimerClock>();
        animator = GetComponent<NetworkManager>().HUDCanvas.GetComponent<Animator>();
    }

    public void Update()
    {
        if (gameIsRunning && timerClock.gameTime <= 0)
            endGame();
    }

    public void startGame()
    {
        gameIsRunning = true;
        animator.SetTrigger("StartGame");
        timerClock.startTimerClock(gameTime);
    }

    public void endGame()
    {
        gameIsRunning = false;
        collectedObjectives = 0;
        timerClock.StopAllCoroutines();
        animator.SetTrigger("GameOver");
        updateGameOverText();
    }

	// get the status of the game is start or not for VR UI
	public bool isGameStart()
	{
		return gameIsRunning;
	}

    public void updateGameOverText()
    {
        string winText = "You Win!";
        string looseText = "You Loose!";

        if(timerClock.gameTime <= 0)
        {
            GameObject.Find("HMFPSCanvas").GetComponent<Text>().text = looseText;
            GameObject.FindGameObjectWithTag("Game Over Text").GetComponent<Text>().text = winText;
        } else
        {
            GameObject.Find("HMFPSCanvas").GetComponent<Text>().text = winText;
            GameObject.FindGameObjectWithTag("Game Over Text").GetComponent<Text>().text = looseText;
        }
    }
}
