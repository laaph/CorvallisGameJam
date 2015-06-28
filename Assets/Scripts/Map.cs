using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {

	public float streetFuel = 0.3f;
	public float skyScraperFuel = 1.0f;
	public float smallHouseFuel = 0.6f;
	public float parkFuel = 0.5f;
	public float barnFuel = 0.9f;
	public float rowHousesFuel = 0.7f;

	public Transform emptyTileObject;

	public Transform street;
	public Transform burnedThing;
	public Transform skyScraper;
	public Transform smallHouse;
	public Transform park;
	public Transform barn;
	public Transform rowHouses;
	
	public Material burnedMat;
	public Material fireMat;

	static int maxSize = 100 ;

	public Transform[,] 	mapObjects	= new Transform[maxSize, maxSize];
	
	// Use this for initialization
	void Start () {
	
		for(int i = 0; i < maxSize; i++) {
			for(int j = 0; j < maxSize; j++) {
				mapObjects[i,j] = Instantiate(emptyTileObject);
				MapTile s = mapObjects[i,j].GetComponent<MapTile>();
				
				Transform tileObject = null;
								
				if(((i % 3) == 0 || (j % 3) == 0) && Random.Range(0f, 1f) < 0.9f) {
					s.type = 0;
					tileObject = Instantiate(street);
					s.fireFuel = streetFuel;
					
				} else {
					switch(Random.Range(0, 10)) {
						case 5:
						case 6:
						case 9:
						case 0:
							s.type = 2;
							tileObject = Instantiate(smallHouse);
							s.fireFuel = smallHouseFuel;
							break;
						case 1:
							s.type = 3;
							tileObject = Instantiate(park);
							s.fireFuel = parkFuel;
							break;
						case 2:
							s.type = 4;
							tileObject = Instantiate(skyScraper);
							s.fireFuel = skyScraperFuel;
							break;
						case 3:
							s.type = 5;
							tileObject = Instantiate(barn);
							s.fireFuel = barnFuel;
							break;
						case 7:
						case 8:
						case 4:
							s.type = 6;
							tileObject = Instantiate(rowHouses);
							s.fireFuel = rowHousesFuel;
						break;
					}
				}
				mapObjects[i, j].SetParent(this.transform);
				tileObject.SetParent(mapObjects[i,j]);
				mapObjects[i, j].position = new Vector3(i*100, 0, j*100);
				s.x = i; s.y = j;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public float randomgaussian(float mean, float stdDev)
	{
		//generates a random number within a gaussian distribution
		Random rand = new Random ();
		
		float u1 = Random.Range (0f, 1f);
		float u2 = Random.Range (0f, ==1f);
		float randStdNormal = Mathf.Sqrt(-2 * Mathf.Log(u1))* Mathf.Sin(2* 
		                                                                Mathf.PI* u2);
		return mean + stdDev * randStdNormal;
	}
	public void Explode(int i, int j, int fragments, float radius)
	{
		float angle = Random.Range(0f,1f)*2*Mathf.PI;
		float length = randomgaussian(1, radius);
		Vector2 vec = new Vector2( length * Mathf.Sin(angle), length * Mathf.Cos(angle) );
		//double[] vec = StarMath.multiply(length,unitvec);
		vec = new Vector2((float)i,(float)j);
		//vec = StarMath.add(new double[]{(double)i,(double)j},vec);
		int i2 = (int)Mathf.Round(vec[0]);
		int j2 = (int)Mathf.Round(vec[1]);
		if (((i2 >= 0) && (i2 < maxSize)) && ((j2 >= 0) && (j2 < maxSize)))
		{
			SetThingsOnFire(i2,j2);
		}
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
