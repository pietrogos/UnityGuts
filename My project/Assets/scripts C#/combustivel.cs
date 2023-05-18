using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combustivel : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Color blueColor;

    public int fuel = 0;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Combustivel"))
        {
            fuel++;
            Debug.Log("Combustivel: " + fuel);

            ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
            ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission;

            float currentLifetime = main.startLifetime.constant;
            main.startLifetime = currentLifetime + 0.01f;

            float currentRate = emission.rateOverTime.constant;
            emission.rateOverTime = currentRate + 2f;
        }
    }
    
}