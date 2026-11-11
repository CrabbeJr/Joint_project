using System.Collections;
using UnityEngine;
using static PlayerScript;

public class ShearBehaviour : MonoBehaviour
{
    private GameObject playerObj;
    private Rigidbody2D playerRb;
    private PlayerScript.playerStats PlayerStats;
    private Rigidbody2D rb;
    private Vector2 aim, randAim; //aim is the direction the projectile should go in when thrown, randAim is used to work out which direction to go when no enemies are nearby
    private Vector2 backAim; //aim for going backwards
    public float initialForce = 5f;
    public float backAcceleration = 1f;
    

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
            Debug.Log(t.gameObject.tag);
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
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currentStats;
        rb = GetComponent<Rigidbody2D>();

        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            Debug.Log(GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy")).position);
            Vector2 nearbyEnemy = (GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy")).position - playerObj.transform.position);
            aim = Vector2.Normalize(nearbyEnemy);
        }
        else
        {
            randAim = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
            aim = Vector2.Normalize(randAim);
        }
        backAim = (-1) * aim;
        rb.AddForce((aim * initialForce * PlayerStats.weaponSpeed) + playerRb.linearVelocity);
    }

    private void FixedUpdate()
    {
        rb.AddForce(backAim * backAcceleration * PlayerStats.weaponSpeed);
    }
}
