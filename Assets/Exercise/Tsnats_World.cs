using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Tsnats_World : MonoBehaviour
{
    public bool isDebugging = true;
    public float dt = 1.0f / 30.0f; // Fixed timestep
    public float t = 0.0f; // Time
    private List<Tsnats_Body> bodies; // List of bodies in the simulation
    public Vector3 gravity = new Vector3(0, -9.8f, 0); // Gravity vector
    public float damping = 0.10f; // Damping coefficient
    private Dictionary<Tsnats_Body, bool> collisionStates; // Dictionary to track collision states

    // New properties for user controls
    public float mass = 8.0f; // Default mass for bodies
    public TsnatsShapeHalfSpace plane; // Ground plane (assigned in the Unity Editor)

    void Start()
    {
        bodies = new List<Tsnats_Body>(); // Initialize empty list of bodies
        collisionStates = new Dictionary<Tsnats_Body, bool>(); // Initialize empty dictionary for collision states
        dt = Time.fixedDeltaTime; // Get fixed timestep from Unity
    }

    public void CheckForNewBodies()
    {
        Tsnats_Body[] foundBodies = FindObjectsOfType<Tsnats_Body>(); // Find all objects with Tsnats_Body component

        foreach (Tsnats_Body bodyFound in foundBodies) // Iterate through found bodies
        {
            if (!bodies.Contains(bodyFound)) // Check if body is already in the list
            {
                bodies.Add(bodyFound); // Add new body to the list
                collisionStates[bodyFound] = false; // Initialize collision state to false
            }
        }
    }

    public Vector3 GetGravityForce(Tsnats_Body body)
    {
        return gravity * body.gravityScale * body.mass; // Calculate gravity force
    }

    private void ResetNetForces()
    {
        foreach (Tsnats_Body body in bodies) // Reset net forces for all bodies
        {
            body.ResetForces(); // Reset forces for each body
        }
    }

    private void ApplyKinematics()
    {
        foreach (Tsnats_Body body in bodies) // Update positions based on velocities
        {

            // Do kinematics
            body.transform.position += body.velocity * dt;
        }
    }

    private void ApplyAcceleration()
    {
        foreach (Tsnats_Body body in bodies)
        {

            Vector3 ForceGravity = GetGravityForce(body); // Calculate gravity force

            body.AddForce(ForceGravity); // Add gravity force to net forces

            Vector3 AccelerationNet = body.ForceNet / body.mass; // Calculate net acceleration

            //Apply acceleration due to gravity
            body.velocity += AccelerationNet * dt; // Update velocity based on acceleration
            //Damp motions
            body.velocity *= (1.0f - (damping * dt)); // Damp velocity to reduce oscillations

            Debug.DrawLine(body.transform.position, body.transform.position + body.velocity, Color.red, 0.25f, false); // Draw velocity
            Debug.DrawLine(body.transform.position, body.transform.position + ForceGravity, new Color(0.8f, 0.0f, 0.8f), 0.25f, false); // Draw gravity force
            Debug.DrawLine(body.transform.position, body.transform.position + body.ForceNet, Color.blue, 0.25f, false); // Draw net force
        }
    }

    public bool CheckCollisionBetweenSpheres(TsnatsShapeSphere shapeA, TsnatsShapeSphere shapeB)
    {
        //look for the objects A and B
        Vector3 displacment = shapeA.transform.position - shapeB.transform.position;
        float distance = displacment.magnitude;

        if (distance < shapeA.radius + shapeB.radius) // over lapping
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private bool CheckCollisionSpherePlane(TsnatsShapeSphere sphere, TsnatsShapePlane plane)
    {
        Vector3 displacement = sphere.transform.position - plane.transform.position;
        Vector3 normal = plane.transform.up;

        //Get the distanse from the sphere to the plane 
        float distance = Vector3.Dot(displacement, normal);


        float overlap = sphere.radius - distance;

        bool isColliding = distance < sphere.radius;
        if (isColliding)
        {
            Vector3 mtv = (sphere.radius - distance) * normal;
            sphere.transform.position += mtv;

            Tsnats_Body sphereBody = sphere.GetComponent<Tsnats_Body>();

            float fgDotN = Vector3.Dot(GetGravityForce(sphereBody), normal);

            if (fgDotN > 0.0f)
            {
                //// Calculate the Minimum Translation Vector (MTV) to push the sphere out of collision
                //Vector3 mtv = (sphere.radius - distance) * normal;

                //// Apply the MTV to the sphere's position to resolve the collision
                //sphere.transform.position += mtv;


                //Tsnats_Body sphereBody = sphere.GetComponent<Tsnats_Body>();
                return isColliding;
            }
            else
            {
                Vector3 ForceGravityParallel = fgDotN * normal;
                Vector3 ForceNormal = -ForceGravityParallel;
                sphereBody.AddForce(ForceNormal);
                Debug.DrawLine(sphere.transform.position, sphere.transform.position + ForceNormal, Color.green, 0.25f, false);
            }

        }

        return isColliding;
    }

    private bool CheckCollisionSphereHalfSpace(TsnatsShapeSphere sphere, TsnatsShapeHalfSpace halfSpace)
    {
        Vector3 displacement = sphere.transform.position - halfSpace.transform.position;
        Vector3 normal = halfSpace.transform.up;
        float distance = Vector3.Dot(displacement, normal);
        bool isColliding = distance < sphere.radius;
        if (isColliding)
        {
            Vector3 mtv = (sphere.radius - distance) * normal;
            sphere.transform.position += mtv;

            Tsnats_Body sphereBody = sphere.GetComponent<Tsnats_Body>();

            float fgDotN = Vector3.Dot(GetGravityForce(sphereBody), normal);

            if (fgDotN > 0.0f)
            {
                return isColliding;
            }
            else
            {
                Vector3 ForceGravityParallel = fgDotN * normal;
                Vector3 ForceNormal = -ForceGravityParallel;
                sphereBody.AddForce(ForceNormal);
                Debug.DrawLine(sphere.transform.position, sphere.transform.position + ForceNormal, Color.green, 0.25f, false);

                float frictionCoeficient = sphereBody.frictionCoeficient;
            }

        }

        return isColliding;
    }


    private bool CheckCollisionBetween(Tsnats_Body bodyA, Tsnats_Body bodyB)
    {

        if (bodyA.shape == null || bodyB.shape == null) return false;

        // Check for sphere-sphere collision
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Sphere &&
             bodyB.shape.GetShapeType() == TsnatsShape.Type.Sphere)
        {
            return CheckCollisionBetweenSpheres((TsnatsShapeSphere)bodyA.shape, (TsnatsShapeSphere)bodyB.shape);
        }
        // Check for sphere-plane collision
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Sphere &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.Plane)
        {
            return CheckCollisionSpherePlane((TsnatsShapeSphere)bodyA.shape, (TsnatsShapePlane)bodyB.shape);
        }
        // Check for sphere-halfspace collision
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Sphere &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.HalfSpace)
        {
            return CheckCollisionSphereHalfSpace((TsnatsShapeSphere)bodyA.shape, (TsnatsShapeHalfSpace)bodyB.shape);
        }
        // Check for plane-sphere collision (which is the same as sphere-plane)
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Plane &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.Sphere)
        {
            return CheckCollisionSpherePlane((TsnatsShapeSphere)bodyB.shape, (TsnatsShapePlane)bodyA.shape);
        }
        // Check for halfspace-sphere collision (which is the same as sphere-halfspace)
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.HalfSpace &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.Sphere)
        {
            return CheckCollisionSphereHalfSpace((TsnatsShapeSphere)bodyB.shape, (TsnatsShapeHalfSpace)bodyA.shape);
        }
        // Check for halfspace-plane collision
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.HalfSpace &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.Plane)
        {
            // Assuming half-spaces do not collide with planes
            return false;
        }
        // Check for plane-halfspace collision (which is the same as halfspace-plane)
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Plane &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.HalfSpace)
        {
            // Assuming planes do not collide with half-spaces
            return false;
        }
        // Check for halfspace-halfspace collision (non-colliding for infinite spaces)
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.HalfSpace &&
                 bodyB.shape.GetShapeType() == TsnatsShape.Type.HalfSpace)
        {
            // Half-spaces are infinite, so let's assume they don't collide with each other
            return false;
        }
        else
        {
            throw new System.Exception("Unknown collision types: " + bodyA.shape.GetShapeType() + " and " + bodyB.shape.GetShapeType());
        }
    }

    public void CheckCollisions()
    {
        // First, set all bodies to not colliding.
        foreach (var body in bodies)
        {
            collisionStates[body] = false;

        }

        for (int i = 0; i < bodies.Count; i++)
        {
            Tsnats_Body bodyA = bodies[i];

            for (int j = i + 1; j < bodies.Count; j++)
            {
                Tsnats_Body bodyB = bodies[j];
                bool isColliding = CheckCollisionBetween(bodyA, bodyB);

                if (isColliding)
                {
                    Debug.Log($"Collision detected between {bodyA.name} and {bodyB.name}");
                    collisionStates[bodyA] = true;
                    collisionStates[bodyB] = true;
                }
            }
        }

        // Update colors based on collision state.
        foreach (var body in bodies)
        {
            Renderer renderer = body.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (collisionStates[body])
                {
                    renderer.material.color = Color.red;
                }
                else
                {
                    renderer.material.color = Color.white;
                }
                // Check if the body is a plane for color updating
                if (collisionStates[body] && body.shape.GetShapeType() == TsnatsShape.Type.Plane)
                {

                }

            }
        }
    }
    private void FixedUpdate()
    {
        CheckForNewBodies();
        ResetNetForces();
        ApplyKinematics();
        CheckCollisions();
        ApplyAcceleration();

        t += dt;
    }

}