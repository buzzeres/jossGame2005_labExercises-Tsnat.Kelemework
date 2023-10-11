using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TollisionManager : MonoBehaviour
{
    private List<Tsnats_Body> bodies;  // List to store Tsnats_Body objects with physics bodies
    private List<Tsnats_Body> spheres;  // List to store Tsnats_Body objects with sphere physics bodies

    void Start()
    {
        bodies = new List<Tsnats_Body>();
        spheres = new List<Tsnats_Body>();
    }

    // Register a Tsnats_Body with the CollisionManager
    public void RegisterBody(Tsnats_Body body)
    {
        bodies.Add(body);

        // Check if the body has a sphere collision shape and add it to the spheres list
        if (body.ThysicsBody.shape == ThysicsBody.CollisionShape.Sphere)
        {
            spheres.Add(body);
        }
    }

    void Update()
    {
        // Perform Sphere-Sphere collision detection (no collision response implemented)

        // Iterate through the list of bodies with sphere collision shapes
        for (int i = 0; i < spheres.Count; i++)
        {
            for (int j = i + 1; j < spheres.Count; j++)
            {
                Tsnats_Body bodyA = spheres[i];
                Tsnats_Body bodyB = spheres[j];

                // Calculate the distance between the centers of the two spheres
                float distance = Vector3.Distance(bodyA.transform.position, bodyB.transform.position);

                // Get the radii of the two spheres
                float radiusA = bodyA.ThysicsBody.dimensions.x;
                float radiusB = bodyB.ThysicsBody.dimensions.x;

                // Check for collision by comparing the sum of radii with the distance
                if (distance < radiusA + radiusB)
                {
                    // Handle collision (no collision response implemented)
                    Debug.Log("Collision detected between spheres: " + bodyA.name + " and " + bodyB.name);
                }
            }
        }
    }
}
