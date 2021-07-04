using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class GenericNetSync : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private bool isUser = default;

        private Camera mainCamera;

        private Vector3 networkLocalPosition;
        private Quaternion networkLocalRotation;

        private Vector3 startingLocalPosition;
        private Quaternion startingLocalRotation;
        public float speed = 1.0F;

        // Time when the movement started.
        //private float startTime;

        // Total distance between the markers.
        //private float journeyLength;

        void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.localPosition);
                stream.SendNext(transform.localRotation);
            }
            else
            {
                networkLocalPosition = (Vector3) stream.ReceiveNext();
                networkLocalRotation = (Quaternion) stream.ReceiveNext();
            }
        }

        private void Start()
        {
            if (isUser)
            {
                if (TableAnchor.Instance != null) transform.parent = FindObjectOfType<TableAnchor>().transform;

                if (photonView.IsMine) GenericNetworkManager.Instance.localUser = photonView;
            }

            var trans = transform;
            startingLocalPosition = trans.localPosition;
            startingLocalRotation = trans.localRotation;

            networkLocalPosition = startingLocalPosition;
            networkLocalRotation = startingLocalRotation;
            // Keep a note of the time the movement started.
            //startTime = Time.time;

            // Calculate the journey length.
            //journeyLength = Vector3.Distance(startingLocalPosition, networkLocalPosition);
        }

        // private void FixedUpdate()
        private void Update()
        {
            // Distance moved equals elapsed time times speed..
            //float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            //float fractionOfJourney = distCovered / journeyLength;
            if (mainCamera==null)
            {
                mainCamera = Camera.main;
            }
            if (!photonView.IsMine)
            {
                var trans = transform;
                //trans.localPosition = networkLocalPosition;
                //trans.localRotation = networkLocalRotation;
                trans.localPosition = Vector3.Lerp(trans.localPosition, networkLocalPosition, Time.deltaTime * 5);
                trans.localRotation = Quaternion.Lerp(trans.localRotation, networkLocalRotation, Time.deltaTime * 5);
            }

            if (photonView.IsMine && isUser)
            {
                var trans = transform;
                var mainCameraTransform = mainCamera.transform;
                trans.position = mainCameraTransform.position;
                //transform.position = Vector3.Lerp(startingLocalPosition, networkLocalPosition, fractionOfJourney);
                trans.rotation = mainCameraTransform.rotation;
            }
        }
    }
}
