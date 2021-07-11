using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepsController : MonoBehaviour
{
    [SerializeField] int[] stepYears;
    [SerializeField] int threshold = 50;
    [SerializeField] Animator[] images;
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] bool playAudio;

    private void Start()
    {
        foreach (var item in images)
        {
            item.SetFloat("Blend", 0);
        }
        if (!playAudio) {
            foreach (var item in audioSources) {
                Destroy(item);
            }
        }
    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        UpdateImageAndAudio(Mathf.Round(eventData.NewValue * 2020 + 1));
    }

    void UpdateImageAndAudio(float currYear)
    {
        for (int i = 0; i < stepYears.Length; i++)
        {
            if (currYear >= stepYears[i] - threshold && currYear <= stepYears[i] + threshold)
            {
                float v = Mathf.InverseLerp(threshold, 0, Mathf.Abs(stepYears[i] - currYear));
                images[i].SetFloat("Blend", v);
                if (playAudio) {
                    if(audioSources[i].clip != null)
                    {
                        if (!audioSources[i].isPlaying)
                        {
                            audioSources[i].Play();
                        }
                        audioSources[i].volume = v;
                    }
                }
            }
            else
            {
                images[i].SetFloat("Blend", 0);
                if (playAudio) {
                    if(audioSources != null)
                    {
                        if (audioSources[i].isPlaying)
                        {
                            audioSources[i].Stop();
                        }
                        audioSources[i].volume = 0;
                    }
                }
            }
        }
    }
}
