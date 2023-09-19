using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float initialVelocity = 0.0f;
    public float Bouncelevels = 0.8f; // Adjust this value for desired bounce behavior
    private float _vel;

    void Start()
    {
        _vel = initialVelocity; //_vel was just to satisfy vs
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        float acc = Physics.gravity.y;

        _vel = _vel + acc * dt;

        transform.position = new Vector3(transform.position.x, transform.position.y + _vel * dt, 
            transform.position.z);

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            _vel = -_vel * Bouncelevels;
        }
    }
}









