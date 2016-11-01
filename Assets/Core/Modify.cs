using Assets.Core.Tiles;
using UnityEngine;

namespace Assets.Core
{
    public class Modify : MonoBehaviour
    {
        Vector2 rot;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
                    Terrain.SetTile(hit, new AirTile());
            }

            rot = new Vector2(
                rot.x + Input.GetAxis("Mouse X") * 3,
                rot.y + Input.GetAxis("Mouse Y") * 3);

            transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);
    
            transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
            transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
        }
    }
}