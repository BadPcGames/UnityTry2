using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worlLogic : MonoBehaviour
{

    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private MashGenerator chunk;
    [SerializeField]
    private GameObject water;
    [SerializeField]
    private int range=1;

    private Vector2Int camChunk=new Vector2Int();
    private Dictionary<Vector2Int,bool> chunkIsCreate=new Dictionary<Vector2Int,bool>();

    void Start()
    {
        camChunk=new Vector2Int(0,0);
        for(int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                chunkIsCreate.Add(new Vector2Int(i, j), true);
                Instantiate(chunk, new Vector3(i * 20, 0, j * 20), new Quaternion());
                Instantiate(water, new Vector3(i * 20+10, 0, j * 20+10), new Quaternion());
            }
        }
    }
    void Update()
    {
        if (camIsChangeChunk())
        {
            camChunk = new Vector2Int(Mathf.FloorToInt(cam.transform.position.x / 20),
                                        Mathf.FloorToInt(cam.transform.position.z / 20));
            for (int i =camChunk.x-range; i <= camChunk.x+range; i++)
            {
                for (int j = camChunk.y- range; j <= camChunk.y+range; j++)
                {
                    if(!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
                    {
                        chunkIsCreate.Add(new Vector2Int(i, j), true);
                        Instantiate(chunk, new Vector3(i * 20, 0, j * 20), new Quaternion());
                        Instantiate(water, new Vector3(i * 20 + 10, 0, j * 20 + 10), new Quaternion());
                    }   
                }
            }
        }
    }

    private bool camIsChangeChunk()
    {
        int x, z;
        x = Mathf.FloorToInt(cam.transform.position.x/20);
        z = Mathf.FloorToInt(cam.transform.position.z / 20);

        return camChunk.x != x || camChunk.y != z;
    }
}
