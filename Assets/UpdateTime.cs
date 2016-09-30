using UnityEngine;
using System.Collections;

public class UpdateTime : MonoBehaviour
{

    Animator anim;
    public float ResetTimer = 120.0f;
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
            script.timeLeft = ResetTimer;
    }
}
