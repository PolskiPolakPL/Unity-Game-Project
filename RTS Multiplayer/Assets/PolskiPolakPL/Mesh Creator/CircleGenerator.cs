using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class CircleGenerator : MonoBehaviour
{
    Mesh mesh;

    public bool FilledShape = true;
    public int EdgeCount = 8;
    public float Rradious = 1;
    public float RingThickness = 0.1f;

    Vector3[] vertecies;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Circular Mesh";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        CalculateMesh();
        UpdateMesh();
    }

    void CalculateMesh()
    {

        if (FilledShape)
        {
            vertecies = GetVerteciesInCircle(EdgeCount, Rradious).ToArray();
            triangles = GetFilledTriangles(vertecies).ToArray();
        }
        else
        {
            CalculateHollowCircle(EdgeCount, Rradious, Rradious - RingThickness);
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertecies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    void CalculateHollowCircle(int edges, float outerRadius, float innerRadius)
    {
        List<Vector3> verteciesList = new List<Vector3>();

        List<Vector3> tempVertexList = GetVerteciesInCircle(edges, outerRadius);
        verteciesList.AddRange(tempVertexList);
        tempVertexList = GetVerteciesInCircle(edges, innerRadius);
        verteciesList.AddRange(tempVertexList);

        vertecies = verteciesList.ToArray();
        triangles = GetHollowTriangles(vertecies).ToArray();
    }

    List<Vector3> GetVerteciesInCircle(int edges, float radius)
    {
        List<Vector3> verteciesList = new List<Vector3>();

        //normalized size of steps
        float circleStepPrecision = (float)1/edges;
        
        //2*PI
        float TAU = 2*Mathf.PI;

        // size of steps in radians
        float radianCircleStepPrecision = circleStepPrecision*TAU;

        float currentRadian;
        for (int i=0; i<edges; i++)
        {
            currentRadian = radianCircleStepPrecision*i;
            // new point = ( cos(x)*r, 0, sin(x)*r) )
            verteciesList.Add(new Vector3(Mathf.Cos(currentRadian)*radius, 0, Mathf.Sin(currentRadian) * radius));
        }
        return verteciesList;
    }

    List<int> GetFilledTriangles(Vector3[] vertecies)
    {
        List<int> trianglesList = new List<int>();

        int triangleAmount = vertecies.Length-2;

        for(int i=0; i<triangleAmount; i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i+2);
            trianglesList.Add(i+1);
        }
        return trianglesList;
    }

    List<int> GetHollowTriangles(Vector3[] vertecies)
    {
        int triangleAmount = vertecies.Length;
        int edges = vertecies.Length / 2;
        List<int> trianglesList = new List<int>();


        int outerIndex, innerIndex;
        for (int i = 0; i<edges; i++)
        {
            outerIndex = i;
            innerIndex = i+edges;
            
            //outer triangle
            trianglesList.Add(outerIndex);
            trianglesList.Add(innerIndex);
            trianglesList.Add((i+1)%edges);

            //inner triangle
            trianglesList.Add(outerIndex);
            trianglesList.Add(edges + ((edges + i - 1) % edges));
            trianglesList.Add(outerIndex+edges);
        }

        return trianglesList;
    }
}
