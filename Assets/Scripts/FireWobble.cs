using UnityEngine;
using System.Collections;

public class FireWobble : MonoBehaviour {

    [SerializeField]
    Vector3 wobbleAmounts;
    [SerializeField]
    float rate;
    
    bool direction;

	// Use this for initialization
	void Start () {
        InvokeRepeating("wobbleMove", 0.0f, rate);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void wobbleMove()
    {
        if (direction == true)
        {
            transform.position += wobbleAmounts;
            direction = false;
        }
        else if(direction == false)
        {
            transform.position -= wobbleAmounts;
            direction = true;
        }
    }
}
