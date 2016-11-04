using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCooldown : MonoBehaviour {

    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 10.0f;
    private Button btn;

    void Start()
    {
        btn = GameObject.Find("FakeObjectiveButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //Fill amount over time
            cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
            if (cooldown.fillAmount == 1)
            {
                coolingDown = false;
                btn.interactable = true;
            }
        }
    }

    public void resetCooldown()
    {
        cooldown.fillAmount = 0;
        coolingDown = true;
        btn.interactable = false;
    }
}
