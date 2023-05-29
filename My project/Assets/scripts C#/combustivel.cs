using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combustivel : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Color blueColor;

    public int fuel = 0;
    private float update;

    private float MIN_LIFETIME = 0.5f;
    private float MIN_EMISSION = 86f;
    private float MAX_LIFETIME = 2.8f;
    private float MAX_EMISSION = 250f;

    private float RATE_RAISE_LIFETIME = 0.6f;
    private float RATE_LOWER_LIFETIME = 0.2f;
    private float RATE_RAISE_EMISSION = 50f;
    private float RATE_LOWER_EMISSION = 20f;
    
    private void Update()
    {
        update += Time.deltaTime;

        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission;

        float currentLifetime = main.startLifetime.constant;
        float currentRate =  emission.rateOverTime.constant;

        if (update > 2.5f)
        {
            update = 0.0f;

            if (currentLifetime - RATE_LOWER_LIFETIME >= MIN_LIFETIME) 
            {
                main.startLifetime = currentLifetime - RATE_LOWER_LIFETIME;
            }
            else 
            {
                main.startLifetime = MIN_LIFETIME;
            }

            if (currentRate - RATE_LOWER_EMISSION >= MIN_EMISSION)
            {
                emission.rateOverTime = currentRate - RATE_LOWER_EMISSION;
            } 
            else
            {
                emission.rateOverTime = MIN_EMISSION;
            }
        }
    }

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
            float currentRate = emission.rateOverTime.constant;

            if(currentLifetime + RATE_RAISE_LIFETIME <= MAX_LIFETIME)
            {
                main.startLifetime = currentLifetime + RATE_RAISE_LIFETIME;
            } 
            else 
            {
                main.startLifetime = MAX_LIFETIME;
            }



            if(currentRate + RATE_RAISE_EMISSION <= MAX_EMISSION)
            {
                emission.rateOverTime = currentRate + RATE_RAISE_EMISSION;
            } 
            else 
            {
                emission.rateOverTime = MAX_EMISSION;
            }
        }
    }
}