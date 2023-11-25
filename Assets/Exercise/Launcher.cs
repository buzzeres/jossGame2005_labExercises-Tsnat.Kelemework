using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchSpeed = 10.0f;
    public float launchAngleElevationDegrees = 30.0f;

    // Assign the projectile Prefab in the Unity Inspector.
    public GameObject ProjectileClone;

    void Shoot()
    {
        // Check if ProjectileClone is assigned before instantiating.
        if (ProjectileClone != null)
        {
            // Create a new instance of the ProjectileClone.
            GameObject newObject = Instantiate(ProjectileClone);

            // Set the position for the Launcher.
            newObject.transform.position = transform.position;

            // Get the Tsnats_Body component from the new projectile.
            Tsnats_Body tswana = newObject.GetComponent<Tsnats_Body>();

            // Calculate the velocity based on the launch angle and speed.
            float launchAngleRadians = launchAngleElevationDegrees * Mathf.Deg2Rad;
            Vector3 velocity = new Vector3(
                Mathf.Cos(launchAngleRadians) * launchSpeed,
                Mathf.Sin(launchAngleRadians) * launchSpeed,
                0
            );

            // Set the velocity of the projectile.
            tswana.velocity = velocity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
}