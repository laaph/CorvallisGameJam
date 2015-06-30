using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Application.LoadLevel("JonathanScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame() {
		Application.LoadLevel("JonathanScene");
	}
}
