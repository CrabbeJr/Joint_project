using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject spawnedStrimmerAttack = null;
    public GameObject strimmerAttack;
    public GameObject strimmerObject;
    public GameObject shearObject;
    public GameObject axeObject;
    private PlayerScript playerStats;

    //cooldowns - could mabye be turned into one variable later and have each weapon have their cooldown dictated by some function of value
    public float strimmerCooldown = 0.5f;
    public float shearCooldown = 1f;

    private float timeBetweenSpawns = 0.1f;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerScript>();
        timeBetweenSpawns = 0.1f;
    }

    public bool NotOnScreen(GameObject gameObj)
    {
        //taken from https://discussions.unity.com/t/detecting-if-a-visual-element-is-off-screen/880405

        var pos = Camera.main.WorldToScreenPoint(gameObj.transform.position);
        bool isOffscreen = pos.x <= 0 || pos.x >= Screen.width || pos.y <= 0 || pos.y >= Screen.height;

        return isOffscreen;
    }



    private void StartStrimmer()
    {
        Instantiate(strimmerObject, transform.position, Quaternion.identity);
        InvokeRepeating(nameof(spawnAttackStrimmer), 0, strimmerCooldown);
    }

    private void spawnAttackStrimmer()
    {
        spawnedStrimmerAttack = Instantiate(strimmerAttack, transform.position, Quaternion.identity);
    }

    //function to throw shear
    private IEnumerator throwShear()
    {
        while(true)
        {
            Debug.Log("RAH?");
            for (int i = 0; i <= playerStats.currentStats.weaponAdditionalProjectileCount + playerStats.weaponStat[1].additionalProjectiles; i++)
            {
                if (GameObject.FindGameObjectWithTag("Enemy") != null)
                {
                    Instantiate(shearObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
            yield return new WaitForSeconds(shearCooldown);
        }
    }

    //function to throw shears repeatedly
    private void StartShears()
    {
        StartCoroutine("throwShear");
    }


    private IEnumerator throwAxe()
    {
        while (true)
        {
            Debug.Log("RAH?");
            for (int i = 0; i <= playerStats.currentStats.weaponAdditionalProjectileCount + playerStats.weaponStat[1].additionalProjectiles; i++)
            {
                if (GameObject.FindGameObjectWithTag("Enemy") != null)
                {
                    Instantiate(axeObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
            yield return new WaitForSeconds(strimmerCooldown);
        }
    }

    //function to throw axes repeatedly
    private void StartAxes()
    {
        StartCoroutine("throwAxe");
    }
}
