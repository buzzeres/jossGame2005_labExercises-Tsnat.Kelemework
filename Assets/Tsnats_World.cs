using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tsnats_World : MonoBehaviour
{
    public float dt = 1.0f/30.0f;    // Start is called before the first frame update
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


    private void FixedUpdate()
    {
        CheckForNEWBodies();
        foreach (Tsnats_Body body in bodies)
        {
            body.velocity += gravity * body.gravityScale * dt;

            //body.velocity *= 1.0f - (damping * dt);
            Vector3 drag = -body.velocity * body.velocity.magnitude * body.velocity.magnitude * body.AirFriction;

            body.transform.position += body.velocity * dt;

        }

        t += dt;
    }

}
