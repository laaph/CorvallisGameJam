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
            if (child.CompareTag("Fire"))
            {
                if (child.GetComponent<Fire>().FireID < amount)
                {
                    child.GetComponent<ParticleSystem>().Play(true);
                }
                else
                {
                    child.GetComponent<ParticleSystem>().Pause(true);
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
                        if (prefab.CompareTag("Goo"))
                        {
                            GameObject currentInstance;
                            currentInstance = Instantiate(prefab, currentPosition, Quaternion.identity) as GameObject;
                            currentInstance.transform.parent = map.mapObjects[xTile, yTile];
                            //currentInstance.GetComponent<Renderer>().enabled = false;
                            currentInstance.GetComponent<Goo>().ProgressionNumber = currentProgression;
                            currentProgression += 1;
                        }

                        if (prefab.CompareTag("Fire"))
                        {
                            GameObject currentInstance;
                            currentInstance = Instantiate(prefab, currentPosition, Quaternion.identity) as GameObject;
                            currentInstance.transform.parent = map.mapObjects[xTile, yTile];
                            currentInstance.GetComponent<ParticleSystem>().Pause();
                            currentInstance.GetComponent<Fire>().FireID = currentProgression;
                            currentProgression += 1;
                        }                        
                    }
                }
            }            
        }
    }

    public void despawn(int xTile, int yTile)
    {
        if (prefab.CompareTag("Goo"))
        {
            foreach (Transform child in map.mapObjects[xTile, yTile])
            {
                if (child.CompareTag("Goo"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
        if (prefab.CompareTag("Fire"))
        {
            foreach (Transform child in map.mapObjects[xTile, yTile])
            {
                if (child.CompareTag("Fire"))
                {
                    Destroy(child.gameObject);
                }
            }
        }        
    }

    public void setVisible(int xTile, int yTile, bool visible)
    {
        if (prefab.CompareTag("Goo"))
        {
            foreach (Transform child in map.mapObjects[xTile, yTile])
            {
                if (child.CompareTag("Goo"))
                {
                    child.gameObject.GetComponent<Renderer>().enabled = visible;
                }
            }
        }
        if (prefab.CompareTag("Fire"))
        {
            foreach (Transform child in map.mapObjects[xTile, yTile])
            {
                if (child.CompareTag("Fire"))
                {
                    if (visible == true)
                    {
                        child.gameObject.GetComponent<ParticleSystem>().Play();
                    }
                    else if (visible == false)
                    {
                        child.gameObject.GetComponent<ParticleSystem>().Pause();
                    }
                }
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
