using UnityEngine;
using System.Collections;

public class PeopleInit : MonoBehaviour {

	public Transform[] people; // Set in editor
	public Transform peopleHolder; // Set in editor 
	Map m;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindWithTag("Map");
		m = g.GetComponentInChildren<Map>();
		int maxSize = 100;// m.maxSize;
		
		for(int i = 0; i < maxSize; i++) {

			for(int j = 0; j < maxSize; j++) {
				if(Random.Range(0f, 1f) > 0.5) {
					Transform person = Instantiate(people[Random.Range(0, people.Length)]);
					person.position = new Vector3(i * 100 + Random.Range(0, 100), 2, j * 100 + Random.Range(0, 100));
					person.SetParent(peopleHolder);
				}
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
