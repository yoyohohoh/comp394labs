using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReveal : MonoBehaviour
{
    public float speed = 5;
    private bool isRotating;

    private void Start()
    {
        isRotating = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RotatePlatform());
        }
    }

    private IEnumerator RotatePlatform()
    {
        isRotating = true;
        float targetRotation = transform.eulerAngles.z + 180f;
        float currentRotation = transform.eulerAngles.z;

        while (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) > 0.1f)
        {
            currentRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation);

            yield return null;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z);
        isRotating = false;
    }

}
