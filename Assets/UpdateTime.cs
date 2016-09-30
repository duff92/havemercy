using UnityEngine;
using System.Collections;

public class UpdateTime : MonoBehaviour
{

    Animator anim;

    TimerClock script;

    // Use this for initialization
    void Start()
    {

        script = GetComponent<TimerClock>();
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("15"))
            script.timeLeft = 10.0f;
    }
}
