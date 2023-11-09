using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TsnatsShape : MonoBehaviour
{
    public enum Type
    {
        Sphere = 0,
        Plane,
        HalfSpace
    }

    public abstract Type GetShapeType();
}

