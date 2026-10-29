using UnityEngine;

namespace strimmer.follow
{
    public class StrimmerFollow : MonoBehaviour
    {

        [SerializeField] private Transform playerTransform;

        void Update()
        {
            transform.position = playerTransform.position;
        }
    }
}
