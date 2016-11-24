using UnityEngine;


namespace Assets.Core
{
    

    public class TrackingCamera : MonoBehaviour
    {
        public GameObject target;

        private Vector3 offset;


        public float damping = 1f;


        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            offset = transform.position - target.transform.position;
        }


        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// It is called after all Update functions have been called.
        /// </summary>
        void LateUpdate()
        {
            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 lerpPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.position = lerpPosition;
            
            transform.LookAt(target.transform.position);
        }
        
    }


}