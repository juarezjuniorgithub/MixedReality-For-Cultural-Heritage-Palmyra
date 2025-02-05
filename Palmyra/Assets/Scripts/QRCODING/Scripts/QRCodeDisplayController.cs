﻿using System;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Audio;
using MRTKExtensions.QRCodes;
using TMPro;
using UnityEngine;
using B83;

public class QRCodeDisplayController : MonoBehaviour
{
    [SerializeField]
    private float qrObservationTimeOut = 3500;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private TextMeshPro displayText;

    [SerializeField]
    private AudioSource confirmSound;

    private IQRCodeTrackingService qrCodeTrackingService;

    private QRInfo lastSeenCode;

    [SerializeField]
    private QRTrackerController qrTrackerController;

    private bool qrCodeAlreadyDetected = false;

    //Text to speech
    [SerializeField] private TextToSpeech textToSpeech;
    [SerializeField] private TextMeshPro instructionsText;
    private float repeatTime = 20;
    private float startingTime;

    private IQRCodeTrackingService QRCodeTrackingService
    {
        get
        {
            while (!MixedRealityToolkit.IsInitialized && Time.time < 5) ;
            return qrCodeTrackingService ??
                   (qrCodeTrackingService = MixedRealityToolkit.Instance.GetService<IQRCodeTrackingService>());
        }
    }

    private void Start()
    {
        menu.SetActive(false);
        if (!QRCodeTrackingService.IsSupported)
        {
            return;
        }

        if (QRCodeTrackingService.IsInitialized)
        {
            StartTracking();
        }
        else
        {
            QRCodeTrackingService.Initialized += QRCodeTrackingService_Initialized;
        }

        startingTime = Time.time;

        //Text to speech message after 3 seconds from starting.
        Run.After(3, () => textToSpeech.StartSpeaking(instructionsText.text));
    }


    private void QRCodeTrackingService_Initialized(object sender, EventArgs e)
    {
        StartTracking();
    }

    private void StartTracking()
    {
        menu.SetActive(true);
        QRCodeTrackingService.QRCodeFound += QRCodeTrackingService_QRCodeFound;
        QRCodeTrackingService.Enable();
    }

    private void QRCodeTrackingService_QRCodeFound(object sender, QRInfo codeReceived)
    {
        if (lastSeenCode?.Data != codeReceived.Data)
        {
            if(codeReceived.Data == qrTrackerController.locationQrValue && !qrCodeAlreadyDetected) {
                //displayText.text = $"code observed: {codeReceived.Data}";
                if (confirmSound.clip != null)
                {
                    confirmSound.Play();
                }
                qrCodeAlreadyDetected = true;
                menu.SetActive(false);
            }
        }
        lastSeenCode = codeReceived;
    }

    private void Update()
    {
        if (lastSeenCode == null)
        {
            return;
        }
        if (Math.Abs(
            (lastSeenCode.LastDetectedTime.UtcDateTime - DateTimeOffset.UtcNow).TotalMilliseconds) >
              qrObservationTimeOut)
        {
            lastSeenCode = null;
            displayText.text = string.Empty;
        }
        //only if windows platform
#if WINDOWS_UWP
        if(Time.time - startingTime > repeatTime) {
            textToSpeech.StartSpeaking(instructionsText.text);
            startingTime = Time.time;
        }
#endif
    }
}
