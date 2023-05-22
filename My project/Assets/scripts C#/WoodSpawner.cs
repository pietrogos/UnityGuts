using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toraSpawner : MonoBehaviour
{
    public GameObject toraPrefab; // Set this in the Inspector with your tora prefab
    public int maxToras = 15; // Maximum number of tora pieces allowed in the scene at once
    private int currentToras = 15; // Current number of active tora pieces
    public Vector3 spawnValues; // Set the range for random spawn positions in the Inspector

    private void Start()
    {
        StartCoroutine(SpawnToras());
    }

    IEnumerator SpawnToras()
    {
        while (true) // Infinite loop, be careful with these!
        {
            // Only spawn a new tora piece if we haven't reached the maximum number
            if (currentToras < maxToras)
            {
                float spawnX = Random.Range(-spawnValues.x, spawnValues.x);
                float spawnZ = Random.Range(-spawnValues.z, spawnValues.z);
                Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ);
                Instantiate(toraPrefab, spawnPosition, Quaternion.identity);
                currentToras++;
            }

            // Wait for 1 second before the next loop iteration
            yield return new WaitForSeconds(1);
        }
    }

    public void DecreaseToraCount()
    {
        if (currentToras > 0)
            currentToras--;
    }
}

