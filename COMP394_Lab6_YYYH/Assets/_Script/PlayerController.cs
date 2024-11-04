using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody rb;
    public Scrollbar Health;
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from the player
        float x = Input.GetAxis("Horizontal"); // Left/Right movement
        float z = Input.GetAxis("Vertical"); // Forward/Backward movement

        // Create a movement vector based on input
        Vector3 move = new Vector3(x, 0, z) * moveSpeed;

        // Move the player by setting the Rigidbody's velocity
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z); // Keep the current y velocity
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Health.size -= 0.1f;

            if (Health.size <= 0)
            {
                Debug.Log("Player die");
                SceneManager.LoadScene(2);
            }
        }
    }

}
