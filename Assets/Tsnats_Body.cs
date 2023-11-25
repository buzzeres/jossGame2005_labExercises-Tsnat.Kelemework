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

    private Vector3 forceNet = Vector3.zero;

    public Vector3 ForceNet 
    {
        get 
        {
            return forceNet; 
        } 
        set 
        {  
            forceNet = value; 
        } 
    }

    public TsnatsShape shape = null;
    public void Awake()
    {
        shape = GetComponent<TsnatsShape>();
    }
}
