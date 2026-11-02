using System;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.U2D.Physics.PhysicsWorld;

public class EnemyScript : MonoBehaviour
{
    public Transform enemyTransform;
    private GameObject player;
    public float moveSpeed = 0.1f;
    public float health = 10;
    public float damage = 1;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position,player.transform.position,moveSpeed);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
