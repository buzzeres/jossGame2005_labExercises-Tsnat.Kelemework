using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsnatsShapeHalfSpace : TsnatsShape
{

    public override Type GetShapeType()
    {
        return Type.HalfSpace;
    }
}
