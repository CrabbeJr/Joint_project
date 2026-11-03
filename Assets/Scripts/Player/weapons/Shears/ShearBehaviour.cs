using UnityEngine;

public class ShearBehaviour : MonoBehaviour
{

    private Rigidbody rb;
    private float initialForce = 5f;
    public float backAcceleration = 1f;

    //function to find nearest enemy. found on https://discussions.unity.com/t/clean-est-way-to-find-nearest-object-of-many-c/409917/2
    //written by "tomvds"
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null; //initializes the transform component of the closest enemy
        float minDist = Mathf.Infinity; //initializes the distance from said enemy
        
        //search algorithm sorting through all enemies within current instance.
        foreach (Transform t in enemies) //loops trhough enemies in scene
        {
            float dist = Vector3.Distance(t.position, transform.position);
            
            if (dist <minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    //commented just now as I'm stopping for today (:
    /*private void Awake()
    {
        Transform[] nearbyEnemies = Find
        Vector3 nearestEnemy = GetClosestEnemy()
        rb.AddForce()
    }*/
}
