using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeCooldown : MonoBehaviour
{

    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 20.0f;

    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float shakeDetectionThreshold = 2.0f;

    private float lowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 acceleration;
    private Vector3 deltaAcceleration;

    public bool isPhoneShaking;

    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        isPhoneShaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount
            cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (cooldown.fillAmount == 1)
            {
                coolingDown = false;
                isPhoneShaking = false;
            }
        }

        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && coolingDown == false)
        {
            // Debug.Log("Phone is shaking! - " + isPhoneShaking);
            resetCooldown();
            isPhoneShaking = true;
        }

    }

    public void resetCooldown()
    {
        cooldown.fillAmount = 0;
        coolingDown = true;
    }

}