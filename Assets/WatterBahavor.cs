using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatterBahavor : MonoBehaviour
{
    [SerializeField]
    private float watterHight=0f;
    [SerializeField]
    private float watterColor = 1f;
    [SerializeField]
    private float watterTransposetive=0.7f;
    [SerializeField]
    private Material material;

    public void setColor(string? value)
    {
        if (value != null)
            watterColor = float.Parse(value);
    }

    public void setHight(string? value)
    {
        if (value != null)
            watterHight = float.Parse(value);
    }
    public void setTrans(string? value)
    {
        if (value != null)
            watterTransposetive = float.Parse(value);
    }

    // Start is called before the first frame update
    public void Start()
    {
        var planeRenderer = gameObject.GetComponent<Renderer>();
        Color customColor = new Color(0.0f, 0.0f, watterColor, watterTransposetive);
        material.color=customColor;
        planeRenderer.material=material;

        planeRenderer.transform.SetLocalPositionAndRotation(new Vector3(0,watterHight,0),new Quaternion());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
