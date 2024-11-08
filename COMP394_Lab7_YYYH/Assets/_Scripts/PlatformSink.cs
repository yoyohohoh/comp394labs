using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSink : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
