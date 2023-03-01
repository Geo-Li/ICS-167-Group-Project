using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityMovement
{
    // Makes the object faces toward the lookingPosition
    public void UpdateRotation(Vector3 lookingPosition);

    // Returns the current speed of the object
    public float GetCurrentSpeed();
}
