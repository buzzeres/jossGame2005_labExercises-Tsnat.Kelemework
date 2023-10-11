using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThysicsBody : MonoBehaviour
{
    public enum CollisionShape { Sphere, Box }  // Define supported collision shapes

    public CollisionShape shape;
    public Vector3 dimensions;  // Dimensions for the collision shape

    // Add other properties and methods as needed
}
