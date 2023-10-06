using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TsnatsWorld : MonoBehaviour
{
    public float dt = 1.0f/30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    public List<Tsnats_Body> bodies;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    void Start()
    {
        bodies = new List<Tsnats_Body>();
        dt = Time.fixedDeltaTime;

    }

    public void AddNewodiesFromScene()
    {
        Tsnats_Body[] foundBodies = FindObjectOfType<Tsnats_Body>();

<<<<<<< Updated upstream
        foreach (Tsnats_Body foundBody in foundBodies)
=======
        Tsnats_Body[] allBodies = FindObjectsOfType< Tsnats_Body > (false);

        foreach (Tsnats_Body foundBody in allBodies)
>>>>>>> Stashed changes
        {
            
        }
    }


    private void FixedUpdate()
    {
        AddNewodiesFromScene();
        foreach (Tsnats_Body body in bodies)
        {
<<<<<<< Updated upstream
            body.velocity += gravity * body.gravityScale * dt;
            body.transform.position += body.velocity + dt;
=======
            {
                body.velocity += gravity * body.gravityScale * dt;

                body.transform.position += body.velocity * dt;

            }
>>>>>>> Stashed changes
        }
       

        t += dt;
    }

}
