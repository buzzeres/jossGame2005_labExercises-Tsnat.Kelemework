using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionmanagementsystem : MonoBehaviour
{
    static void Main(string[] args)
    {
        // Create two physics bodies, one as a sphere and the other as a rectangle.
        PhysicsBody sphere = new PhysicsBody(ShapeType.Sphere, radius: 2.0);
        PhysicsBody rectangle = new PhysicsBody(ShapeType.Rectangle, width: 4.0, height: 3.0);

        // Check for collision between the two bodies.
        bool isColliding = CheckCollision(sphere, rectangle);

        Console.WriteLine($"Are the two bodies colliding? {isColliding}");
    }

    // Enum to represent different shape types.
    enum ShapeType
    {
        Sphere,
        Rectangle
    }

    // Class to represent a physics body.
    class PhysicsBody
    {
        public ShapeType Shape { get; }
        public double Radius { get; }
        public double Width { get; }
        public double Height { get; }

        public PhysicsBody(ShapeType shape, double radius = 0.0, double width = 0.0, double height = 0.0)
        {
            Shape = shape;
            Radius = radius;
            Width = width;
            Height = height;
        }
    }

    // Function to check for collision between two physics bodies.
    static bool CheckCollision(PhysicsBody body1, PhysicsBody body2)
    {
        if (body1.Shape == ShapeType.Sphere && body2.Shape == ShapeType.Sphere)
        {
            // Calculate the distance between the centers of the two spheres.
            double distance = Math.Sqrt(
                Math.Pow(body2.Radius - body1.Radius, 2) +
                Math.Pow(body2.Radius - body1.Radius, 2)
            );

            // Check if the distance is less than or equal to the sum of their radii.
            return distance <= (body1.Radius + body2.Radius);
        }
        else
        {
            // Handle collision detection for other shape types (e.g., rectangles).
            // Implement the specific collision detection logic for each shape.
            // For simplicity, we assume no collision for other shapes in this example.
            return false;
        }
    }
}

