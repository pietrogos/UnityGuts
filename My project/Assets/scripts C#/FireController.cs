using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fire;
    public ParticleSystem rings;
    public ParticleSystem embersSmall;
    public ParticleSystem embersFlickering;
    public ParticleSystem fireMains;

    private ParticleSystem.MainModule fireMain;
    private ParticleSystem.EmissionModule fireEmission;

    private ParticleSystem.MainModule ringsMain;
    private ParticleSystem.EmissionModule ringsEmission;

    private ParticleSystem.MainModule embersSmallMain;
    private ParticleSystem.EmissionModule embersSmallEmission;

    private ParticleSystem.MainModule embersFlickeringMain;
    private ParticleSystem.EmissionModule embersFlickeringEmission;

    private ParticleSystem.MainModule fireMainsMain;
    private ParticleSystem.EmissionModule fireMainsEmission;

    private Vector3 fireMainsOriginalScale;

    private float fireRateOverTime = 40f;
    private float ringsRateOverTime = 10f;
    private float embersSmallRateOverTime = 40f;
    private float embersFlickeringRateOverTime = 10f;
    private float fireMainsRateOverTime = 10f;

    private void Awake()
    {
        fireMain = fire.main;
        fireEmission = fire.emission;

        ringsMain = rings.main;
        ringsEmission = rings.emission;

        embersSmallMain = embersSmall.main;
        embersSmallEmission = embersSmall.emission;

        embersFlickeringMain = embersFlickering.main;
        embersFlickeringEmission = embersFlickering.emission;

        fireMainsMain = fireMains.main;
        fireMainsEmission = fireMains.emission;

        fireMainsOriginalScale = fireMains.transform.localScale;
    }

    private void Update()
    {
        // Decrease rate over time every 3 seconds
        fireRateOverTime = Mathf.Max(40f, fireRateOverTime - (2f * Time.deltaTime));
        ringsRateOverTime = Mathf.Max(10f, ringsRateOverTime - (10f / 3f * Time.deltaTime));
        embersSmallRateOverTime = Mathf.Max(40f, embersSmallRateOverTime - (3f * Time.deltaTime));
        embersFlickeringRateOverTime = Mathf.Max(10f, embersFlickeringRateOverTime - (3f * Time.deltaTime));
        fireMainsRateOverTime = Mathf.Max(10f, fireMainsRateOverTime - (3f * Time.deltaTime));

        // Decrease Fire Mains scale every 3 seconds
        Vector3 decreaseScale = new Vector3(0.175f, 0.175f, 0.175f) * Time.deltaTime;
        fireMains.transform.localScale = Vector3.Max(fireMainsOriginalScale * 0.5f, fireMains.transform.localScale - decreaseScale);

        // Update the emissions
        fireEmission.rateOverTime = fireRateOverTime;
        ringsEmission.rateOverTime = ringsRateOverTime;
        embersSmallEmission.rateOverTime = embersSmallRateOverTime;
        embersFlickeringEmission.rateOverTime = embersFlickeringRateOverTime;
        fireMainsEmission.rateOverTime = fireMainsRateOverTime;
    }

    public void OnWoodAdded()
    {
        // Increase rate over time
        fireRateOverTime = 400f;
        ringsRateOverTime += 25f;
        embersSmallRateOverTime = 300f;
        embersFlickeringRateOverTime = Mathf.Max(50f, embersFlickeringRateOverTime);
        fireMainsRateOverTime = Mathf.Max(25f, fireMainsRateOverTime);

        // Increase Fire Mains scale
        fireMains.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);

        // Apply changes instantly
        fireEmission.rateOverTime = fireRateOverTime;
        ringsEmission.rateOverTime = ringsRateOverTime;
        embersSmallEmission.rateOverTime = embersSmallRateOverTime;
        embersFlickeringEmission.rateOverTime = embersFlickeringRateOverTime;
        fireMainsEmission.rateOverTime = fireMainsRateOverTime;

        // Start the co-routines to decrease values after 2 seconds
        StartCoroutine(DecreaseAfterSeconds(fireEmission, 400f, 10f, 2f));
        StartCoroutine(DecreaseAfterSeconds(ringsEmission, ringsRateOverTime, 10f, 2f));
        StartCoroutine(DecreaseAfterSeconds(embersSmallEmission, 300f, 15f, 2f));
        StartCoroutine(DecreaseAfterSeconds(embersFlickeringEmission, embersFlickeringRateOverTime, 10f, 2f));
        StartCoroutine(DecreaseAfterSeconds(fireMainsEmission, fireMainsRateOverTime, 8f, 2f));
    }

    private IEnumerator DecreaseAfterSeconds(ParticleSystem.EmissionModule emission, float startingValue, float decreaseValue, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        emission.rateOverTime = startingValue - decreaseValue;
    }
}