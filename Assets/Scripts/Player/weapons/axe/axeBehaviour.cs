using UnityEngine;

public class axeBehaviour : MonoBehaviour
{

    private GameObject gameObj;
    private Rigidbody2D rb;
    private Vector2 aim;
    public float damage = 10;
    public float throwForce = 125;
    private float gravity = 5;

    private AttackManager AM;
    private PlayerScript.playerStats PlayerStats;

    void Awake()
    {
        AM = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        rb = GetComponent<Rigidbody2D>();
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currentStats;

        aim = Vector2.Normalize(new Vector2(Random.Range(-3, 3), Random.Range(5, 10)));
        Debug.Log(aim);

        rb.AddForce(new Vector2((aim.x * throwForce), (aim.y * throwForce * Mathf.Pow(PlayerStats.weaponSpeed, 1/2))));
    }



    void FixedUpdate()
    {
        rb.AddForce(Vector2.down * gravity * Mathf.Pow(PlayerStats.weaponSpeed, 1/3));

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
