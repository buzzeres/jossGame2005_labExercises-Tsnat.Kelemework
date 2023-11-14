using UnityEngine;

public class ForceVisualization : MonoBehaviour
{
    public Transform circleTransform; // Assign the circle's transform here
    public float mass = 8f; // Mass of the circle in kg
    public float gravity = 9.81f; // Acceleration due to gravity in m/s^2
    public Transform planeTransform; // Assign the plane's transform here
    public float dt = 1.0f / 30.0f;
    public float t = 0.0f;

    private Vector3 normalForce;
    private Vector3 gravityForce;
    private Vector3 frictionForce;

    void CalculateForces()
    {
        // Calculate the angle of the plane with respect to the horizontal
        float angle = planeTransform.eulerAngles.x;

        // Force of gravity acting on the circle
        gravityForce = new Vector3(0, -mass * gravity, 0);

        // Normal force is the component of gravity perpendicular to the plane
        normalForce = Quaternion.Euler(0, 0, angle) * new Vector3(0, mass * gravity, 0);

        // Friction force is the component of gravity parallel to the plane
        // Since the coefficient of kinetic friction is infinite, we simulate it by making the friction force equal
        // to the component of gravity that is parallel to the plane's surface to prevent motion
        frictionForce = Quaternion.Euler(0, 0, angle) * new Vector3(0, 0, mass * gravity);
    }

    void DrawForces()
    {
        CalculateForces();

        // Use the circle's position as the starting point for force vectors
        Vector3 objectPosition = circleTransform.position;

        // Draw the force vectors at the circle's position
        Debug.DrawRay(objectPosition, normalForce, Color.green);
        Debug.DrawRay(objectPosition, gravityForce, Color.magenta);
        Debug.DrawRay(objectPosition, frictionForce, Color.red);
    }

    void FixedUpdate()
    {
        CalculateForces();
        DrawForces();
        t += dt;
    }
}
