using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject cloud;
    [SerializeField]
    private int range=1;
    [SerializeField]
    private int mainSeed=2;
    [SerializeField]
    private bool manualSeed;
    [SerializeField]
    private GameObject treeSpawner;

    private IEnumerator coroutine;

    public void setSeed(string value)
    {
        mainSeed = int.Parse(value);
    }

    public void setIsManualSeed(bool value)
    {
        manualSeed = value;
    }


    private Vector2Int camChunk=new Vector2Int();
    private Dictionary<Vector2Int,int> chunkIsCreate=new Dictionary<Vector2Int,int>();
    private int size=70;
    private List<GameObject> trees;

    void Start()
    {
        trees = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            GameObject tree = treeSpawner;
            tree.tag = "tree";
            trees.Add(tree);
        }
        if (!manualSeed)
        {
            mainSeed = System.DateTime.Now.Millisecond;
        }
        Random.seed = mainSeed;

        camChunk = new Vector2Int(0, 0);
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                int chunkSeed = Random.Range(0, int.MaxValue);
                chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                chunk.setSeed(chunkSeed);
                chunk.makeChumk();
                chunk.mekeTreeForChunk(i * size, j * size, trees);
                Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
            }
        }
        water.transform.localScale = new Vector3(20f * range, 1, 20f * range);
        Instantiate(water, new Vector3(camChunk.x * size+size / 2 , 0, camChunk.y * size+size/2 ), new Quaternion());
        Instantiate(cloud, new Vector3((camChunk.x + 5) * size, 0, camChunk.y * size + 10), new Quaternion());
    }

    public void clearAndBuild()
    {
        if (!manualSeed)
        {
            mainSeed = System.DateTime.Now.Millisecond;
        }
        Random.seed = mainSeed;
        chunkIsCreate = new Dictionary<Vector2Int, int>();

        camChunk = new Vector2Int(Mathf.FloorToInt(cam.transform.position.x / size),
                                        Mathf.FloorToInt(cam.transform.position.z / size));

        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject obj in objectsToDelete)
        {
                Destroy(obj);
        }

        objectsToDelete = GameObject.FindGameObjectsWithTag("tree");
        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);
        }


        for (int i = -range + camChunk.x; i < range; i++)
        {
            for (int j = -range + camChunk.y; j < range; j++)
            {

                    int chunkSeed = Random.Range(0, int.MaxValue);
                    chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                    chunk.setSeed(chunkSeed);
                    chunk.makeChumk();
                    chunk.mekeTreeForChunk(i * size, j * size, trees);
                    Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                
            }
        }

        GameObject watterPlane = GameObject.FindGameObjectWithTag("Water");
        if (watterPlane != null)
            watterPlane.transform.position = new Vector3(camChunk.x * size + size / 2, 0, camChunk.y * size + size / 2);
        GameObject cloudSpawner = GameObject.FindGameObjectWithTag("Cloud");
        if (cloudSpawner != null)
            cloudSpawner.transform.position = new Vector3((camChunk.x + 5) * size, 0, camChunk.y * size);

    }

    void Update()
    {
        if (camIsChangeChunk())
        {
            int changeByX = Mathf.FloorToInt(cam.transform.position.x / size) - camChunk.x;
            int changeByZ = Mathf.FloorToInt(cam.transform.position.z / size) - camChunk.y;

            camChunk = new Vector2Int(Mathf.FloorToInt(cam.transform.position.x / size),
                                        Mathf.FloorToInt(cam.transform.position.z / size));

            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("Ground");
            foreach (GameObject obj in objectsToDelete)
            {
                if ((obj.transform.position.x < (camChunk.x - range) * size) || (obj.transform.position.x > (camChunk.x + range) * size)
                    || (obj.transform.position.z < (camChunk.y - range) * size) || (obj.transform.position.z > (camChunk.y + range) * size))
                    Destroy(obj);
            }

            objectsToDelete = GameObject.FindGameObjectsWithTag("tree");
            foreach (GameObject obj in objectsToDelete)
            {
                if (
                    (obj.transform.position.x < (camChunk.x - range) * size) || (obj.transform.position.x > (camChunk.x + range) * size + size / 2)
                    || (obj.transform.position.z < (camChunk.y - range) * size) || (obj.transform.position.z > (camChunk.y + range) * size+size/2)
                    )
                    Destroy(obj);
            }

            coroutine = addChunks(changeByX, changeByZ, true);
            StartCoroutine(coroutine);

            GameObject watterPlane = GameObject.FindGameObjectWithTag("Water");
            if (watterPlane != null)
                watterPlane.transform.position = new Vector3(camChunk.x * size + size/2, 0, camChunk.y * size +size/2);
            GameObject cloudSpawner = GameObject.FindGameObjectWithTag("Cloud");
            if (cloudSpawner != null)
                cloudSpawner.transform.position = new Vector3((camChunk.x + 5) * size, 0, camChunk.y * size);

        }
    }


    private IEnumerator addChunks(int changeByX, int changeByZ, bool wait)
    {
        if (changeByX != 0)
        {
            int i = camChunk.x + (range * changeByX);
            for (int j = camChunk.y - range; j <= camChunk.y + range; j++)
            {
                if (!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
                {
                    int chunkSeed = Random.Range(0, int.MaxValue);
                    chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                    chunk.setSeed(chunkSeed);
                    chunk.makeChumk();
                    chunk.mekeTreeForChunk(i * size, j * size, trees);
                    Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());    
                }
                else
                {
                    chunk.setSeed(chunkIsCreate[new Vector2Int(i, j)]);
                    chunk.makeChumk();
                    chunk.mekeTreeForChunk(i * size, j * size, trees);
                    Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                }

                if (wait)
                {
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }
        if (changeByZ != 0)
        {
            int j = camChunk.y + (range * changeByZ);
            for (int i = camChunk.x - range; i <= camChunk.x + range; i++)
            {

                if (!chunkIsCreate.ContainsKey(new Vector2Int(i, j)))
                {
                    int chunkSeed = Random.Range(0, int.MaxValue);
                    chunkIsCreate.Add(new Vector2Int(i, j), chunkSeed);
                    chunk.setSeed(chunkSeed);
                    chunk.makeChumk();
                    chunk.mekeTreeForChunk(i * size, j * size, trees);
                    Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                }
                else
                {
                    chunk.setSeed(chunkIsCreate[new Vector2Int(i, j)]);
                    chunk.makeChumk();
                    chunk.mekeTreeForChunk(i * size, j * size, trees);
                    Instantiate(chunk, new Vector3(i * size, 0, j * size), new Quaternion());
                }
                if (wait)
                {
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
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
