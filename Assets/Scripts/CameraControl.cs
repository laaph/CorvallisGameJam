using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	public GameObject cursor;
	public float movementSpeed = 0.05f;
	float borderMargin = 50;
	float max_y=1000;
	float min_y=0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
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
		/*
		if(true)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,120,Time.deltaTime*5);
		}
		else
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,60,Time.deltaTime*5);
		}
        */
		//transform.position = new Vector3(0,0,100);
		//print(Input.GetAxis("Mouse ScrollWheel"));
		//float cameray = Mathf.Lerp(0,2000,Input.GetAxis(("Mouse ScrollWheel")));//
		//transform.position.y = Mathf.Lerp(0,500,Input.GetAxis(("Mouse ScrollWheel")));
		float y_translate = -Input.GetAxis("Mouse ScrollWheel")*50;
		float camera_eulerx= Mathf.Clamp(transform.eulerAngles.x-Input.GetAxis("Mouse ScrollWheel")*20, 45,90);
		float new_y = Mathf.Clamp(transform.position.y+y_translate, min_y, max_y);
		transform.position= new Vector3(transform.position.x,new_y, transform.position.z);
		transform.eulerAngles = new Vector3(camera_eulerx,0,0);
		/*if((new_y<max_y))
		{
			new_y = max_y-transform.position.y;
		}
		else
		{
			if(new_y>min_y)
			{
				new_y= transform.position.y-min_y;
			}
		}*/
		//transform.Translate(25*Vector3.up * Input.GetAxis("Mouse ScrollWheel"), Space.World);
		
		//float camera_eulerx = Mathf.Lerp(45,90,Input.GetAxis(("Mouse ScrollWheel")));
		//transform.eulerAngles.x =Mathf.Lerp(45,90,Input.GetAxis(("Mouse ScrollWheel")));
		//transform.position= new Vector3(transform.position.x,cameray, transform.position.z);
			
		//transform.Translate(new_y*Vector3.up,Space.World);
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
				//Debug.Log (g.tag);
				Debug.Log (g.ToString ());
				g = g.transform.parent.gameObject;
			}
			MapTile s = g.GetComponent<MapTile> ();
			s.StartBurning (s.x, s.y);
			//			Debug.Log ("clicked");
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
