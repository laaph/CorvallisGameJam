using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	public GameObject cursor;
	public float movementSpeed = 0.05f;
	public float scrollSpeed = 100;
	public bool gameActionOn 	= true;
	public bool mainMenu		= false;
	public bool creditsScreen	= false;
	public bool storyScreen		= false;
	float borderMargin = 50;
	float max_y=1000;
	float min_y=0;

	// Use this for initialization
	void Start ()
	{
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		float y_translate = -Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
		float camera_eulerx= Mathf.Clamp(transform.eulerAngles.x-Input.GetAxis("Mouse ScrollWheel")*20, 45,90);
		float new_y = Mathf.Clamp(transform.position.y+y_translate, min_y, max_y);
		transform.position= new Vector3(transform.position.x,new_y, transform.position.z);
	
		//Move right
		if (Input.mousePosition.x > Screen.width - borderMargin) {
			transform.Translate (movementSpeed, 0, 0, Space.World);
		}
		//Move left
		if (Input.mousePosition.x < borderMargin) {
			transform.Translate (-movementSpeed, 0, 0, Space.World);
		}
		//Move up
		if (Input.mousePosition.y > Screen.height - borderMargin) {
			transform.Translate (0, 0, movementSpeed, Space.World);
		}
		//Move down
		if (Input.mousePosition.y < borderMargin) {
			transform.Translate (0, 0, -movementSpeed, Space.World);
			
		}
		
		if(gameActionOn) {
		
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	
			if (Physics.Raycast (ray, out hit)) {
				cursor.SetActive (true);
				cursor.transform.position = hit.point;
			} else {
				cursor.SetActive (false);
			}

			if (Input.GetMouseButtonDown (0)) {
				GameObject g = hit.collider.gameObject;
				while (g.tag != "MapTile") {
//					Debug.Log (g.tag);
//					Debug.Log (g.ToString ());
					g = g.transform.parent.gameObject;
				}
				MapTile s = g.GetComponent<MapTile> ();
				s.StartBurning (s.x, s.y);
							Debug.Log ("clicked");
			}
	
	//		Set ooze (for debug)
			if (Input.GetMouseButtonDown (1)) {
				GameObject g = hit.collider.gameObject;
				while (g.tag != "MapTile") {
					g = g.transform.parent.gameObject;
				}
				MapTile s = g.GetComponent<MapTile> ();
				s.Oozify (s.x, s.y);
			}
		}
	}
}
