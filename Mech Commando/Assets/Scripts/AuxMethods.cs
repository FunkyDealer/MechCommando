using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AuxMethods {
    /// Normalize an angle, guaranteeing it to be between -pi and pi 
    public static float NormAngle(float angle) {
        while (angle < -Mathf.PI) angle += Mathf.PI * 2f;
        while (angle > Mathf.PI) angle -= Mathf.PI * 2f;
        return angle;
    }




    //These 3 methods bellow are lighter than doing Vector3.Distance() > distance 

    public static bool CompareDistanceBigger(Vector3 pointA, Vector3 pointB, float dist)
    {
        return ((pointA - pointB).sqrMagnitude < dist * dist);
    }

    public static bool CompareDistanceSmaller(Vector3 pointA, Vector3 pointB, float dist)
    {
        return ((pointA - pointB).sqrMagnitude > dist * dist);
    }

    public static bool CompareDistanceEqual(Vector3 pointA, Vector3 pointB, float dist)
    {
        return ((pointA - pointB).sqrMagnitude == dist * dist);
    }

}
