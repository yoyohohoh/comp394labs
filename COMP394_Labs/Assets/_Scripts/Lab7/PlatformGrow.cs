using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGrow : MonoBehaviour
{
    public float maxHeight = 10;
    public float speed = 5;
    private float currentHeight = 1;
    private Vector3 initialScale;
    private bool isPlayerOnPlatform = false;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        initialScale = this.transform.localScale;
        currentHeight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerOnPlatform)
        {
            if (currentHeight < maxHeight)
            {
                currentHeight += speed * Time.deltaTime;
                currentHeight = Mathf.Min(currentHeight, maxHeight);
                this.transform.localScale = new Vector3(1, currentHeight, 1);
                this.transform.position = new Vector3(this.transform.position.x, maxHeight - 1, this.transform.position.z);
                if (player != null && player.IsChildOf(this.transform))
                {
                    player.localScale = new Vector3(1f, 1/ currentHeight, 1f);
                }
            }

        }
        else
        {
            this.transform.localScale = initialScale;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }


}
