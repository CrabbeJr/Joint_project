using UnityEngine;
using UnityEngine.Tilemaps;

public class tilesetScript : MonoBehaviour
{
    public Tilemap map;
    private Camera camera;
    public Tile tile;

    void Start()
    {
        camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        map.ClearAllTiles();
        Vector3 playerPos = transform.position;
        playerPos.x += 16;
        playerPos.y += 9;
        map.FloodFill(map.WorldToCell(playerPos),tile);
        playerPos.x -= 32;
        playerPos.y -= 18;
        map.FloodFill(map.WorldToCell(playerPos), tile);
    }
}
