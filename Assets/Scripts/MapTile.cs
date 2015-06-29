using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

	[SerializeField] Material fireMat;
	public Material burnedMat;

	public Transform currentPrefab;
	public Transform originalPrefab;
	public Transform burningPrefab;	
	public Transform burnedPrefab;
	public Transform oozePrefab;
 
	public int x,y;
	bool onFire;
	public bool isBurned;
	public bool isOozed;
	float fireSpreadRate = 0.5f;
	float fireSpreadTimer = 0;
	float oozeSpreadRate = 0.5f;
	float oozeSpreadTimer = 0;

	public float fireFuel = 1;
	public int oozeLife = 1;
	
	
	public int type;
	// enum for int[,] map
	// 0 = street
	// 1 = burned thing
	// 2 = smallhouses
	// 3 = park
	// 4 = skyscraper
	// 5 = barn
	// 6 = rowhouses
	
	Map m;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindWithTag("Map");
		m = g.GetComponentInChildren<Map>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Spread fire every interval of time
		fireSpreadTimer += Time.deltaTime;
		if (fireSpreadTimer > fireSpreadRate) {
			if (onFire) {
				if (Random.Range (0f, 1f) > 0.15) {
					// Set neighbors on fire
					// Pick a direction
					int x1 = 0;
					int y1 = 0;
					while (x1 == 0 && y1 == 0) {
						x1 = Random.Range (-1, 2);
						y1 = Random.Range (-1, 2);
					}
					m.SetThingsOnFire (x + x1, y + y1);
				}
				//Decrease fuel
				fireFuel -= Time.deltaTime;
				if (fireFuel <= 0) {
					//print (fireFuel);
					onFire = false;
					isBurned = true;
					SetBurned(x, y);
					if (type == 4)
					{
					if(Random.Range(0f,1f)<0.05)
					{
					//m.Explode(x,y,6,6);
					}
					}
				}
			}	
			//Reset timer
			fireSpreadTimer = 0;
		}
		oozeSpreadTimer += Time.deltaTime;
		if (oozeSpreadTimer > oozeSpreadRate) {
			if (isOozed) {
				if (Random.Range (0f, 1f) > 0.2) {
					// Set neighbors oozed
					// Pick a direction
					int x1 = 0;
					int y1 = 0;
					while (x1 == 0 && y1 == 0) {
						x1 = Random.Range (-1, 2);
						y1 = Random.Range (-1, 2);
					}
					m.SetThingsOozed (x + x1, y + y1);
				}
				if (Random.Range (0f, 1f) > 0.9) {
					isOozed = true;
					Oozify (x, y);
				}
			}
			//Reset timer
			oozeSpreadTimer = 0;
		}
		
	}	
	public void setFire() {
		onFire = true;
	}

	public void setBurned() {
		isBurned = true;
		onFire = false;
	}

	public void setOozed() {
		isOozed = true;
	}

	//Swap to burning prefab
	public void StartBurning(int x, int y) {
		if (!isBurned && !isOozed) {
			currentPrefab.gameObject.SetActive(false);
			burnedPrefab.gameObject.SetActive(true);
			onFire = true;
		}
		//Burn ooze
		if (isOozed) {
			oozeLife -= 1;
			if (oozeLife <= 0) {
				currentPrefab.gameObject.SetActive(false);
				burnedPrefab.gameObject.SetActive(true);
				oozePrefab.gameObject.SetActive(false);
				GameObject.FindGameObjectWithTag("Map").GetComponent<Populate>().setProgression(x, y, 0);
				onFire = true;
			}
		}
	}

	//Swap to burned prefab
	public void SetBurned(int x, int y) {
//			if (burnedPrefab) {
//			Transform t = Instantiate (burnedPrefab);
//				GameObject newPrefab = t.gameObject;
//				newPrefab.transform.position = transform.position;
//				newPrefab.transform.SetParent (transform.parent.transform);
//				newPrefab.GetComponent<MapTile> ().setBurned ();
//				newPrefab.GetComponent<MapTile> ().x = x;
//				newPrefab.GetComponent<MapTile> ().y = y;
//				newPrefab.transform.parent.gameObject.GetComponent<Map> ().SetGridObject (x, y, newPrefab.transform);
//				Destroy (gameObject);
//			}
		onFire = false;

	}

	//Swap to ooze prefab
	public void Oozify(int x, int y) {
		if (!onFire && !isBurned) {
			currentPrefab.gameObject.SetActive(false);
			oozePrefab.gameObject.SetActive(true);
			GameObject.FindGameObjectWithTag("Map").GetComponent<Populate>().setProgression(x, y, 10);
			isOozed = true;
		}
	}
}
