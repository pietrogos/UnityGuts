using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToraBehaviour : MonoBehaviour
{
    private float lifespan = 3.0f;
    private bool isInsideFire = false;
    private bool isInsideSpawn = false;

    private void Update()
    {
        if(!isInsideSpawn)
        {
            lifespan -= Time.deltaTime;
        }

        if (lifespan <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spawn"))
        {
            isInsideSpawn = true;
        }
        if (other.CompareTag("FogueiraCollider"))
        {
            isInsideFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawn"))
        {
            isInsideSpawn = false;
        }
        if (other.CompareTag("FogueiraCollider"))
        {
            isInsideFire = false;
        }
    }
}
