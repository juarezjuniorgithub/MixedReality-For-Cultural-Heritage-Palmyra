using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.GettingStarted
{
    public class PartAssemblyController : MonoBehaviourPun
    {
        public delegate void PartAssemblyControllerDelegate();

        [SerializeField] private Transform locationToPlace = default;

        ObjectManipulator objectManipulator;

        private const float MinDistance = 0.001f;
        public  float MaxDistance = 0.1f;

        private bool shouldCheckPlacement;

        private AudioSource audioSource;
        private ToolTipSpawner toolTipSpawner;
        private List<Collider> colliders;
        //private List<PartAssemblyController> partAssemblyControllers;

        private Transform originalParent;
        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private IEnumerator checkPlacementCoroutine;

        private bool hasAudioSource;
        private bool hasToolTip;

        private bool isPlaced;
        private bool isResetting;
        public Material mainStoneMat;

        private void Awake()
        {
            objectManipulator = GetComponent<ObjectManipulator>();

            // Check if object should check for placement
            if (locationToPlace != transform) shouldCheckPlacement = true;

            // Cache references
            audioSource = GetComponent<AudioSource>();
            toolTipSpawner = GetComponent<ToolTipSpawner>();

            colliders = new List<Collider>();
            if (shouldCheckPlacement)
                foreach (var col in GetComponents<Collider>())
                    colliders.Add(col);

            var trans = transform;
            originalParent = trans.parent;
            originalPosition = trans.localPosition;
            originalRotation = trans.localRotation;

            checkPlacementCoroutine = CheckPlacement();

            // Check if object has audio source
            hasAudioSource = audioSource != null;

            // Check if object has tool tip
            hasToolTip = toolTipSpawner != null;

            // Start coroutine to continuously check if the object has been placed
            //if (shouldCheckPlacement) StartCoroutine(checkPlacementCoroutine);
        }

        private void OnEnable()
        {
            if (shouldCheckPlacement) StartCoroutine(checkPlacementCoroutine);

        }

        public void SetPlacement()
        {
            objectManipulator.ForceEndManipulation();

            // Update placement state
            isPlaced = true;
            Debug.Log("Placed");

            // Play audio snapping sound
            if (hasAudioSource) audioSource.Play();

            // Disable ability to manipulate object
            foreach (var col in colliders) col.enabled = false;

            // Disable tool tips
            if (hasToolTip) toolTipSpawner.enabled = false;

            // Set parent and placement of object to target
            var trans = transform;
            trans.SetParent(locationToPlace.parent);
            trans.position = locationToPlace.position;
            trans.rotation = locationToPlace.rotation;
            locationToPlace.gameObject.SetActive(false);

            if (isPlaced)
            {
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                meshRenderer.material = mainStoneMat;
            }
        }

        /// <summary>
        ///     Triggers the reset placement feature.
        ///     Hooked up in Unity.
        /// </summary>
        public void ResetPlacement()
        {
            Reset();
        }

        /// <summary>
        ///     Resets the part's parent and placement.
        /// </summary>
        public void Reset()
        {
            // Update placement state
            isPlaced = false;

            // Enable ability to manipulate object
            //foreach (var col in colliders) col.enabled = true;
            GetComponent<Collider>().enabled = true;
            // Enable tool tips
            if (hasToolTip) toolTipSpawner.enabled = true;

            // Reset parent and placement of object
            var trans = transform;
            trans.SetParent(originalParent);
            trans.localPosition = originalPosition;
            trans.localRotation = originalRotation;
        }

        /// <summary>
        ///     Checks the part's position and snaps/keeps it in place if the distance to target conditions are met.
        /// </summary>
        private IEnumerator CheckPlacement()
        {
            while (true)
            {
                MaxDistance = (MonumentScale.currentScale.x * 0.0071428571428571f);
                yield return new WaitForSeconds(0.01f);

                if (!isPlaced)
                {
                    if (Vector3.Distance(transform.position, locationToPlace.position) > MinDistance &&
                        Vector3.Distance(transform.position, locationToPlace.position) < MaxDistance)
                        SetPlacement();
                }
                else if (isPlaced)
                {
                    if (!(Vector3.Distance(transform.position, locationToPlace.position) > MinDistance)) continue;
                    var trans = transform;
                    trans.position = locationToPlace.position;
                    trans.rotation = locationToPlace.rotation;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        ///     Raised when RestPlacement is called and PUN is enabled.
        /// </summary>
        public event PartAssemblyControllerDelegate OnResetPlacement;

        /// <summary>
        ///     Raised when SetPlacement is called and PUN is enabled.
        /// </summary>
        public event PartAssemblyControllerDelegate OnSetPlacement;

        public void ChangeToGreen()
        {
            locationToPlace.GetComponent<Renderer>().material.color = Color.green;
            locationToPlace.GetComponent<FadeInFadeOut>().material = locationToPlace.GetComponent<Renderer>().material;
            //locationToPlace.GetComponent<FadeInFadeOut>().StopFadeInFadeOut();
        }

        public void ChangeToBlue()
        {
            locationToPlace.GetComponent<Renderer>().material.color = Color.cyan;
            //locationToPlace.GetComponent<FadeInFadeOut>().StartFadeInFadeOut();
        }
    }

}
