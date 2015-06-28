using UnityEngine;
using System.Collections;

public class BillboardLookat : MonoBehaviour {


    Transform target;

    void Start()
    {

    }

    void Update() 
    {
        transform.LookAt(target);
    }
}