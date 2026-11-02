using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject spawnedStrimmerAttack = null;
    public GameObject strimmerAttack;
    public GameObject strimmerObject;


    public float strimmerCooldown = 0.5f;

    private void StartStrimmer()
    {
        Instantiate(strimmerObject,transform.position,Quaternion.identity);
        InvokeRepeating(nameof(spawnAttackStrimmer), 0, strimmerCooldown);
    }

    private void spawnAttackStrimmer()
    {
        spawnedStrimmerAttack = Instantiate(strimmerAttack, transform.position, Quaternion.identity);
    }
}
