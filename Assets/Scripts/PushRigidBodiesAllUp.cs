using UnityEngine;
using DistanceField;

public class PushRigidBodiesAllUp : MonoBehaviour
{
    distanceQuery ds = new distanceQuery();
    void Start()
    {
        ds.getAllPlayers();
    }

    void Update()
    {
        ds.AllGJKsCalls();
        int numberOfBodies = ds.numberOfBodies;
        GameObject g;

        for (int i = 0; i < numberOfBodies; i++)
        {
            for (int j = i + 1; j < numberOfBodies; j++)
            {
                if (ds.dist[i, j] < 1e-6)
                {
                    // Not great, but hey, we just need to push up 5 rigid bodies in the scene
                    if (ds.gos[i].GetComponent<Rigidbody>())
                    {
                        g = ds.gos[i];
                    }
                    else
                    {
                        g = ds.gos[j];
                    }
                    Rigidbody rb = g.GetComponent<Rigidbody>();
                    rb.AddForce((Vector3.up) * (250f));
                }
            }
        }
    }
}
