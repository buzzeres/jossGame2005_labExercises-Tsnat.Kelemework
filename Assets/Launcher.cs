using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchSpeed = 10;
    public GameObject projectileToCopy;
    public float launchElevation = 30;

    public Tsnats_World world;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        }
}
}
