using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
<<<<<<< Updated upstream
    public float launchSpeed = 10;
    public GameObject projectileToCopy;
    public float launchElevation = 30;

    public Tsnats_World world;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
=======
    public float launchSpeed = 10.0f;
    public GameObject projectileToCopy;
    public float launchElevation = 30;

    // Update is called once per frame

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newProjectile = Instantiate(projectileToCopy);
            Tsnats_Body tsnats = newProjectile.GetComponent<Tsnats_Body>();
            newProjectile.transform.position = transform.position;

            tsnats.velocity = new Vector3(
                Mathf.Cos(launchElevation * Mathf.Deg2Rad * launchSpeed),
                    Mathf.Sin(launchElevation * Mathf.Deg2Rad * launchSpeed), 
                0);
        }

    }
    
>>>>>>> Stashed changes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
<<<<<<< Updated upstream
            GameObject newProjectile = Instantiate(projectileToCopy);
            newProjectile.transform.position = transform.position;

            Tsnats_Body tsnats = newProjectile.GetComponent<Tsnats_Body>();

            if (tsnats == null)
            {
                Debug.Log("no Tsnats ");
            }

            tsnats.velocity = new Vector3(launchElevation * Mathf.Deg2Rad* launchSpeed,
                                            launchElevation* Mathf.Deg2Rad*launchSpeed,
                0);
=======
            Shoot();
>>>>>>> Stashed changes
        }
    }
}
