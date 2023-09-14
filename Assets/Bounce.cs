using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bounce : MonoBehaviour
{

    public float vel = 0.0f;
    public float damping = 0.9f;
    // Adjust the damping factor as needed
    private float dt;
    private float acc;
    void FixedUpdate()
    {
        dt = Time.fixedDeltaTime;
        acc = Physics.gravity.y;
        vel = vel + acc * dt;
        transform.position = new Vector3(transform.position.x, transform.position.y + vel * dt, transform.position.z);
        // Check if the ball is below the ground plane (assuming the ground is at y = 0)
        if (transform.position.y < 0.0f)
        {
            // Reset position to zero
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            // Apply damping to reduce bounce
            vel *= -damping; // Multiply by -damping to reduce the bounce each time
        }
    }
}

//using System.Collections; 
//using System.Collections.Generic;
//using UnityEngine; public class Bounce : MonoBehaviour

//{
//    public float initialVelocity = 0.0f;
//    private float vel = 0.0f;
//    private Vector3 initialPosition;


//    void Start()
//    {
//        initialPosition = transform.position;
//        vel = initialVelocity;

//    }
//    void FixedUpdate()
//    {
//        float dt = Time.fixedDeltaTime;
//        float acc = Physics.gravity.y;
//        vel = vel + acc * dt;

//        transform.position = new Vector3(transform.position.x, transform.position.y + vel * dt, transform.position.z);

//        if (transform.position.y < 0)

//        {
//            transform.position = initialPosition;
//            vel = initialVelocity;
//        }
//    }
//}

//using System.Collections; 
//using System.Collections.Generic; 
//using UnityEngine; public class Bounce : MonoBehaviour
//{

//    public float vel = 0.0f; 
//    public float damping = 0.9f; 
//// Adjust the damping factor as needed
//private float dt; 
//private float acc; 
//void FixedUpdate()
//{ 
//    dt = Time.fixedDeltaTime;
//    acc = Physics.gravity.y; 
//    vel = vel + acc * dt; 
//    transform.position = new Vector3( transform.position.x, transform.position.y + vel * dt, transform.position.z ); 
//// Check if the ball is below the ground plane (assuming the ground is at y = 0)
//if (transform.position.y < 0.0f)
//{ 
//    // Reset position to zero
//    transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
//    // Apply damping to reduce bounce
//    vel *= -damping; // Multiply by -damping to reduce the bounce each time
//                 }
//}
//}


