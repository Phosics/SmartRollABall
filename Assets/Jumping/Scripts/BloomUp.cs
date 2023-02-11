using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomUp : MonoBehaviour
{
    private Bloom myBloom;
    private bool beingHandled = false;
    public Light globalLight;

    private float globalLightIntensity;
    // Start is called before the first frame update
    void Start()
    {
        globalLightIntensity = globalLight.intensity;
        gameObject.GetComponent<Volume>().profile.TryGet(out myBloom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaceBloom()
    {
        if (!beingHandled)
        {
            StartCoroutine(HandleIt());
        }
    }

    private IEnumerator HandleIt()
    {
        beingHandled = true;
        globalLight.intensity = 0f;
        myBloom.intensity.value = 10f;
        // process pre-yield
        yield return new WaitForSeconds(2.0f);
        // process post-yield
        globalLight.intensity = globalLightIntensity;
        myBloom.intensity.value = 1f;
        beingHandled = false;
    }
}
