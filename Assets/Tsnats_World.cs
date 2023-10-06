using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tsnats_World : MonoBehaviour
{
    public float dt = 1.0f/30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    public List<Tsnats_World> bodies;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    void Start()
    {
        bodies = new List<Tsnats_World>();
        dt = Time.fixedDeltaTime;

    }

    public void AddNewodiesFromScene()
    {
        Tsnats_Body[] foundBodies = FindObjectOfType<Tsnats_Body>();

        foreach (Tsnats_Body foundBody in foundBodies)
        {
            
        }
    }


    private void FixedUpdate()
    {
        AddNewodiesFromScene();
        foreach (Tsnats_Body body in bodies)
        {
            body.velocity += gravity * body.gravityScale * dt;
            body.transform.position += body.velocity + dt;
        }

        t += dt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
