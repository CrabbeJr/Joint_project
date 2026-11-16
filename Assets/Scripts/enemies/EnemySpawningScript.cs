using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{
    public GameObject enemy;
    private float timeBetween;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetween = Time.time;
        Random.InitState(50);
    }

    private void FixedUpdate()
    {
       
        float currentTime = Time.time;
        if (currentTime - timeBetween >= 0.5f && GameObject.FindGameObjectsWithTag("Enemy").Length < 10)
        {
            Vector2 spawnPos = new Vector2( 0, 0);

            switch (Random.Range(0, 4))
            {
                case 0: // on top
                    spawnPos.y = 5;
                    spawnPos.x = Random.Range(-5, 6);
                break;
                case 1: // on bottom
                    spawnPos.y = -5;
                    spawnPos.x = Random.Range(-5, 6);
                    break;
                case 2: // left
                    spawnPos.x = -5;
                    spawnPos.y = Random.Range(-5, 6);
                    break;
                case 3: // right
                    spawnPos.x = 5;
                    spawnPos.y = Random.Range(-5, 6);
                break;
                

            }

            Vector3 spawnPosV3 = spawnPos.ConvertTo<Vector3>();
            
            Instantiate(enemy, spawnPosV3, Quaternion.identity);
            
            timeBetween = Time.time;
        }
    }
}
