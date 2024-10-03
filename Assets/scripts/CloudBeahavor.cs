using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudBeahavor : MonoBehaviour
{
    public GameObject cloudSpherePrefab; // Префаб для сферы облака
   

    private bool called = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= Random.RandomRange(3,5); i++)
        {
            float scale = Random.RandomRange(0.2f, 1.2f);
            Vector3 randomPosition = new Vector3(transform.position.x-scale-Random.RandomRange(-0.2f,0.2f),transform.position.y + Random.RandomRange(-0.2f, 0.3f), transform.position.z+ Random.RandomRange(-0.3f, 0.4f));
            
            cloudSpherePrefab.transform.localScale = new Vector3(scale,scale,scale);
            GameObject cloudSphere = Instantiate(cloudSpherePrefab, randomPosition, Quaternion.identity);
            cloudSphere.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!called)
        {
            StartCoroutine(CallMethodInSeconds());
            called = true;
        }
    }

    IEnumerator CallMethodInSeconds()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y, transform.position.z);
        called = false;
    }
  
}
