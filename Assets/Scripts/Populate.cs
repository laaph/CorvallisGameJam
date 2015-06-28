using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Populate : MonoBehaviour {

    [SerializeField]
    int amountTest;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int randomMin;
    [SerializeField]
    int randomMax;
    [SerializeField]
    float count;
    [SerializeField]
    float spacing;

    public Map map;
    MapTile currentTile;

    void Start()
    {
        Invoke("spawnAll", 1.0f);
    }

    public void setProgression(int xTile, int yTile, int amount)
    {
        foreach (Transform child in map.mapObjects[xTile,yTile])
        {
            if (child.CompareTag("Goo"))
            {
                if (child.GetComponent<Goo>().ProgressionNumber < amount)
                {
                    child.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    child.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    public void spawnAll()
    {
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                spawn(x, y);
            }
        }
    }

    public void spawn(int xTile, int yTile)
    {
        int currentProgression = 0;
        Vector3 tileCenter = Vector3.zero;

        if (map.mapObjects != null)
        {
            tileCenter = map.mapObjects[xTile, yTile].position;
        }
        Vector3 currentPosition = tileCenter;

        for (int x = 0; x < 3 * count; x++)
        {
            for (int y = 0; y < 7 * count; y++) 
            {
                for (int z = 0; z < 3 * count; z++) 
                {
                    currentPosition = tileCenter + new Vector3((x * 35 - 25 + jitter()) * spacing, (y * 35 + jitter()) * spacing, (z * 35 - 25 + jitter()) * spacing);
                    if (Physics.CheckSphere(currentPosition, 10) == true)
                    {
                        GameObject currentInstance;
                        currentInstance = Instantiate(prefab, currentPosition, Quaternion.identity) as GameObject;
                        currentInstance.transform.parent = map.mapObjects[xTile, yTile];
                        currentInstance.GetComponent<Renderer>().enabled = false;
                        currentInstance.GetComponent<Goo>().ProgressionNumber = currentProgression;
                        currentProgression += 1;
                    }
                }
            }            
        }
    }

    public void despawn(int xTile, int yTile)
    {        
        foreach (Transform child in map.mapObjects[xTile, yTile])
        {
            if (child.CompareTag("Goo"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void setVisible(int xTile, int yTile, bool visible)
    {
        foreach (Transform child in map.mapObjects[xTile, yTile])
        {
            if (child.CompareTag("Goo"))
            {
                child.gameObject.GetComponent<Renderer>().enabled = visible;
            }
        }
    }

    int jitter()
    {
        int value;
        value = Random.Range(randomMin, randomMax);
        return value;
    }
}
