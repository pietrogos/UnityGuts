using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combustivel : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Color blueColor;

    private int fuel = 0;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        if (other.gameObject.CompareTag("Combustivel"))
        {
            fuel++;
            Debug.Log("fuel: " + fuel);
            if (fuel == 3)
            {
                main.startLifetime = 2.0f;
            }
        }
    }
}