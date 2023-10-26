using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tsnats_World : MonoBehaviour
{
    public bool isDebuging = true;
    public float dt = 1.0f / 30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    private List<Tsnats_Body> bodies;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);
    public float damping = 0.10f;
    private int i;

    void Start()
    {
        bodies = new List<Tsnats_Body>();
        dt = Time.fixedDeltaTime;

    }

    public void CheckForNEWBodies()
    {

        Tsnats_Body[] foundBodies = FindObjectsOfType<Tsnats_Body>();
        //Find any bodies in the scene not already tracked, if they are not tracked, add them.

        foreach (Tsnats_Body bodyFound in foundBodies)
        {
            if (!bodies.Contains(bodyFound))
            {
                bodies.Add(bodyFound);
            }
        }
    }


    private void AddlyKinematics()
    {
        foreach (Tsnats_Body body in bodies)
        {
            body.velocity += gravity * body.gravityScale * dt;
            body.velocity *= 1.0f - (damping * dt);
            Vector3 drag = -body.velocity * body.velocity.magnitude * body.velocity.magnitude * body.AirFriction;
            body.velocity += drag * dt;  // Apply the drag to the velocity.
            body.transform.position += body.velocity * dt;

        }
    }


    public bool CheckCollisionBetweenSqheres(Tsnats_Body ShapeA, Tsnats_Body ShapeB)
    {
        //look for the objects A and B
        Vector3 displacment = bodyA.transform.position - bodyB.transform.position;
        float distance = displacment.magnitude;

        if (distance < bodyA.radius + bodyB.radius) // over lapping
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckCollisionBetween(Tsnats_Body bodyA, Tsnats_Body bodyB)
    {
        if(bodyA.shape == null || bodyB.shape == null) return false;
        else if (bodyA.shape.GetShapeType() == TsnatsShape.Type.Sqhere &&
                bodyB.shape.GetShapeType() == TsnatsShape.Type.Sqhere)
        {
            CheckCollisionBetweenSqheres((TsnatsShapeSpher)bodyA.shape, bodyB.shape);
        }
        
    }

    public void CheckCollisions()
    {
        if (isDebuging)
        {
            for (int i = 0; i < bodies.Count; i++)
            {

                bodies[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }
        for (int i = 0; i < bodies.Count; i++)
        {
            Tsnats_Body bodyA = bodies[i];
            
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Tsnats_Body bodyB = bodies[j];

                bool isColliding = CheckCollisionBetween(bodyA, bodyB);

                //if (isColliding)
                //{
                //    ResolveCollision(bodyA, bodyB);
                //}

                if (isDebuging)
                {
                    if (isColliding)
                    {
                        bodyA.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        bodyB.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
        }
    }

    //private void ResolveCollision(Tsnats_Body bodyA, Tsnats_Body bodyB)
    //{
    //    Vector3 collisionNormal = (bodyB.transform.position - bodyA.transform.position).normalized;
    //    bodyA.velocity = Vector3.Reflect(bodyA.velocity, collisionNormal);
    //    bodyB.velocity = Vector3.Reflect(bodyB.velocity, collisionNormal);
    //}


    private void FixedUpdate()
    {
        AddlyKinematics();
        CheckCollisions();
        CheckForNEWBodies();
        
         t += dt;
    }

}