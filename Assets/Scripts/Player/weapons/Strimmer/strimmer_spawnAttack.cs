using UnityEngine;

namespace strimmer.spawnAttack
{
    public class strimmer_spawnAttack : MonoBehaviour
    {
        
        [SerializeField] private Transform strimmerTransform;
        public GameObject strimmerAttack;
        public float cooldown = 0.5f;

        private void start()
        {
            InvokeRepeating(nameof(spawnAttack), 0, cooldown);
        }

        private void spawnAttack()
        {
            Instantiate(strimmerAttack, strimmerTransform.position, Quaternion.identity);
            Destroy(strimmerAttack);
        }

    }

}
