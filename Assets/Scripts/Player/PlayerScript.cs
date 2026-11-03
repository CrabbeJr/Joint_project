using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform transformPlayer;
    public float moveSpeed = 0.01f;
    public float health = 50;

    public bool[] weaponsOwned = { false, false};
    private bool[] weaponsStarted = { false, false };

    private AttackManager attackManager;
    //                             strimmer

    void Start()
    {
        attackManager = this.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {

       // player movement
       // float moveSpeedReal = moveSpeed * Time.deltaTime;
        transformPlayer.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed, 0);
        
        
        //weapon management
        for (int i = 0; i < weaponsOwned.GetLength(0); i++)
        {
            if (weaponsOwned[i] && !weaponsStarted[i])
            {
                switch (i)
                {
                    case 0:
                        attackManager.StartCoroutine("StartStrimmer");
                        weaponsStarted[i] = true;
                    break;
                    
                    case 1:
                        attackManager.StartCoroutine("startShears");
                        weaponsStarted[i] = true;
                    break;
                }
            }
        }
    }
}
