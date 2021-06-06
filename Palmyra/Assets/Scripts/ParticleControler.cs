using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ParticleControler : MonoBehaviour
{
    [SerializeField] ParticleSystemReverseSimulation sandParticles;
    [SerializeField] PinchSlider pinchSlider;
    [SerializeField] float refresthRate = 0.1f;
    [SerializeField] float smoothening = 5;
    float currentSliderValue;
    float oldSliderValue;
    private float delta;
    private float target;

    private void Update()
    {
        if(delta < 0)
        {
            target = 1 - (delta * 100);
        }
        else
        {
            target = -1 - (delta * 100);
        }

        target = -1 - (delta * 100);
        sandParticles.simulationSpeedScale = Mathf.Lerp(sandParticles.simulationSpeedScale, target, Time.deltaTime * smoothening);
    }

    private void Start()
    {
        StartCoroutine(Refresh());
    }

    IEnumerator Refresh()
    {
        while (true)
        {
            currentSliderValue = pinchSlider.SliderValue;
            delta = currentSliderValue - oldSliderValue;
            yield return new WaitForSeconds(refresthRate);
            oldSliderValue = currentSliderValue;
        }
    }
}
