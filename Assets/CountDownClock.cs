using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownClock : MonoBehaviour
{

    float timeLeft = 4.0f;	// Seconds
    bool stop = true;

    int seconds;
    Text clockText;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponentInParent<Animator>();
        clockText = GetComponent<Text>();
        startTimer(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        timeLeft -= Time.deltaTime;

        seconds = (int) timeLeft % 60;
        if (seconds == 0)
        {
            anim.SetTrigger("StartGame");
        };
    }

    public float getTime()
    {
        return timeLeft;
    }

    public void startTimer(float from)
    {
        stop = false;
        timeLeft = from;
        Update();
        StartCoroutine(updateCoroutine());
    }

    private IEnumerator updateCoroutine()
    {
        while (!stop)
        {
            if (seconds == 0)
            {
                clockText.text = "GO!";
            }
            else
            {
                clockText.text = "" + seconds;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}
