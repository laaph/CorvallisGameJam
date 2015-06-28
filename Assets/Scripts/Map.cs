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
	
	
	public Material roadNS;
	public Material roadEW;
	//public Material roadIntersection;  // Currently default.

	public Transform emptyTileObject;

	public Transform street;
	public Transform burnedStreet;
	public Transform oozeStreet;
	
	public Transform skyScraper;
	public Transform burnedSkyScraper;
	public Transform oozeSkyScraper;
	
	public Transform smallHouse;
	public Transform burnedSmallHouse;
	public Transform oozeSmallHouse;
	
	public Transform park;
	public Transform burnedPark;
	public Transform oozePark;
	
	public Transform barn;
	public Transform burnedBarn;
	public Transform oozeBarn;
	
	public Transform rowHouses;
	public Transform burnedRowHouses;
	public Transform oozeRowHouses;
	
	public Material burnedMat;
	public Material fireMat;

	static public int maxSize = 100;

	public Transform[,] 	mapObjects	= new Transform[maxSize, maxSize];
	
	// Use this for initialization
	void Start () {
	
		bool skipHorizRoad;
		bool skipVertRoad;
		skipVertRoad = false; skipHorizRoad = false;
		
		for(int i = 0; i < maxSize; i++) {
		if(i % 3 == 0) {
			skipHorizRoad = false;
		}
			for(int j = 0; j < maxSize; j++) {
				bool nsDir = false;
				bool ewDir = false;
				mapObjects[i,j] = Instantiate(emptyTileObject);
				MapTile s = mapObjects[i,j].GetComponent<MapTile>();
				
				Transform tileObject = null;
				Transform burnedObject = null;
				Transform oozeObject = null;
				
				bool road = false;
									
				if((i % 3) == 0 && (j % 3) == 0) {
					s.type = 0;
					tileObject = street; 
					burnedObject = burnedStreet;
					oozeObject = oozeStreet;
					s.fireFuel = streetFuel;
					road = true;
					
					skipVertRoad = false;  
					// Cross street
					if(Random.Range(0f, 1f) > 0.95f) {
						skipVertRoad = true;
					}
					if(Random.Range(0f, 1f) > 0.95f) {
						skipHorizRoad = true;
					}
					//Debug.Log("x, y" + i + j + " skips " + skipVertRoad + skipHorizRoad);
				}
				
				if((i % 3) == 0 && (j % 3) != 0 && !skipHorizRoad) {
					s.type = 0;
					tileObject = street; 
					burnedObject = burnedStreet;
					oozeObject = oozeStreet;
					s.fireFuel = streetFuel;
					nsDir = true;
					road = true;
				}

				if((i % 3) != 0 && (j % 3) == 0 && !skipVertRoad) {
					s.type = 0;
					tileObject = street;
					burnedObject = burnedStreet;
					oozeObject = oozeStreet;
					s.fireFuel = streetFuel;
					ewDir = true;
					road = true;
				}
																			
	
				if(!road) {
					switch(Random.Range(0, 10)) {
						case 5:
						case 6:
						case 9:
						case 0:
							s.type = 2;
							tileObject = smallHouse; //Instantiate(smallHouse);
							burnedObject = burnedSmallHouse;
							oozeObject = oozeSmallHouse;
							s.fireFuel = smallHouseFuel;
							break;
						case 1:
							s.type = 3;
							tileObject = park; //Instantiate(park);
							burnedObject = burnedPark;
							oozeObject = oozePark;
							s.fireFuel = parkFuel;
							break;
						case 2:
							s.type = 4;
							tileObject = skyScraper;//Instantiate(skyScraper);
							burnedObject = burnedSkyScraper;
							oozeObject = oozeSkyScraper;
							s.fireFuel = skyScraperFuel;
							break;
						case 3:
							s.type = 5;
							tileObject = barn; //Instantiate(barn);
							burnedObject = burnedBarn;
							oozeObject = oozeBarn;
							s.fireFuel = barnFuel;
							break;
						case 7:
						case 8:
						case 4:
							s.type = 6;
							tileObject = rowHouses; //Instantiate(rowHouses);
							burnedObject = burnedRowHouses;
							oozeObject = oozeRowHouses;
							s.fireFuel = rowHousesFuel;
						break;
					}
				}
				mapObjects[i, j].SetParent(this.transform);
				Transform t = Instantiate(tileObject);
				t.SetParent(mapObjects[i,j]);
				Transform b = Instantiate(burnedObject);
				b.SetParent(mapObjects[i,j]);
				b.transform.position = t.transform.position;
				b.gameObject.SetActive(false);
				Transform o = Instantiate(oozeObject);
				o.SetParent(mapObjects[i,j]);
				o.transform.position = t.transform.position;
				o.gameObject.SetActive(false);

				if(nsDir) { 
					Renderer r = t.GetComponent<Renderer>();
					r.sharedMaterial = roadNS;
				}
				if(ewDir) { 
					Renderer r = t.GetComponent<Renderer>();
					r.sharedMaterial = roadEW; 
				} 
				mapObjects[i, j].position = new Vector3(i*100, 0, j*100);
				s.x = i; s.y = j;
				s.currentPrefab  = t;
				s.originalPrefab = tileObject;
				s.burnedPrefab   = b;
				s.oozePrefab 	 = o;
				s.burningPrefab  = tileObject;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public float randomgaussian(float mean, float stdDev)
	{
		//generates a random number within a gaussian distribution
//		Random rand = new Random ();
		
		float u1 = Random.Range (0f, 1f);
		float u2 = Random.Range (0f, 1f);
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

	public int GetMapSize(){
		return maxSize;
	}
}
