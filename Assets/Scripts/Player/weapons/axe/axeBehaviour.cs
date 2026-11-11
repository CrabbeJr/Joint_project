using UnityEngine;

public class axeBehaviour : MonoBehaviour
{

    private GameObject gameObj;
    private Rigidbody2D rb;
    private Vector2 aim;
    public float damage = 10;
    public float throwForce = 200;
    public float gravity = 5;

    private AttackManager AM;

    void Awake()
    {
        AM = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        rb = GetComponent<Rigidbody2D>();
        //gameObj = GetComponent<GameObject>();
        aim = Vector2.Normalize(new Vector2(Random.Range(-3, 3), Random.Range(5, 10)));

        rb.AddForce(aim * throwForce);
    }



    void FixedUpdate()
    {
        rb.AddForce(Vector2.down * gravity);

        if (AM.NotOnScreen(gameObject))
        {
            Destroy(this.gameObject);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().health -= damage;
        }
    }

    
}
