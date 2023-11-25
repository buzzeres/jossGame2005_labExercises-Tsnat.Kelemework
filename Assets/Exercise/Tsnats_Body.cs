using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tsnats_Body : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float gravityScale = 1.0f;
    public bool isStatic = false; // A new property to fix the object in place
    public float mass = 1.0f;

    public float MassInverse
    {
        get { return 1 / mass; }
        private set { mass = 1 / value; }
    }
    
private Vector3 forceNet = Vector3.zero;

    public Vector3 ForceNet
    {
        get
        {return forceNet;}
        set
        {forceNet = value; }
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
    public void Awake()
    {
        shape = GetComponent<TsnatsShape>();
    }
}