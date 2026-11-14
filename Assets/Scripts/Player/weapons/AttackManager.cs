using System.Collections;
using UnityEngine;
using static PlayerScript;

public class AttackManager : MonoBehaviour
{
    //setting up attack refrences
    private GameObject spawnedStrimmerAttack = null;
    public GameObject strimmerAttack;
    public GameObject strimmerObject;
    public GameObject shearObject;
    public GameObject axeObject;

    //setting up player stats
    PlayerScript playerScr;
    PlayerScript.playerStats stats;
    int currentCharacter;

    public float strimmerCooldown = 0.5f;
    public float shearCooldown = 1f;
    public float axeCooldown = 2f;

    private void Start()
    {
        playerScr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        currentCharacter = playerScr.playerCharacter;
        stats = playerScr.characterStats[currentCharacter];
    }


    public bool NotOnScreen(GameObject gameObj)
    {
        //taken from https://discussions.unity.com/t/detecting-if-a-visual-element-is-off-screen/880405

        var pos = Camera.main.WorldToScreenPoint(gameObj.transform.position);
        bool isOffscreen = pos.x <= 0 || pos.x >= Screen.width ||
                           pos.y <= 0 || pos.y >= Screen.height;


        return isOffscreen;
    }



    private void StartStrimmer()
    {
        Instantiate(strimmerObject, transform.position, Quaternion.identity);
        InvokeRepeating(nameof(spawnAttackStrimmer), 0, strimmerCooldown / stats.weaponCooldown);
    }

    private void spawnAttackStrimmer()
    {
        spawnedStrimmerAttack = Instantiate(strimmerAttack, transform.position, Quaternion.identity);
    }

    //function to throw shear
    private void throwShear()
    {
        Instantiate(shearObject, transform.position, Quaternion.identity);
    }

    //function to throw shears repeatedly
    private void StartShears()
    {
        InvokeRepeating(nameof(throwShear), 0, shearCooldown / stats.weaponCooldown);
    }


    private void throwAxe()
    {
        Instantiate(axeObject, transform.position, Quaternion.identity);
    }

    //function to throw axes repeatedly
    private void StartAxes()
    {
        InvokeRepeating(nameof(throwAxe), 0, axeCooldown / stats.weaponCooldown);
    }
}
