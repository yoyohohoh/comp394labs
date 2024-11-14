using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectGems : MonoBehaviour
{
    public int target;
    public Text gemsLeft;
    public Text winText;
    int initialGems;

    private void Start()
    {
        initialGems = GameObject.FindGameObjectsWithTag("Gem").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Gem").Length;
        gemsLeft.text = $"Remaining Gems to Collect: {count.ToString()}";

        if (count <= initialGems - target)
        {
            Debug.Log("You Win!");
            winText.enabled = true;
        }
    }

}
