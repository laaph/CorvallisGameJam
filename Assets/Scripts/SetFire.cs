using UnityEngine;
using System.Collections;

public class SetFire : MonoBehaviour {

	public Material fireMat;
	public Material burnedMat;
	public GameObject burningPrefab;
	public GameObject burnedPrefab;

	public int x,y;
	bool clickable;
	bool onFire;
	bool isBurned;
	float spreadRate = 0.5f;
	float spreadTimer = 0;
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
		spreadTimer += Time.deltaTime;
		if (spreadTimer > spreadRate) {
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
					//r.material = m.getBurnedMat ();
				}
				
			}

			//Reset timer
			spreadTimer = 0;
		}
	}	
	public void setFire() {
		onFire = true;
	}

	public void setBurned() {
		isBurned = true;
		onFire = false;
	}

	public void StartBurning(int x, int y) {
		if (!isBurned) {
			if (burningPrefab) {
				GameObject newPrefab = Instantiate (burningPrefab);
				newPrefab.transform.position = transform.position;
				newPrefab.transform.SetParent (transform.parent.transform);
				newPrefab.GetComponent<SetFire> ().setFire ();
				newPrefab.GetComponent<SetFire> ().x = x;
				newPrefab.GetComponent<SetFire> ().y = y;
				newPrefab.transform.parent.gameObject.GetComponent<Map> ().SetGridObject (x, y, newPrefab.transform);
				Destroy (gameObject);
			}
		}
	}

	public void SetBurned(int x, int y) {
		if (burnedPrefab) {
			GameObject newPrefab = Instantiate(burnedPrefab);
			newPrefab.transform.position = transform.position;
			newPrefab.transform.SetParent(transform.parent.transform);
			newPrefab.GetComponent<SetFire>().setBurned();
			newPrefab.GetComponent<SetFire>().x = x;
			newPrefab.GetComponent<SetFire>().y = y;
			newPrefab.transform.parent.gameObject.GetComponent<Map>().SetGridObject(x, y, newPrefab.transform);
			Destroy(gameObject);
		}
	}
	
	void OnMouseEnter() {
		clickable = true;
	//	Debug.Log("Clickable true at x " + x + " y " + y);
	}
	void OnMouseExit() {
		clickable = false;
	//	Debug.Log("No longer clickable at x " + x + "y " + y);
	}
}
