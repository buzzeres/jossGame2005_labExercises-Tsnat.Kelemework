using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsnatsShapeSphere : TsnatsShape
{
    public float radius = 1.0f;

    public void Start()
    {
        
    }

    public override Type GetShapeType()
    {
        return TsnatsShape.Type.Sphere;  // Corrected spelling
    }
}
