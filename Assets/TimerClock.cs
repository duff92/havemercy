using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TimerClock : MonoBehaviour {

    public float timeLeft = 20.0f + 4.0f;	// Seconds (2 minutes) + countdown seconds
	float minutes;
	float seconds;
	Text clockText;
    Animator anim;

    private PhotonView myPhotonView;

	// Use this for initialization
	void Start () {
        myPhotonView = gameObject.GetComponent<PhotonView>();
        anim = GetComponentInParent<Animator>();
        clockText = GetComponent<Text>();
        if (PhotonNetwork.isMasterClient)
        {    
            myPhotonView.RPC("startTimer", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
	}

    [PunRPC]
	public void startTimer(float from){
        Debug.Log("start Timer");
        timeLeft -= (float)(PhotonNetwork.time - from);

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            minutes = 0;
            seconds = 0;
        }
		StartCoroutine(updateCoroutine());
	}

	private IEnumerator updateCoroutine(){
		while(timeLeft > 0)
        {
			clockText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds(0.2f);
		}
        anim.SetTrigger("GameOver");
	}
}
