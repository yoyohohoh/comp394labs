using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveWithSin : MonoBehaviour
{
    public float speed = 5f;                     // Speed of movement
    public float maxHorizontalDistance = 4f;     // Maximum horizontal distance
    public float verticalSpeed = 3f;             // Vertical speed
    public float maxVerticalDistance = 5f;       // Maximum vertical distance
    public Vector3 originalPos;                  // Initial position

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newDeltaPos = Vector3.zero;

        // Horizontal movement
        if (maxHorizontalDistance > 0)
        {
            newDeltaPos.x = maxHorizontalDistance * Mathf.Sin(Time.fixedTime * speed / maxHorizontalDistance);
            //Debug.Log($"Time.fixedTime={Time.fixedTime}, newDeltaPos.x={newDeltaPos.x}");
        }

        // Vertical movement
        if (maxVerticalDistance > 0)
        {
            newDeltaPos.y = maxVerticalDistance * Mathf.Sin(Time.fixedTime * verticalSpeed / maxVerticalDistance);
        }

        // Update the platform position
        this.transform.position = originalPos + newDeltaPos;
    }
}

