using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerClock : MonoBehaviour
{

    public float gameTime;
    private float minutes = 0.0f;
    private float seconds;
    private bool stop = true;
    private Text clockText;

    // Use this for initialization
    void Start()
    {
		Time.timeScale = 1.0f;
        clockText = GetComponent<Text>();
    }

    public void startTimerClock(float timeLimit)
    {
        gameTime = timeLimit;
        if(PhotonNetwork.isMasterClient)
            this.GetComponent<PhotonView>().RPC("startTimer", PhotonTargets.All, PhotonNetwork.time);
    }

    [PunRPC]
    public void startTimer(double from)
    {
        gameTime -= (float)(PhotonNetwork.time - from);

        StartCoroutine("updateCoroutine");
    }

    private IEnumerator updateCoroutine()
    {
        while (gameTime > 0)
        {
            if(clockText != null)
                clockText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(gameTime / 60), (float)gameTime % 60);
            //yield return new WaitForSeconds(0.2f);
            yield return new WaitForEndOfFrame();
            gameTime -= Time.deltaTime;
        }
    }
}