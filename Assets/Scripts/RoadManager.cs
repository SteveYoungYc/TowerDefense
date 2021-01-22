using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roads;

    void Start()
    {
        roads = GameObject.FindGameObjectsWithTag("Road");
        foreach (var road in roads)
        {
            
            print(road.name + road.transform.position);
        }
    }

    // Update is called once per frame
    void Update() 
    {

    }
}
