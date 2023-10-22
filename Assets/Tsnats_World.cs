using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tsnats_World : MonoBehaviour
{
    public float dt = 1.0f / 30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    private List<Tsnats_Body> bodies;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);
    public float damping = 0.10f;

    void Start()
    {
        bodies = new List<Tsnats_Body>();
        dt = Time.fixedDeltaTime;

    }

    public void CheckForNEWBodies()
    {

        Tsnats_Body[] allBodies = FindObjectsOfType<Tsnats_Body>(false);
        //Find any bodies in the scene not already tracked, if they are not tracked, add them.

        foreach (Tsnats_Body foundBody in allBodies)
        {
            if (!bodies.Contains(foundBody))
            {
                bodies.Add(foundBody);
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

            body.transform.position += body.velocity * dt;

        }
    }

    private void CheckCollisions()
    {
        //For each object 
        for(int i = 0; i < bodies.Count; i++)
        {
            Tsnats_Body bodyA = bodies[i];

            //checking the collision between eachother
            for (int j = 1+1; j < bodies.Count; j++)
            {
                Tsnats_Body bodyB = bodies[j];


                Vector3 displacment = bodyA.transform.position - bodyB.transform.position;
                float distance = displacment.magnitude;

                if (distance < bodyA.radius + bodyB.radius)
                {
                    print(bodyA.gameObject.name + "colliding with " + bodyB.gameObject.name);
                }
                else 
                {

                }


            }

        }
    }

    private void FixedUpdate()
    {
        CheckForNEWBodies();
        CheckCollisions();
        AddlyKinematics();
         t += dt;
    }

}