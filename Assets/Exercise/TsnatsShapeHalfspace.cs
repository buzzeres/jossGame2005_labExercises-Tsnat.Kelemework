using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsnatsShapeHalfSpace : TsnatsShape
{

    public Vector3 normal; // Normal vector of the plane
    public Vector3 point; // A point on the plane


    public override Type GetShapeType()
    {
        return Type.HalfSpace;
    }
}
