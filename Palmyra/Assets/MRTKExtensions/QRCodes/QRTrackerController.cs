using System.Collections;
using System;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;

namespace MRTKExtensions.QRCodes
{
    public class QRTrackerController : MonoBehaviour
    {
        [SerializeField]
        GameObject instructions;

        [SerializeField]
        private SpatialGraphCoordinateSystemSetter spatialGraphCoordinateSystemSetter;

        [SerializeField]
        private string locationQrValue = string.Empty;

        private Transform markerHolder;
        private AudioSource audioSource;
        private GameObject markerDisplay;
        private QRInfo lastMessage;
        private int trackingCounter;

        bool isUpdateTracking = false;

        public bool IsTrackingActive { get; private set; } = true;

        private IQRCodeTrackingService qrCodeTrackingService;
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
            if (!QRCodeTrackingService.IsSupported)
            {
                return;
            }

            markerHolder = spatialGraphCoordinateSystemSetter.gameObject.transform;
            markerDisplay = markerHolder.GetChild(0).gameObject;
            markerDisplay.SetActive(false);

            audioSource = markerHolder.gameObject.GetComponent<AudioSource>();

            QRCodeTrackingService.QRCodeFound += ProcessTrackingFound;
            spatialGraphCoordinateSystemSetter.PositionAcquired += SetScale;
            spatialGraphCoordinateSystemSetter.PositionAcquisitionFailed += 
                (s,e) => ResetTracking();


            if (QRCodeTrackingService.IsInitialized)
            {
                StartTracking();
            }
            else
            {
                QRCodeTrackingService.Initialized += QRCodeTrackingService_Initialized;
            }
        }

        IEnumerator CallTrackerUpdate() //Added to keep active the QR Code Tracker in the Background @Remove if issue in QR Tracking
        {
            while(isUpdateTracking)
            {
                yield return new WaitForSeconds(3);
                if (QRCodeTrackingService.IsInitialized)
                {
                    Debug.Log("<<<<<<<<<<<QR TIME TRACKER ACTIVE AT QRTrackerController Script>>>>>>>>>>>>>>>>>>");
                    IsTrackingActive = true;
                    trackingCounter = 0;
                }
            }
        }

        private void QRCodeTrackingService_Initialized(object sender, EventArgs e)
        {
            StartTracking();
        }

        private void StartTracking()
        {
            QRCodeTrackingService.Enable();
        }

        public void ResetTracking()
        {
            if (QRCodeTrackingService.IsInitialized)
            {
                markerDisplay.SetActive(false);
                IsTrackingActive = true;
                instructions.SetActive(true); //activate instructions
                trackingCounter = 0;
                isUpdateTracking = false;
            }
        }

        private void ProcessTrackingFound(object sender, QRInfo msg)
        {
            if ( msg == null || !IsTrackingActive)
            {
                return;
            }

            lastMessage = msg;

            if (msg.Data == locationQrValue)
            {
                if (trackingCounter++ == 2)
                {
                    IsTrackingActive = false;
                    if(!isUpdateTracking)
                        StartCoroutine(CallTrackerUpdate());
                    isUpdateTracking = true;
                    spatialGraphCoordinateSystemSetter.SetLocationIdSize(msg.SpatialGraphNodeId,
                        msg.PhysicalSideLength);
                }
            }
        }


        private void SetScale(object sender, Pose pose)
        {
            markerHolder.localScale = Vector3.one * lastMessage.PhysicalSideLength;
            markerDisplay.SetActive(true);
            PositionSet?.Invoke(this, pose);
            //if(!isUpdateTracking)
                audioSource.Play();
        }

        public EventHandler<Pose> PositionSet;
    }
}
