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
            GetComponent<NetworkManager>().HMFPSCanvas.GetComponent<Text>().text = looseText;
            GetComponent<NetworkManager>().HUDCanvas.GetComponentInChildren<Text>(true).text = winText;
        } else
        {
            GetComponent<NetworkManager>().HMFPSCanvas.GetComponent<Text>().text = winText;
            GetComponent<NetworkManager>().HUDCanvas.GetComponentInChildren<Text>(true).text = looseText;
        }
    }
}
