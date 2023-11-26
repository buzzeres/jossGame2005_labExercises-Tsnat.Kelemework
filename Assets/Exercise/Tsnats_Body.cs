using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;  // Note: This using directive seems unnecessary
using UnityEngine;

public class Tsnats_Body : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float gravityScale = 1.0f;
    public bool isStatic = false; // A new property to fix the object in place
    public float frictionCoefficient = 0.1f;
    public float mass = 1.0f;
    public bool hasGravity = true; // New flag to control gravity
    public float damping = 0.10f; // Damping coefficient



    public float MassInverse
    {
        get { return 1 / mass; }
        private set { mass = 1 / value; }
    }

    private Vector3 forceNet = Vector3.zero;

    public Vector3 ForceNet
    {
        get { return forceNet; }
        set { forceNet = value; }
    }

    public void AddForce(Vector3 force)
    {
        ForceNet += force;
    }

    public void ResetForces()
    {
        ForceNet = Vector3.zero;
    }

    public TsnatsShape shape = null;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        // Assuming TsnatsShape is a class or component that represents the shape of the object
        shape = GetComponent<TsnatsShape>();
    }
}
