using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformExpand : MonoBehaviour
{
    public float speed = 5;
    public float maxWidth;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float newWidth = Mathf.Max(1f, Mathf.PingPong(Time.time * speed, maxWidth));

        this.transform.localScale = new Vector3(newWidth,1,1);
        if (player != null && player.IsChildOf(this.transform))
        {
            player.localScale = new Vector3(1/newWidth, 1f, 1f);
        }
    }
}
