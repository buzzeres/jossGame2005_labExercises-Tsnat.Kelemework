using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Tsnats_World : MonoBehaviour
{
    public bool isDebugging = true;
    public float dt = 1.0f / 30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    private List<Tsnats_Body> bodies;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);
    public float damping = 0.10f;
    private Dictionary<Tsnats_Body, bool> collisionStates;

    // New properties for user controls
    public float mass = 8.0f; // in kilograms
    public TsnatsShapeHalfSpace plane; // Assign this in the Unity Editor


    void Start()
    {
        bodies = new List<Tsnats_Body>();
        collisionStates = new Dictionary<Tsnats_Body, bool>();
        dt = Time.fixedDeltaTime;

    }

    public void CheckForNewBodies()
    {
        Tsnats_Body[] foundBodies = FindObjectsOfType<Tsnats_Body>();

        foreach (Tsnats_Body bodyFound in foundBodies)
        {
            if (!bodies.Contains(bodyFound))
            {
                bodies.Add(bodyFound);
                collisionStates[bodyFound] = false;
            }
        }
    }

    private void ApplyKinematics()
    {
        foreach (Tsnats_Body body in bodies)
        {
            if (!body.isStatic)
            {
                body.velocity += gravity * body.gravityScale * dt;
                body.velocity *= 1.0f - (damping * dt);
                Vector3 drag = -body.velocity * body.velocity.magnitude * body.velocity.magnitude * body.AirFriction;
                body.velocity += drag * dt;
                body.transform.position += body.velocity * dt;
            }
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
        

        bool isColliding = distance < sphere.radius;
        // Collision response
        if (isColliding)
        {
            // Calculate the Minimum Translation Vector (MTV) to push the sphere out of collision
            Vector3 mtv = (sphere.radius - distance) * normal;

            // Apply the MTV to the sphere's position to resolve the collision
            sphere.transform.position += mtv;
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
        ApplyKinematics();
        CheckCollisions();
        CheckForNewBodies();

        Tsnats_Body circle = bodies.Find(body => body.shape.GetShapeType() == TsnatsShape.Type.Sphere);
        if (circle != null && plane != null)
        {
            // Calculate force vectors
            Vector3 fg = gravity * mass; // Gravitational force vector
            Vector3 N = plane.transform.up * (-Vector3.Dot(gravity, plane.transform.up)) * mass; // Normal force vector
            Vector3 fgPerp = Vector3.ProjectOnPlane(fg, plane.transform.up); // Parallel component of gravity

            //the friction force will be equal and opposite to fgPerp
            Vector3 frictionForce = -fgPerp;

            // Visualize the forces
            Debug.DrawRay(circle.transform.position, N, Color.green, dt); // Normal force in green
            Debug.DrawRay(circle.transform.position, frictionForce, Color.blue, dt); // Friction force in blue for visibility
            Debug.DrawRay(circle.transform.position, fg, Color.magenta, dt); // Gravity force in purple
        }

        t += dt;
    }

}