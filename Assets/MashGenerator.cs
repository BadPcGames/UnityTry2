using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MashGenerator : MonoBehaviour
{
    private int xSize=20;
    private int zSize=20;
    private float minY =-5;
    private float maxY = 5;
    private int steps = 20;
    private int players = 20;
    private float dif = 3;
    private int smoothIteration=2;
    [SerializeField]
    private Gradient gradient;

    public void setXSize(float value)
    {
        xSize=(int)value;
    }
    public void setZSize(float value)
    {
        zSize = (int)value;
    }
    public void setMinY(float value)
    {
            minY = value;
    }
    public void setMaxY(float value)
    {
            maxY = value;
    }
    public void setSteps(float value)
    {
            steps = (int)value;
    }
    public void setPlayers(float value)
    {
            players = (int)value;
    }
    public void setDif(float value)
    {
            dif =value;
    }
    public void setSmooth(float value)
    {
            smoothIteration = (int)value;
    }


    Mesh mesh;
    Color[] colors;
    Vector3[] vertices;
    int[] triangles;

    public void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Create();
        UpdateMesh();
        float scale = 40.0f / (xSize + zSize);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    private void Update()
    {
       
    }

    void Create()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        Generator generator = new Generator();
        generator.MinY = minY;
        generator.MaxY = maxY;

        vertices = generator.GetVertices(xSize, zSize, steps, players, dif,smoothIteration);

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        float trueMin = float.MaxValue;
        float trueMax = float.MinValue;

        foreach (Vector3 el in vertices)
        {
            if (trueMin > el.y)
            {
                trueMin = el.y;
            }
            if (trueMax < el.y)
            {
                trueMax = el.y;
            }
        }

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height =Mathf.InverseLerp(minY,maxY ,vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                //float height = (float)vertices[i].y / (float)trueMax;
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }

    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        float scale = 40.0f / (xSize + zSize);
       
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3((vertices[i].x * scale) - 10, (vertices[i].y * scale), ((vertices[i].z) * scale) - 10), 0.1f);

        }
    }

}
