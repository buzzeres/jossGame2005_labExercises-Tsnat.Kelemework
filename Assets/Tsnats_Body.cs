using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tsnats_Body : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float gravityScale = 1.0f;
    public float AirFriction = 0.1f;
    public ThysicsBody ThysicsBody;  // Reference to the physics body

}
