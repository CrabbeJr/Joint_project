using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform transformPlayer; 
    [Serializable] public class playerStats
    {
        public float health = 50;
        public float moveSpeed = 0.05f;
        public float weaponCooldown = 1f;
        public float weaponArea = 1f;
        public float moveSpeedMultiplier = 1f;
        public float weaponSpeed = 1f;
        public float weaponDamage = 1f;
        public float weaponDuration = 1f;
        public float playerRegen = 0f;
        public float playerLuck = 0f;
        public float playerXpGain = 0f;
        public float playerPickupRangeMultiplier = 1f;
        public float weaponAdditionalProjectileCount = 0f;
        public bool[] weaponsOwned = new bool[4];
    }

    public int playerCharacter = 0;

    public playerStats[] characterStats;
    public playerStats currentStats;

    public bool[] weaponsOwned = { false, false, false};
    private bool[] weaponsStarted = { false, false , false};

    private AttackManager attackManager;


    void Start()
    {
        currentStats = characterStats[playerCharacter];
        attackManager = this.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        float moveSpeedReal = currentStats.moveSpeed * currentStats.moveSpeedMultiplier;
        transformPlayer.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeedReal, Input.GetAxis("Vertical") * moveSpeedReal, 0);
        
        
        //weapon management
        for (int i = 0; i < currentStats.weaponsOwned.GetLength(0); i++)
        {
            if (currentStats.weaponsOwned[i] && !weaponsStarted[i])
            {
                switch (i)
                {
                    case 0:
                        attackManager.StartCoroutine("StartStrimmer");
                        weaponsStarted[i] = true;
                    break;
                    
                    case 1:
                        attackManager.StartCoroutine("StartShears");
                        weaponsStarted[i] = true;
                    break;

                    case 2:
                        attackManager.StartCoroutine("StartAxes");
                        weaponsStarted[i] = true;
                    break;
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy.downTime <= 0)
            {
                enemy.downTime = 0.2f;
                takeDamage(enemy.damage);
            }
        }
    }
    private void takeDamage(float damage)
    {
        currentStats.health -= damage;
        if (currentStats.health <= 0)
        {
            Debug.Log("DED");
        }
    }
}
