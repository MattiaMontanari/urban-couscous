using UnityEngine;
using System.Runtime.InteropServices;


public class ns_DistanceField : MonoBehaviour
{
}

namespace DistanceField
{
    public class distanceQuery
    {

#if UNITY_STANDALONE_OS || UNITY_EDITOR_OSX || UNITY_INCLUDE_TESTS
        [DllImport("libopengjk_ce", EntryPoint = "csFunction")]
        static extern double gjk(int na, double[,] ia, int nb, double[,] ib);
#else
        [DllImport("openGJKlib", EntryPoint = "csFunction", CallingConvention = CallingConvention.StdCall)]
        private static extern float gjk(int na, float[,] ia, int nb, float[,] ib);
#endif
        Mesh meshA, meshB;
        Mesh[] meshes;
        public double[,] dist;
        public GameObject[] gos { get; set; }
        public int numberOfBodies { get; set; }


        public void getAllPlayers()
        {
            // Will measure distance for all GOs tagget Player
            gos = GameObject.FindGameObjectsWithTag("Player");

            numberOfBodies = gos.Length;
            dist = new double[numberOfBodies, numberOfBodies];
            meshes = new Mesh[gos.Length];
            for (int i = 0; i < gos.Length; i++)
            {
                meshes[i] = gos[i].GetComponent<MeshFilter>().mesh;
                if (Debug.isDebugBuild)
                {
                    Debug.Log("GameObject " + i + " has label: " + gos[i].name);
                }
                if (meshes[i] == null)
                {
                    Debug.LogError("Mesh not found");
                }
            }
        }

        public void AllGJKsCalls()
        {
            for (int i = 0; i < numberOfBodies; i++)
            {
                for (int j = i + 1; j < numberOfBodies; j++)
                {

                    meshA = gos[i].GetComponent<MeshFilter>().mesh;
                    meshB = gos[j].GetComponent<MeshFilter>().mesh;

                    Vector3[] vrtxA = meshA.vertices;
                    Vector3[] vrtxB = meshB.vertices;
                    double[,] coordsA = new double[3, vrtxA.Length];
                    double[,] coordsB = new double[3, vrtxB.Length];

                    for (int k = 0; k < vrtxA.Length; k++)
                    {
                        Vector3 vrtx = gos[i].transform.TransformPoint(vrtxA[k]);
                        coordsA[0, k] = (double)vrtx.x;
                        coordsA[1, k] = (double)vrtx.y;
                        coordsA[2, k] = (double)vrtx.z;
                    }
                    for (int k = 0; k < vrtxB.Length; k++)
                    {
                        Vector3 vrtx = gos[j].transform.TransformPoint(vrtxB[k]);
                        coordsB[0, k] = (double)vrtx.x;
                        coordsB[1, k] = (double)vrtx.y;
                        coordsB[2, k] = (double)vrtx.z;
                    }

                    dist[i, j] = gjk(vrtxA.Length, coordsA, vrtxB.Length, coordsB);
                }
            }

        }

    }
}
