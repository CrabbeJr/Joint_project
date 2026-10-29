using UnityEngine;


namespace cameraController
{
    public class CameraController : MonoBehaviour
    {
        //refrences player position
        [SerializeField] private Transform playerTransform;
        private float zPosition = -10;

        private void Update()
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, zPosition);
        }
    }
}