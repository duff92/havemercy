using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TimerClock : MonoBehaviour {

    public float timeLeft = 20.0f + 4.0f;	// Seconds (2 minutes) + countdown seconds
	float minutes = 0.0f;
	float seconds;
    bool stop = true;
	Text clockText;
    Animator anim;

    private PhotonView myPhotonView;

	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
        clockText = GetComponent<Text>();
        
        Debug.Log("Is master client" + PhotonNetwork.isMasterClient); 
        if(PhotonNetwork.isMasterClient)
            this.GetComponent<PhotonView>().RPC("startTimer", PhotonTargets.AllBuffered, PhotonNetwork.time);
	}
   

    [PunRPC]
	public void startTimer(double from){
        Debug.Log("start Timer");
        Debug.Log("Time: " + PhotonNetwork.time);
        timeLeft -= (float)(PhotonNetwork.time - from);

        StartCoroutine("updateCoroutine");
    }

	private IEnumerator updateCoroutine(){
		while(timeLeft > 0)
        {
            Debug.Log("Time left: " + timeLeft);
            clockText.text = string.Format("{0:0}:{1:00}", minutes, (float) timeLeft % 60);
            //yield return new WaitForSeconds(0.2f);
            yield return new WaitForEndOfFrame();
            timeLeft -= Time.deltaTime;
        }
        anim.SetTrigger("GameOver");
	}
}
