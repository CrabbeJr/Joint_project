using UnityEngine;
using System.Collections;

public class ShearBehaviour : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 aim, randAim; //aim is the direction the projectile should go in when thrown, randAim is used to work out which direction to go when no enemies are nearby
    private Vector2 backAim; //aim for going backwards
    public float initialForce = 5f;
    public float backAcceleration = 1f;
    public int damage = 5;
    private AttackManager AM;
    

    //function to find nearest enemy. found on https://discussions.unity.com/t/clean-est-way-to-find-nearest-object-of-many-c/409917/2
    //written by "tomvds"
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null; //initializes the transform component of the closest enemy
        float minDist = Mathf.Infinity; //initializes the distance from said enemy
        
        //search algorithm sorting through all enemies within current instance.
        foreach (GameObject t in enemies) //loops trhough enemies in scene
        {
            float dist = Vector2.Distance(t.transform.position, transform.position);
            if (dist <minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

private void Awake()
    {

        AM = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        
        rb = this.GetComponent<Rigidbody2D>();

        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            Vector2 nearbyEnemy = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy")).position;
            aim = Vector2.Normalize(nearbyEnemy);
        }
        else
        {
            randAim = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
            aim = Vector2.Normalize(randAim);
        }
        backAim = (-1) * aim;
        rb.AddForce(aim * initialForce);
    }

    private void FixedUpdate()
    {
        rb.AddForce(backAim * backAcceleration);
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
