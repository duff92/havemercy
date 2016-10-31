using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HaveMercyLogic : MonoBehaviour {

    public float gameTime;

    private bool gameIsRunning;
    public int collectedObjectives;
    public TimerClock timerClock;
    public int MaxWalls;
    private Animator animator;

    // Use this for initialization
    public void Start ()
    {
        GameObject.Find("FloorCollider").GetComponent<OnClickInstantiate>().WallAmount = MaxWalls;
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
        string VRWinText = "All objectives captured! Capture middle objective to restart game";
        string VRLooseText = "Time's up! Capture middle objective to restart game";
        string BVWinText = "Success!";
        string BVLooseText = "Try Again!";

        if(timerClock.gameTime <= 0)
        {
            GetComponent<NetworkManager>().HMFPSText.text = VRLooseText;
            GetComponent<NetworkManager>().HUDText.text = BVWinText;
        } else
        {
            GetComponent<NetworkManager>().HMFPSText.text = VRWinText;
            GetComponent<NetworkManager>().HUDText.text = BVLooseText;
        }
    }
}
