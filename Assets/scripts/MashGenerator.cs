using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MashGenerator : MonoBehaviour
{
    [SerializeField]
    private int xSize=20;
    [SerializeField]
    private int zSize=20;
    [SerializeField]
    private float minY =-3;
    [SerializeField]
    private float maxY = 5;
    [SerializeField]
    private int steps = 20;
    [SerializeField]
    private int players = 20;
    [SerializeField]
    private float dif = 3;
    [SerializeField]
    private int smoothIteration=2;
    [SerializeField]
    private Material material;
    [SerializeField] List<Layer> layers=new List<Layer>();

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
    Vector3[] vertices;
    int[] triangles;
    int seed;

    public void setSeed(int value)
    {
        seed = value;
    }

    public void makeChumk()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Create();
        UpdateMesh();
        GenerateTexture();
        float scale = 40.0f / (xSize + zSize);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Create()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        Random.seed = seed;
        Generator generator = new Generator(seed);
        generator.MinY = minY;
        generator.MaxY = maxY;

        vertices = generator.GetVertices(xSize, zSize, (int)(steps * Random.Range(0.3f, 1.5f)), (int)(players * Random.Range(0.3f, 1.5f)), (dif * Random.Range(0.3f, 1.5f)), smoothIteration);

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

       
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    //void OnDrawGizmos()
    //{
    //    if (vertices == null)
    //    {
    //        return;
    //    }
    //    float scale = 40.0f / (xSize + zSize);
       
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(new Vector3((vertices[i].x * scale) - 10, (vertices[i].y * scale), ((vertices[i].z) * scale) - 10), 0.1f);

    //    }
    //}

    private void GenerateTexture() 
    {
        float trueMin = minY;
        float trueMax = maxY;

        material.SetFloat("minTerrainHeight",trueMin);
        material.SetFloat("maxTerrainHeight", trueMax);

        int layersCount=layers.Count;
        material.SetInt("numTextures", layersCount);

        float[] heights = new float[layersCount];
        int index = 0;
        foreach (Layer l in layers)
        {
            heights[index] = l.startHieght;
            index++;
        }
        material.SetFloatArray("terrainHeights", heights);

        Texture2DArray textures = new Texture2DArray(64, 64, layersCount, TextureFormat.RGBA32, true);

        for (int i = 0; i < layersCount; i++)
        {
            textures.SetPixels(layers[i].texture.GetPixels(), i);
        }

        textures.Apply();
        material.SetTexture("terrainTextures", textures);
    }
}
[System.Serializable]
class Layer
{
    public Texture2D texture;
    [Range (0,1)]public float startHieght;
}
