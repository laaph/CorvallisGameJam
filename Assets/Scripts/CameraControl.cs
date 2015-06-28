using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject cursor;
	public float movementSpeed = 0.05f;
	float borderMargin = 50;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Move right
		if (Input.mousePosition.x > Screen.width - borderMargin) {
			transform.Translate( movementSpeed, 0, 0, Space.World );
		}
		//Move left
		if (Input.mousePosition.x < borderMargin) {
			transform.Translate( -movementSpeed, 0, 0, Space.World );
		}
		//Move up
		if (Input.mousePosition.y > Screen.height - borderMargin) {
			transform.Translate( 0, 0, movementSpeed, Space.World );
		}
		//Move down
		if (Input.mousePosition.y < borderMargin) {
			transform.Translate( 0, 0, -movementSpeed, Space.World );
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) { // back 
			Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize-1, 1);
		}
	
		if (Input.GetAxis("Mouse ScrollWheel") > 0) { // forward 
			Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize-1, 6);
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			cursor.SetActive(true);
			cursor.transform.position = hit.point;
		} else {
			cursor.SetActive(false);
		}

		if (Input.GetMouseButtonDown (0)) {
			MapTile s = hit.collider.gameObject.GetComponent<MapTile>();
			s.StartBurning(s.x, s.y);
//			Debug.Log ("clicked");
		}
		//Set ooze (for debug)
		if (Input.GetMouseButtonDown (1)) {
			MapTile s = hit.collider.gameObject.GetComponent<MapTile>();
			s.Oozify(s.x, s.y);
		}

	}
}
