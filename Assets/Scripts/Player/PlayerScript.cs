using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private string[] rewardsStored;
    private GameObject buttons;
    public Transform transformPlayer;
    [Serializable] public class weaponStats
    {
        public int additionalProjectiles = 0; 
    };
    [Serializable] public class playerStats
    {
        public float health = 50;
        public float moveSpeed = 0.05f;
        public float moveSpeedMultiplier = 1f;
        public float weaponCooldown = 1f;
        public float weaponArea = 1f;
        public float weaponSpeed = 1f;
        public float weaponDamage = 1f;
        public float weaponDuration = 1f;
        public float playerRegen = 0f;
        public float playerLuck = 0f;
        public float playerXpGain = 0f;
        public float playerPickupRangeMultiplier = 1f;
        public float weaponAdditionalProjectileCount = 0f;
        public bool[] weaponsOwned = new bool[4];
        public int level;
        public float xp;
    }

    public int playerCharacter = 0;

    public playerStats[] characterStats;
    public playerStats currentStats;
    public weaponStats[] weaponStat;
    private bool[] weaponsStarted = { false, false , false};

    private AttackManager attackManager;

    //variables for player movement
    public Vector3 velocity, pastPos;


    void Start()
    {
        buttons = GameObject.FindGameObjectWithTag("UI");
        buttons.gameObject.SetActive(false);
        pastPos = new Vector3(0, 0, 0);
        currentStats = characterStats[playerCharacter];
        attackManager = this.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    int xpNeeded(int level)
    {
        return Math.Pow((level * 50),1.05).ConvertTo<int>();
    }
    List<string> selectPotentialRewards()
    {
        List<string> potentialRewards = new List<string>();
        string[] regularRewards =
        {
            "all damage",
            "all speed (weapon)",
            "all projectiles"
        };
        for (int i = 0; i < weaponsStarted.Length; i++)
        {
            if (!currentStats.weaponsOwned[i])
            {
                switch (i)
                {
                    case 0:
                        potentialRewards.Add("strimmer");
                        break;
                    case 1:
                        potentialRewards.Add("shear");
                        break;
                    case 2:
                        potentialRewards.Add("axe");
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 1:
                        potentialRewards.Add("shear Projectiles");
                        break;
                    case 2:
                        potentialRewards.Add("axe Projectiles");
                        break;
                }
            }
        }
        for (int i = 0; i < regularRewards.Length; i++)
        {
            potentialRewards.Add(regularRewards[i]);
        }
        return potentialRewards;
    }
    private void FixedUpdate()
    {
        float moveSpeedReal = currentStats.moveSpeed * currentStats.moveSpeedMultiplier;
        transformPlayer.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeedReal, Input.GetAxis("Vertical") * moveSpeedReal, 0);

        velocity = (transformPlayer.position - pastPos) / Time.deltaTime;
        pastPos = transformPlayer.position;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Xp")
        {
            currentStats.xp += 30 * currentStats.playerXpGain;
            levelUp();
            Destroy(collision.gameObject);   
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
    private void levelUp()
    {
        if (currentStats.xp > xpNeeded(currentStats.level))
        {

            Time.timeScale = 0.0f;

            currentStats.xp -= xpNeeded(currentStats.level);
            currentStats.level++;
            List<string> rewards = selectPotentialRewards();
            buttons.gameObject.SetActive(true);
            GameObject button1 = buttons.transform.GetChild(0).gameObject;
            GameObject button2 = buttons.transform.GetChild(1).gameObject;
            GameObject button3 = buttons.transform.GetChild(2).gameObject;

            bool randomNumsFound = false;
            int[] randomNums = { -1, -1, -1 };

            while (!randomNumsFound)
            {
                randomNums[0] = UnityEngine.Random.Range(0,rewards.Count);
                randomNums[1] = UnityEngine.Random.Range(0, rewards.Count);
                randomNums[2] = UnityEngine.Random.Range(0, rewards.Count);
                if (allUnique3(randomNums) || rewards.Count < 3)
                {
                    randomNumsFound = true;
                }
            }

            rewardsStored = new string[] { rewards[randomNums[0]], rewards[randomNums[1]], rewards[randomNums[2]] };

            button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardsStored[0];
            button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardsStored[1];
            button3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardsStored[2];
        }
    }
    private bool allUnique3(int[] nums)
    { 
        return nums[0] != nums[1] && nums[0] != nums[2] && nums[1] != nums[2];
    }

    public void rewardTaken(int rewardTakenNum)
    {
        Debug.Log("AAAA");
        switch (rewardsStored[rewardTakenNum])
        {
            case "all damage":
                currentStats.weaponDamage += 0.05f;
                break;
            case "all speed (weapon)":
                currentStats.weaponSpeed += 0.05f;
                break;
            case "all projectiles":
                currentStats.weaponAdditionalProjectileCount++;
                break;
            case "shear":
                currentStats.weaponsOwned[1] = true;
                break;
            case "strimmer":
                currentStats.weaponsOwned[0] = true;
                break;
            case "axe":
                currentStats.weaponsOwned[2] = true;
                break;
            case "axe Projectiles":
                weaponStat[2].additionalProjectiles++;
                break;
            case "shear Projectiles":
                weaponStat[1].additionalProjectiles++;
                break;
        }
    }
}
