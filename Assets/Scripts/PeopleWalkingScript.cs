using UnityEngine;
using System.Collections;

public class PeopleWalkingScript : MonoBehaviour {

	public float walkingSpeed = 0.1f;
	int direction;
	
	

	// Use this for initialization
	void Start () {
		direction = Random.Range(0, 4);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 v = transform.position;
		switch(direction) {
			case 0:
				v = new Vector3(transform.position.x + walkingSpeed, 2, transform.position.z);
				break;
			case 1:
				v = new Vector3(transform.position.x - walkingSpeed, 2, transform.position.z);
				break;
			case 2:
				v = new Vector3(transform.position.x, 2, transform.position.z + walkingSpeed);
				break;
			case 3:
				v = new Vector3(transform.position.x, 2, transform.position.z - walkingSpeed);
				break;				
		}	
		transform.position = v;
		if(Random.Range(0f, 1f) > 0.99) {
			direction = Random.Range(0, 4);
		}
	}
}
