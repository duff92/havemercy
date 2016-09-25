using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TimerClock : MonoBehaviour {

    public float timeLeft = 20.0f + 4.0f;	// Seconds (2 minutes) + countdown seconds
	float minutes;
	float seconds;
    bool stop = true;
	Text clockText;
    Animator anim;

    private PhotonView myPhotonView;

	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
        clockText = GetComponent<Text>();
        startTimer(timeLeft);
	}

    void Update()
    {
        if (stop) return;
        timeLeft -= Time.deltaTime;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            stop = true;
            minutes = 0;
            seconds = 0;
        }
    }

    [PunRPC]
	public void startTimer(float from){
        Debug.Log("start Timer");
        stop = false;
        timeLeft = from;
        Update();
		StartCoroutine(updateCoroutine());
	}

	private IEnumerator updateCoroutine(){
		while(timeLeft > 0)
        {
            Debug.Log("seconds " + seconds);
			clockText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds(0.2f);
		}
        anim.SetTrigger("GameOver");
	}
}
