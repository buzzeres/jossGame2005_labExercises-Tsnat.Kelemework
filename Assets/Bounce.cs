using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float vel = 0.0f;

    // 60fps
    // 60frames / 1 second
    // 0.02 seconds, then we can convert to framerate by dividing 1 second by frequency
    // rate = 1 / frequency
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime; // 0.02s, updating at 50fps!
        float acc = Physics.gravity.y;    // -9.81

        vel = vel + acc * dt;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + vel * dt,
            transform.position.z
        );

        // Write code to do the following:
        // When the ball goes below the ground plane,
        // reset its position to zero and reset its velocity to the initial velocity
        // (if done correctly, the ball will bounce)
    }
}
