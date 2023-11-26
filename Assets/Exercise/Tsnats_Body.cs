using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tsnats_Body : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float gravityScale = 1.0f;
    public float AirFriction = 0.1f;
    public TsnatsShape shape = null;
    public bool isStatic = false; // A new property to fix the object in place
    public float mass = 1.0f;


    public void Awake()
    {
        shape = GetComponent<TsnatsShape>();
    }
}