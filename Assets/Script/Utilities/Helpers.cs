using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers 
{
    private static Matrix4x4 isometrix = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));

    public static Vector3 ToIso(this Vector3 input) => isometrix.MultiplyPoint3x4(input);

}
