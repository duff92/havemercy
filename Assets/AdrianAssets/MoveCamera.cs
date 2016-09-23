using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    public float speed = 10.0F;
    public float threshhold;

    void Update()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        if (dir.sqrMagnitude > threshhold)
        {
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            dir *= Time.deltaTime;
            transform.Translate(dir * speed);
        }
        //clamp camera position so it can't go outside the arena limits
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12.6f, 12.6f), transform.position.y, Mathf.Clamp(transform.position.z, -14f, 14f));
    }
}
