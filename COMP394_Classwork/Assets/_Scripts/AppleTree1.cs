using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppleTree1 : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab1;
    public GameObject applePrefab2;
    // Speed at which the AppleTree moves
    public float speed = 1f;
    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;
    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;
    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;
    void Start()
    {
        // Dropping apples every second
        Invoke("DropApple", 2f);
    }

    void DropApple()
    { // b
        int randomProbability = Random.Range(0,100);

        if(randomProbability < 50)
        {
            GameObject apple = Instantiate<GameObject>(applePrefab1); // c
            apple.transform.position = transform.position; // d
            Invoke("DropApple", secondsBetweenAppleDrops); // e
        }
        else
        {
            GameObject apple = Instantiate<GameObject>(applePrefab2); // c
            apple.transform.position = transform.position; // d
            Invoke("DropApple", secondsBetweenAppleDrops); // e
        }


    }

    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position; // b
        pos.x += speed * Time.deltaTime; // c
        transform.position = pos; // d

        //// Changing Direction
        //if (pos.x < -leftAndRightEdge)
        //{ // a
        //    speed = Mathf.Abs(speed); // Move right // b
        //}
        //else if (pos.x > leftAndRightEdge)
        //{ // c
        //    speed = -Mathf.Abs(speed); // Move left // c
        //}

        //Changing Random Direction
        //if (pos.x < -leftAndRightEdge)
        //{
        //    speed = Mathf.Abs(speed); // Move right
        //}
        //else if (pos.x > leftAndRightEdge)
        //{
        //    speed = -Mathf.Abs(speed); // Move left
        //}
        //else if (Random.value < chanceToChangeDirections)
        //{ // a
        //    speed *= -1; // Change direction // b
        //}

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    void FixedUpdate()
    {
        // Changing Direction Randomly is now time-based because of FixedUpdate()
        if (Random.value < chanceToChangeDirections)
        { // b
            speed *= -1; // Change direction
        }
    }
}