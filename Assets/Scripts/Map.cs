using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public Transform street;
	public Transform burnedThing;
	public Transform skyScraper;
	public Transform smallHouse;
	public Transform park;
	public Transform barn;
	public Transform rowHouses;
	
	public Material burnedMat;
	public Material fireMat;

	static int maxSize = 50;
	public int[,] 			map 		= new int[maxSize, maxSize];
	public Transform[,] 	mapObjects	= new Transform[maxSize, maxSize];
	// 0 = street
	// 1 = burned thing
	// 2 = smallhouses
	// 3 = park
	// 4 = skyscraper
	// 5 = barn
	// 6 = rowhouses
	
	
	// Use this for initialization
	void Start () {
	
		for(int i = 0; i < maxSize; i++) {
			for(int j = 0; j < maxSize; j++) {
				if(((i % 3) == 0 || (j % 3) == 0) && Random.Range(0f, 1f) < 0.9f) {
					map[i, j] = 0;
					mapObjects[i,j] = Instantiate(street);
				} else {
					switch(Random.Range(0, 10)) {
						case 5:
						case 6:
						case 9:
						case 0:
							map[i, j] = 2;
							mapObjects[i, j] = Instantiate(smallHouse);
							break;
						case 1:
							map[i, j] = 3;
							mapObjects[i, j] = Instantiate(park);
							break;
						case 2:
							map[i, j] = 4;
							mapObjects[i, j] = Instantiate(skyScraper);
							break;
						case 3:
							map[i, j] = 5;
							mapObjects[i, j] = Instantiate(barn);
							break;
						case 7:
						case 8:
						case 4:
							map[i, j] = 6;
							mapObjects[i, j] = Instantiate(rowHouses);
						break;
					}
				}
				mapObjects[i, j].SetParent(this.transform);
				mapObjects[i, j].position = new Vector3(i*100, 0, j*100);
				MapTile tile = mapObjects[i,j].GetComponent<MapTile>();
				tile.x = i; tile.y = j;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetThingsOnFire(int x, int y) {
		if(x >= maxSize || y >= maxSize || x < 0 || y < 0) {
			return;
		}
		MapTile tile = mapObjects[x,y].GetComponent<MapTile>() as MapTile;
		tile.StartBurning(x, y);
	}

	public void SetThingsOozed(int x, int y) {
		if(x >= maxSize || y >= maxSize || x < 0 || y < 0) {
			return;
		}
		MapTile tile = mapObjects[x,y].GetComponent<MapTile>() as MapTile;
		tile.Oozify(x, y);
	}

	public void SetGridObject(int x, int y, Transform newTransform) {
		mapObjects[x,y] = newTransform;
	}

	public Material getBurnedMat() {
		return burnedMat;
	}
	public Material getFireMat() {
		return fireMat;
	}
}
