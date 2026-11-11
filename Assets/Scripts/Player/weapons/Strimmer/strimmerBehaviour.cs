using Unity.VisualScripting;
using UnityEngine;

namespace strimmer.follow
{
    public class StrimmerBehaviour : MonoBehaviour
    {

        private GameObject player;
        private string objectName;
        public double maxTimeAlive = 0.1;
        public float damage = 2;

        public int iframesGiven = 5;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            objectName = this.gameObject.name;
        }
        void Update()
        {
            if (objectName == "strimmerAttack(Clone)")
            {
                maxTimeAlive -= Time.deltaTime;
                if (maxTimeAlive <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            transform.position = player.transform.position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {          
                collision.gameObject.GetComponent<EnemyScript>().health -= damage;
            }
        }
    }
}
