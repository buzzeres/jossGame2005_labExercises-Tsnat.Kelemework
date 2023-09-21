
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float launchVelocity;
    public float launchAngle;
    public float launchHeight;
    private float velX;
    private float velY;

    void Start()
    {
        LaunchBall();
    }

    void LaunchBall()
    {
        Debug.Log("Launch!");
        // Calculate the initial velocities based on launch parameters
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        velX = launchVelocity * Mathf.Cos(launchAngleRad);
        velY = launchVelocity * Mathf.Sin(launchAngleRad);
        // Set the initial position
        transform.position = new Vector3(0, launchHeight, transform.position.z);
    }

    void Update()
    {
        // Restart the launch if the Space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        float acc = Physics.gravity.y;

        // Update velocity using acceleration over time (velX remains constant)
        velY = velY + acc * dt;

        // Update position using velocity over time
        transform.position = new Vector3(
            transform.position.x + velX * dt,
            transform.position.y + velY * dt,
            transform.position.z
        );
    }
}

