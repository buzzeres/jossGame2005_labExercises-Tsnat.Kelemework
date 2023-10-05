using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsnats_World : MonoBehaviour
{
    public float dt = 1.0f/30.0f;    // Start is called before the first frame update
    public float t = 0.0f;
    void Start()
    {
        dt = Time.fixedDeltaTime;

    }


    private void FixedUpdate()
    {
        object bodies;
        foreach (Tsnats_Body body in bodies)
        {
            body.velocity +=  * dt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
