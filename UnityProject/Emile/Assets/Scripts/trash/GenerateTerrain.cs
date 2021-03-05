using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//procedural terrain starter code from Brackeys https://www.youtube.com/watch?v=64NblGkAabk
[RequireComponent(typeof(MeshFilter))]
public class GenerateTerrain : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    private int width = 10;
    private int depth = 10; 

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh(); 
    }

    void CreateShape()
    {
        vertices = new Vector3[width * depth];
        for (int w = 0; w < width; w++){
            for(int d = 0; d < depth; d++)
            {
                vertices[w * depth + d] = new Vector3(w, 0, d);
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
