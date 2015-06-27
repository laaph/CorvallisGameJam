using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

	[SerializeField] Material fireMat;
	public Material burnedMat;
	public GameObject burningPrefab;
	public GameObject burnedPrefab;
	public GameObject oozePrefab;

	public int x,y;
	bool onFire;
	bool isBurned;
	bool isOozed;
	float fireSpreadRate = 0.5f;
	float fireSpreadTimer = 0;
	float oozeSpreadRate = 0.5f;
	float oozeSpreadTimer = 0;
	Renderer r;
	Map m;

	// Use this for initialization
	void Start () {
		//onFire = false;
		r = GetComponent<Renderer>();
		GameObject g = GameObject.FindWithTag("Map");
		m = g.GetComponentInChildren<Map>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Spread fire every interval of time
		fireSpreadTimer += Time.deltaTime;
		if (fireSpreadTimer > fireSpreadRate) {
			if (onFire) {
				if (Random.Range (0f, 1f) > 0.2) {
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
				if (Random.Range (0f, 1f) > 0.9) {
					onFire = false;
					isBurned = true;
					SetBurned(x, y);
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
			if (burningPrefab) {
				GameObject newPrefab = Instantiate (burningPrefab);
				newPrefab.transform.position = transform.position;
				newPrefab.transform.SetParent (transform.parent.transform);
				newPrefab.GetComponent<MapTile> ().setFire ();
				newPrefab.GetComponent<MapTile> ().x = x;
				newPrefab.GetComponent<MapTile> ().y = y;
				newPrefab.transform.parent.gameObject.GetComponent<Map> ().SetGridObject (x, y, newPrefab.transform);
				Destroy (gameObject);
			}
		}
	}

	//Swap to burned prefab
	public void SetBurned(int x, int y) {
			if (burnedPrefab) {
				GameObject newPrefab = Instantiate (burnedPrefab);
				newPrefab.transform.position = transform.position;
				newPrefab.transform.SetParent (transform.parent.transform);
				newPrefab.GetComponent<MapTile> ().setBurned ();
				newPrefab.GetComponent<MapTile> ().x = x;
				newPrefab.GetComponent<MapTile> ().y = y;
				newPrefab.transform.parent.gameObject.GetComponent<Map> ().SetGridObject (x, y, newPrefab.transform);
				Destroy (gameObject);
			}
	}

	//Swap to ooze prefab
	public void Oozify(int x, int y) {
		if (!onFire && !isBurned) {
			if (oozePrefab) {
				GameObject newPrefab = Instantiate (oozePrefab);
				newPrefab.transform.position = transform.position;
				newPrefab.transform.SetParent (transform.parent.transform);
				newPrefab.GetComponent<MapTile> ().setOozed ();
				newPrefab.GetComponent<MapTile> ().x = x;
				newPrefab.GetComponent<MapTile> ().y = y;
				newPrefab.transform.parent.gameObject.GetComponent<Map> ().SetGridObject (x, y, newPrefab.transform);
				Destroy (gameObject);
			}
		}
	}
}
