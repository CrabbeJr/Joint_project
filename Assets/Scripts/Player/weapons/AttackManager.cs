using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject spawnedStrimmerAttack = null;
    public GameObject strimmerAttack;
    public GameObject strimmerObject;
    public GameObject shearObject;

    //cooldowns - could mabye be turned into one variable later and have each weapon have their cooldown dictated by some function of value
    public float strimmerCooldown = 0.5f;
    public float shearCooldown = 1f;

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
    private void throwShear()
    {
        Instantiate(shearObject, transform.position, Quaternion.identity);
    }

    //function to throw shears repeatedly
    private void StartShears()
    {
        InvokeRepeating(nameof(throwShear), 0, shearCooldown);
    }
}
