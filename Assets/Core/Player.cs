using UnityEngine;


namespace Assets.Core
{
    

    public class Player : MonoBehaviour
    {
        public float movementSpeed = 2.0f;
        public float turningSpeed = 60f;


        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            float horizonatal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
            transform.Rotate(0, horizonatal, 0);

            float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, vertical);
        }
    }


}