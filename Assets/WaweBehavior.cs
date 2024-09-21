using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaweBehavior : MonoBehaviour
{
    [SerializeField]
    private float watterHight = 0f;
    [SerializeField]
    private float watterColor = 1f;
    [SerializeField]
    private float watterTransposetive = 0.7f;
    [SerializeField]
    private Material material;

    private bool called = false;

    public void setColor(float value)
    {
        watterColor = value;
    }

    public void setHight(float value)
    {
        watterHight = value;
    }
    public void setTrans(float value)
    {
        watterTransposetive = value;
    }
    // Start is called before the first frame update
    public void Start()
    {
        var planeRenderer = gameObject.GetComponent<Renderer>();
  
        Color customColor = new Color(0f, 0f, watterColor, watterTransposetive);
        material.color = customColor;
        planeRenderer.material = material;
        transform.position = new Vector3(11, watterHight, 0);
    }

    void Update()
    {
        if (!called)
        {
            StartCoroutine(CallMethodInSeconds());
            called = true;
        }
        if (transform.position.x < -11)
        {
            transform.position= new Vector3(11,watterHight,0);
        }
    }

    IEnumerator CallMethodInSeconds()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x - 0.1f, watterHight, transform.position.z);
        called = false;
    }
}
