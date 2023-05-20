using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combustivel : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Color blueColor;

    public int fuel = 0;
    private float update;

    
    private void Update()
    {
        update += Time.deltaTime;

        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission;
        Debug.Log(main.startLifetime.constant);
        Debug.Log(emission.rateOverTime.constant);

        float currentLifetime = main.startLifetime.constant;
        float currentRate =  emission.rateOverTime.constant;

        if (update > 5.0f)
        {
            update = 0.0f;

            if (currentLifetime - 0.01f >= 0.5f) 
            {
                main.startLifetime = currentLifetime - 0.01f;
            }

            if (currentRate - 2f >= 86.59)
            {
                emission.rateOverTime = currentRate - 2f;
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
            main.startLifetime = currentLifetime + 0.01f;

            float currentRate = emission.rateOverTime.constant;
            emission.rateOverTime = currentRate + 2f;
        }
    }
    
}