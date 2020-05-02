using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    [Range(0,5)]
    public float cellSize = 1;

    public Vector3 gridOffset;

    [Range(1, 10)]
    public int gSizeX, gSizeY;



    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        CreateProceduralGrid();
        CreateMesh();
    }

    private void CreateProceduralGrid()
    {
        //set array sizes
        vertices = new Vector3[gSizeX * gSizeY * 4];
        triangles = new int[gSizeX * gSizeY * 6];
        
        //set a tracker for vertices and triangles
        int v = 0;
        int t = 0;

        //set vertex offset (using the middle of the cell to get vertex positions).
        float vertexOffset = cellSize * 0.5f;

        //populate the vertices and triangles
        for (int x = 0; x < gSizeX; x++)
        {
            for (int y = 0; y < gSizeY; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;

                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }

        
        
        
    }

    private void CreateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
