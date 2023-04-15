using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DistanceField;

public class runDistanceQuery : MonoBehaviour
{
    distanceQuery ds = new distanceQuery();
    void Start()
    {
        ds.getAllPlayers();
    }

    void Update()
    {
        // This will measure 3 distances: cube-cube, sphere-sphere and cube-sphere
        ds.AllGJKsCalls();

        foreach (double d in ds.dist) 
        { 
            Debug.Log("dist = " + d);
        }

    }
}
