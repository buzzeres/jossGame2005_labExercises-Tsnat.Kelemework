using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsnatsShapePlane : TsnatsShape
{
    public Vector3 normal;
    public float distanceFromOrigin;

    public override Type GetShapeType()
    {
        return Type.Plane;
    }
}