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
    [SerializeField]
    private int mainSeed=2;

    private Vector2Int camChunk=new Vector2Int();
    private Dictionary<Vector2Int,int> chunkIsCreate=new Dictionary<Vector2Int,int>();
    private int size=15;

    void Start()
    {
        Random.seed=mainSeed;
        camChunk=new Vector2Int(0,0);
        for(int i = -range; i <=range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                int chunkSeed = Random.Range(0, int.MaxValue);
                chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                chunk.setSeed(chunkSeed);
                chunk.makeChumk();
                Instantiate(water, new Vector3(i * size +10, 0, j * size+10), new Quaternion());
            }
        }
    }
    void Update()
    {
        if (camIsChangeChunk())
        {
            int changeByX= Mathf.FloorToInt(cam.transform.position.x/size)-camChunk.x;
            int changeByZ = Mathf.FloorToInt(cam.transform.position.z / size) - camChunk.y;

            camChunk = new Vector2Int(Mathf.FloorToInt(cam.transform.position.x / size),
                                        Mathf.FloorToInt(cam.transform.position.z / size));

            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("Ground");


            foreach (GameObject obj in objectsToDelete)
            {
                if((obj.transform.position.x < (camChunk.x - range) * 20)|| (obj.transform.position.x > (camChunk.x + range) * 20)
                    || (obj.transform.position.z < (camChunk.y - range) * 20)|| (obj.transform.position.z > (camChunk.y + range) * 20))
                Destroy(obj);
            }
            Debug.Log("ChangeByx" + changeByX);
            Debug.Log("ChangeByz" + changeByZ);

            if (changeByX != 0)
            {
                int i = camChunk.x + (range* changeByX);
                for (int j = camChunk.y - range; j <= camChunk.y + range; j++)
                {
                    if (!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
                    {
                        int chunkSeed = Random.Range(0, int.MaxValue);
                        chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                        Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                        chunk.setSeed(chunkSeed);
                        chunk.makeChumk();
                        Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
                    }
                    else
                    {
                        Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                        chunk.setSeed(chunkIsCreate[new Vector2Int(i, j)]);
                        chunk.makeChumk();
                        Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
                    }
                }
            }
            if (changeByZ != 0)
            {
                int j = camChunk.y + (range*changeByZ);
                for (int i = camChunk.x - range; i <= camChunk.x + range; i++)
                {

                    if (!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
                    {
                        int chunkSeed = Random.Range(0, int.MaxValue);
                        chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                        Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                        chunk.setSeed(chunkSeed);
                        chunk.makeChumk();
                        Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
                    }
                    else
                    {
                        Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                        chunk.setSeed(chunkIsCreate[new Vector2Int(i, j)]);
                        chunk.makeChumk();
                        Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
                    }
                }
            }


            //for (int i =camChunk.x-range; i <= camChunk.x+range; i++)
            //{
            //    for (int j = camChunk.y- range; j <= camChunk.y+range; j++)
            //    {
            //        if(!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
            //        {
            //            int chunkSeed = Random.Range(0, int.MaxValue);
            //            chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
            //            Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
            //            chunk.setSeed(chunkSeed);
            //            chunk.makeChumk();
            //            Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
            //        }
            //        else
            //        {
            //            Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
            //            chunk.setSeed(chunkIsCreate[new Vector2Int(i,j)]);
            //            chunk.makeChumk();
            //            Instantiate(water, new Vector3(i * size + 10, 0, j * size + 10), new Quaternion());
            //        }
            //    }
            //}
        }
    }

    private bool camIsChangeChunk()
    {
        int x, z;
        x = Mathf.FloorToInt(cam.transform.position.x/size);
        z = Mathf.FloorToInt(cam.transform.position.z / size);

        return camChunk.x != x || camChunk.y != z;
    }
}
