using System;
using UnityEngine;

[Serializable]
public class MovementInfo 
{
    // Current position
    public Vector3 position;
    // Current orientation
    public float orientation;

    // Movement direction and speed
    public Vector3 velocity;
    // Rotation direction and speed
    public float rotation;
}
