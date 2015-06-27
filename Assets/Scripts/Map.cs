using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public Transform street;
	public Transform burnedThing;
	public Transform house;
	public Transform park;
	
	public Material burnedMat;
	public Material fireMat;

	static int maxSize = 100;
	public int[,] 			map 		= new int[maxSize, maxSize];
	public Transform[,] 	mapObjects	= new Transform[maxSize, maxSize];
	// 0 = street
	// 1 = burned thing
	// 2 = house
	// 3 = park
	
	
	// Use this for initialization
	void Start () {
	
		for(int i = 0; i < maxSize; i++) {
			for(int j = 0; j < maxSize; j++) {
				if(((i % 3) == 0 || (j % 3) == 0) && Random.Range(0f, 1f) < 0.9f) {
					map[i, j] = 0;
					mapObjects[i,j] = Instantiate(street);
				} else {
					if(Random.Range(0f, 1f) > 0.7f) {
						map[i, j] = 2;
						mapObjects[i, j] = Instantiate(house);
					} else {
						map[i, j] = 3;
						mapObjects[i, j] = Instantiate(park);
					}
				}
				mapObjects[i, j].SetParent(this.transform);
				mapObjects[i, j].position = new Vector3(i*100, 0, j*100);
				mapObjects[i, j].gameObject.AddComponent<SetFire>();
				SetFire s = mapObjects[i,j].GetComponent<SetFire>();
				s.x = i; s.y = j;
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
		SetFire s = mapObjects[x,y].GetComponent<SetFire>() as SetFire;
		s.setFire();
	}
	public Material getBurnedMat() {
		return burnedMat;
	}
	public Material getFireMat() {
		return fireMat;
	}
}
