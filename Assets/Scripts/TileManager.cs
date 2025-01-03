using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab;
    public float zSpawn = 0;
    public const float tileLength = 437;
    Quaternion rotation = Quaternion.Euler(0, 180, 0);
    public Transform player;

    List<GameObject> activeObject = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < tilePrefab.Length; i++)
        {
            if (i == 0)
                spawnTile(0);
            else
                spawnTile(Random.Range(1, tilePrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z - tileLength > zSpawn - tilePrefab.Length * tileLength)
        {
            spawnTile(Random.Range(1, tilePrefab.Length));
            Destroy(activeObject[0]);
            activeObject.RemoveAt(0);
        }
    }
    void spawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefab[tileIndex], transform.forward * zSpawn, rotation);
        go.transform.SetParent(transform);
        zSpawn += tileLength;
        activeObject.Add(go);
    }
}
