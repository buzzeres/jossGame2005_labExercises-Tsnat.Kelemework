using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newScript : MonoBehaviour
{
    public float lineLenght = 4.0f;

    public float a = 1.0f;
    public float b = 3.0f;
    public float t = 0.0f;
     public float X, Y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World! My name is: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(lineLenght, 0.0f, 0.0f), Color.red);

        Debug.DrawLine(transform.position, transform.position + new Vector3(lineLenght, 0.0f, 0.0f), Color.green);

        Debug.DrawLine(transform.position, transform.position + new Vector3(lineLenght, 0.0f, 0.0f), Color.blue);
    }

    private void FixedUpdate()
    {
        float dt = 1.0f / 60.0f;
        X = X + (-Mathf.Sin(t * a) * a * b * dt);
        Y = Y +(-Mathf.Cos(t * a) * a * b * dt);

        transform.position = new Vector3(X, Y, transform.position.z);

        t = dt;

    }

}
