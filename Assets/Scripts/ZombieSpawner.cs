using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject zombiePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
        for (int y = 0; y < height; y++)
        {
            Debug.LogFormat("Spawn {0} y", y);
            for (int x = 0; x < width; x++)
            {
                GameObject zombie = Instantiate(zombiePrefab);
                zombie.transform.position = new Vector2(x, y);
                Debug.LogFormat("Spawn {0} x", x);
            }
        }
        Debug.LogFormat("Spawned {0} zombies", width * height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
