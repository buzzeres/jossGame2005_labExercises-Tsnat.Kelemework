using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchSpeed = 10.0f;
    public float LaunchAngleEvevationdegres = 30.0f;

    public GameObject ProjectileClone;
  //  public Tsnats_World world;

  void Shoot()
  {
      GameObject newObject = Instantiate(ProjectileClone);
      //  world.bodies.Add(newObject.GetComponent<Tsnats_Body>());
      //this sets the position for the Launcher
      newObject.transform.position = transform.position;
      Tsnats_Body tswana = newObject.GetComponent<Tsnats_Body>();


      tswana.velocity = new Vector3(
          Mathf.Cos(LaunchAngleEvevationdegres * Mathf.Deg2Rad * launchSpeed),
          Mathf.Sin(LaunchAngleEvevationdegres * Mathf.Deg2Rad * launchSpeed),
          0);
    }


  void Update()
  {

      if (Input.GetKeyDown(KeyCode.Space))
      {
          Shoot();
      }

  }
}
