using UnityEngine;
using System.Collections;

public class Goo : MonoBehaviour {

    public int ProgressionNumber { get; set; }

    void Awake()
    {
        ProgressionNumber = 0;
    }
}
