using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TsnatsShape : MonoBehaviour
{


    public enum Type
    {
        Sqhere = 0
    }

    public abstract Type GetShapeType();
        

}
