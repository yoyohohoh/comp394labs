using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 5; // m/sec
    public float maxHorizontalDistance = 4; // m
    public float verticalSpeed = 3; // m/sec
    public float maxVerticalDistance = 5; // m
    public Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
    }

    void FixedUpdate()
    {
        if (maxHorizontalDistance > 0)
        {
            var newX = Mathf.PingPong(Time.fixedTime * speed, maxHorizontalDistance);
            //print($"Time.fixedTime= {Time.fixedTime}, newX= {newX}");
            this.transform.position = originalPos + new Vector3(newX, 0, 0);
        }
        else if (maxVerticalDistance > 0)
        {
            var newY = Mathf.PingPong(Time.fixedTime * verticalSpeed, maxVerticalDistance);
            this.transform.position = originalPos + new Vector3(0, newY, 0);
        }
    }

}
