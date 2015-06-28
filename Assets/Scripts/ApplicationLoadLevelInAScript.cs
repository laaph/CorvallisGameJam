using UnityEngine;
using System.Collections;

public class ApplicationLoadLevelInAScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void callForNewScene() {
		Application.LoadLevel("MainScene");
	}
}
