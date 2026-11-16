using UnityEngine;

public class axeBehaviour : MonoBehaviour
{

    private GameObject gameObj;
    private Rigidbody2D rb;
    private Vector2 aim;
    public float throwForce = 125;
    private float gravity = 5;
    public float damage = 10;

    private AttackManager AM;
    private PlayerScript playerScr;
    private PlayerScript.playerStats stats;
    private int currentCharacter;

    void Awake()
    {
        AM = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        playerScr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        currentCharacter = playerScr.playerCharacter;
        stats = playerScr.characterStats[currentCharacter];

        rb = GetComponent<Rigidbody2D>();
        damage *= stats.weaponDamage;

        aim = Vector2.Normalize(new Vector2(Random.Range(-3, 3), Random.Range(5, 10)));
        Vector2 playerVelocity = new Vector2(playerScr.velocity.x, playerScr.velocity.y);

        rb.AddForce((new Vector2((aim.x * throwForce), (aim.y * throwForce * Mathf.Pow(stats.weaponSpeed, 1/2)))) + (playerVelocity * throwForce / 2));
    }



    void FixedUpdate()
    {
        rb.AddForce(Vector2.down * gravity * Mathf.Pow(stats.weaponSpeed, 1/3));

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
