using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentController : MonoBehaviour
{
    [SerializeField] Animator monumentAnimator;

    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float m_CurrentClipLength;
    float timer;
    // Use this for initialization
    void Start()
    {
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = monumentAnimator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
        print(m_CurrentClipLength);
        timer = (1 / m_CurrentClipLength) / 60;
    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        //monumentAnimator.SetFloat("Blend", eventData.NewValue);
        monumentAnimator.Play("Main", 0, eventData.NewValue);
    }
}
