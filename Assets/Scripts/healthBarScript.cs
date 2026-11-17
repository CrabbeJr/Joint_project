using UnityEngine;

public class healthBarScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerScript player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }
    private void FixedUpdate()
    {
        transform.localScale = new Vector3 (player.currentStats.health / 50,1,1);
        if (transform.localScale.x < 0) { transform.localScale = new Vector3(0, 1, 1); }
    }
}
