using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class TsnatsShapeSphere : TsnatsShape
{
    public float radius = 1.0f;
    private void Start()
    {
       
    }

    public override Type GetShapeType()
    {
        return TsnatsShape.Type.Sqhere;
    }
}
